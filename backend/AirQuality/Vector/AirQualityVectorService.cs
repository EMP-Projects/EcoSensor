using EcoSensorApi.AirQuality.Properties;
using EcoSensorApi.Config;
using Gis.Net.Core.Repositories;
using Gis.Net.OpenMeteo.AirQuality;
using Gis.Net.Osm.OsmPg.Vector;
using Gis.Net.Vector;
using Gis.Net.Vector.Services;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace EcoSensorApi.AirQuality.Vector;

/// <inheritdoc />
public class AirQualityVectorService : 
    GisVectorCoreManyService<AirQualityVectorModel, 
        AirQualityVectorDto, 
        AirQualityVectorQuery, 
        AirQualityVectorRequest, 
        EcoSensorDbContext, 
        AirQualityPropertiesModel, 
        AirQualityPropertiesDto>
{
    private readonly OsmVectorService<EcoSensorDbContext> _osmVectorService;
    private readonly ConfigService _configService;
    private readonly AirQualityPropertiesService _airQualityPropertiesService;
    private readonly IAirQualityService _airQualityService;
    
    /// <inheritdoc />
    public AirQualityVectorService(
        ILogger<AirQualityVectorService> logger, 
        AirQualityVectorRepository netCoreRepository, 
        OsmVectorService<EcoSensorDbContext> osmVectorService, 
        ConfigService configService, 
        AirQualityPropertiesService airQualityPropertiesService, 
        IAirQualityService airQualityService) : 
        base(logger, netCoreRepository)
    {
        _osmVectorService = osmVectorService;
        _configService = configService;
        _airQualityPropertiesService = airQualityPropertiesService;
        _airQualityService = airQualityService;
    }

    /// <inheritdoc />
    public override string? NameProperties { get; set; } = "AirQuality";

    /// <inheritdoc />
    protected override ListOptions<AirQualityVectorModel, AirQualityVectorDto, AirQualityVectorQuery> GetRowsOptions(
        AirQualityVectorQuery q) => new(q)
    {
        OnExtraMappingAsync = async (model, dto) =>
        {
            dto.EntityVector = await _osmVectorService.Find(model.EntityVectorId);
        }
    };

    /// <inheritdoc />
    protected override Task<Feature> OnLoadProperties(Feature feature, AirQualityVectorDto dto)
    {
        // Load the properties of the feature from the DTO object and add them to the feature attributes collection.
        var properties = dto.PropertiesCollection?.Where(x => x.Date >= DateTime.UtcNow).ToList();
        if (properties != null && properties.Count != 0)
            feature.Attributes.Add(NameProperties, properties);
        return Task.FromResult(feature);
    }

    /// <inheritdoc />
    protected override Task<long[]?> QueryParamsByProperties(AirQualityVectorQuery query) => Task.FromResult<long[]?>(null);
    
    private async Task<ICollection<OsmVectorDto>?> GetOsmVectorList(string keyName)
    {
        // read the list of OSM vectors
        var listOsm = await _osmVectorService.List(new OsmVectorQuery
        {
            SrCode = 3857,
            EntityKey = keyName
        });

        if (listOsm.Count != 0) return listOsm;
        
        const string msg = "I can't read geometries from OpenStreetMap";
        Logger.LogError(msg);
        return null;
    }
    
    /// <summary>
    /// Retrieves a list of air quality vectors based on the specified key name.
    /// </summary>
    /// <param name="keyName">The key name used to query the air quality vectors.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of 
    /// <see cref="AirQualityVectorDto"/> objects if found; otherwise, null.
    /// </returns>
    private async Task<ICollection<AirQualityVectorDto>?> GetAirQualityVectorList(string? keyName)
    {
        // read the list of air quality vectors
        var listAq = await List(new AirQualityVectorQuery
        {
            SrCode = 3857,
            EntityKey = keyName
        });

        if (listAq.Count != 0) return listAq;
    
        const string msg = "I can't read geometries from AirQuality";
        Logger.LogError(msg);
        return null;
    }

    private async Task<int> CreateMatrixPoints(string entityKey, double matrixDistance, ICollection<OsmVectorDto> listOsm)
    {
        if (listOsm.Count == 0)
        {
            Logger.LogWarning("The list of OSM vectors is empty");
            return 0;
        }
        
        // create a list of points
        var measurementPoints = new List<Point>();
    
        // read all the points created from the geometry coordinates
        foreach (var osm in listOsm)
        {
            // read all the coordinates
            var coordinates = osm.Geom?.Coordinates;
            // I create a collection of geographic points from coordinates
            var coordinatesGeom = coordinates?.Select(coordinate => GisUtility.CreatePoint(3857, coordinate)).ToList();

            if (coordinatesGeom is null)
            {
                Logger.LogWarning("I can't read the coordinates of the geometry - {0} - {1}", osm.Id, entityKey);
                continue;
            }

            // I select only the first point
            foreach (var coords in coordinatesGeom)
            {
                // if the list is empty, I add the first point
                if (measurementPoints.Count == 0)
                    measurementPoints.Add(coords);
                else
                {
                    // otherwise only if greater than or equal to the distance configured for the layer (2500 mt)
                    var nearestPoint = measurementPoints.MinBy(p => p.Distance(coords));
                    var distance = nearestPoint?.Distance(coords) ?? 0;
                    if (distance >= matrixDistance)
                        measurementPoints.Add(coords);
                    else
                    {
                        Logger.LogWarning("The distance between the points is less than the configured value - {0} - {1}", osm.Id, entityKey);
                        continue;
                    }
                }
                
                // create a new air quality point
                var aqPoint = new AirQualityVectorDto
                {
                    SourceData = ESourceData.Osm,
                    Geom = coords,
                    TimeStamp = DateTime.UtcNow,
                    Guid = Guid.NewGuid(),
                    EntityKey = entityKey,
                    EntityVectorId = osm.Id,
                    Lat = coords.Y,
                    Lng = coords.X
                };
                
                // insert new air quality point
                await Insert(aqPoint);
            }
        }
        
        // save the data in the database
        return await SaveContext();
    }
    
    /// <summary>
    /// Calculate measurement points for air quality.
    /// </summary>
    /// <returns>The total number of new measurement points created.</returns>
    /// <exception cref="Exception">Generic exception.</exception>
    public async Task<int> CreateMeasurementPoints()
    {
        // distance between the points in meters
        const double matrixDistance = 250;
        
        // read the list of configuration layers
        var layers = await _configService.List(new ConfigQuery());

        var resultSavedItems = 0;
        foreach (var layer in layers)
        {
            // read the list of OSM vectors
            var listOsm = await GetOsmVectorList(layer.EntityKey);
            if (listOsm is null)
            {
                Logger.LogWarning("I can't read the geometries from OpenStreetMap - {0}", layer.EntityKey);
                continue;
            }
            
            // select only the points
            var listOsmPoints = listOsm.Where(x => x.Geom != null && GisGeometries.IsPoint(x.Geom)).ToList();
            resultSavedItems += await CreateMatrixPoints(layer.EntityKey, matrixDistance, listOsmPoints);
            
            // select only the polygons
            var listOsmPolygons = listOsm.Where(x => x.Geom != null && GisGeometries.IsPolygon(x.Geom)).ToList();
            resultSavedItems += await CreateMatrixPoints(layer.EntityKey, matrixDistance, listOsmPolygons);
            
            // select only the lines
            var listOsmLines = listOsm.Where(x => x.Geom != null && GisGeometries.IsLineString(x.Geom)).ToList();
            resultSavedItems += await CreateMatrixPoints(layer.EntityKey, matrixDistance, listOsmLines);
            
        }
        
        // save the data in the database
        return resultSavedItems;
    }

    /// <summary>
    /// Reads air quality values from the API and saves them in the database.
    /// </summary>
    /// <returns>The total number of new air quality data recorded.</returns>
    public async Task<int> CreateAirQuality()
    {
        // read the list of configuration layers
        var layers = await _configService.List(new ConfigQuery());
        var resultSavedItems = 0;

        foreach (var layer in layers)
        {
            // read the records of the geographical coordinates of the measurement points
            var listAirQuality = await GetAirQualityVectorList(layer.EntityKey);

            if (listAirQuality is null)
            {
                Logger.LogWarning("I was unable to read the features of the measurement points by {0}", layer.EntityKey);
                continue;
            }
            
            foreach (var airQuality in listAirQuality)
            {
                // create a list of air quality properties
                var listAllNewAirQualityDto = new List<AirQualityPropertiesDto>();
                
                // convert the coordinates to WGS84
                var pointWgs84 = CoordinateConverter.ConvertWebMercatorToWgs84(airQuality.Lng, airQuality.Lat);
                
                // create the object with the coordinates in WGS84
                // and create the options for the OpenMeteo API
                var openMeteoOptions = new AirQualityOptions(new AirQualityLatLng(pointWgs84.Y, pointWgs84.X));
                
                // read the data from the OpenMeteo API
                var resultAq = await _airQualityService.AirQuality(openMeteoOptions);
                
                // check if the result is null
                if (resultAq is null)
                {
                    const string msg = "I was unable to read from the OpenMeteo API";
                    Logger.LogError(msg);
                    return 0;
                }
                
                // check if the coordinates are null
                if (resultAq.Longitude is null && resultAq.Latitude is null) continue;
                
                // convert the coordinates to WebMercator
                var pointWebMercator = CoordinateConverter.ConvertWgs84ToWebMercator(resultAq.Longitude!.Value, resultAq.Latitude!.Value);
                
                // read the feature ID
                var propId = airQuality.Id;
                
                // add the PM10 data
                listAllNewAirQualityDto.AddRange(await _airQualityPropertiesService.CreateListAirQualityValues(
                        EPollution.Pm10,
                        pointWebMercator.Y,
                        pointWebMercator.X,
                        resultAq.Elevation!.Value,
                        propId,
                        resultAq.HourlyUnits?.Pm10,
                        resultAq.Hourly?.Time,
                        resultAq.Hourly?.Pm10,
                        resultAq.Hourly?.EuropeanAqiPm10
                    ));
                
                // add the PM2.5 data
                listAllNewAirQualityDto.AddRange(await _airQualityPropertiesService.CreateListAirQualityValues(
                    EPollution.Pm25,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    resultAq.Elevation!.Value,
                    propId,
                    resultAq.HourlyUnits?.Pm25,
                    resultAq.Hourly?.Time,
                    resultAq.Hourly?.Pm25,
                    resultAq.Hourly?.EuropeanAqiPm25
                ));
                
                // add the carbon monoxide data
                listAllNewAirQualityDto.AddRange(await _airQualityPropertiesService.CreateListAirQualityValues(
                    EPollution.CarbonMonoxide,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    resultAq.Elevation!.Value,
                    propId,
                    resultAq.HourlyUnits?.CarbonMonoxide,
                    resultAq.Hourly?.Time,
                    resultAq.Hourly?.CarbonMonoxide,
                    resultAq.Hourly?.EuropeanAqi
                ));
                
                // add the ozone data
                listAllNewAirQualityDto.AddRange(await _airQualityPropertiesService.CreateListAirQualityValues(
                    EPollution.Ozone,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    resultAq.Elevation!.Value,
                    propId,
                    resultAq.HourlyUnits?.Ozone,
                    resultAq.Hourly?.Time,
                    resultAq.Hourly?.Ozone,
                    resultAq.Hourly?.EuropeanAqiOzone
                ));
                
                // add the sulphur dioxide data
                listAllNewAirQualityDto.AddRange(await _airQualityPropertiesService.CreateListAirQualityValues(
                    EPollution.SulphurDioxide,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    resultAq.Elevation!.Value,
                    propId,
                    resultAq.HourlyUnits?.SulphurDioxide,
                    resultAq.Hourly?.Time,
                    resultAq.Hourly?.SulphurDioxide,
                    resultAq.Hourly?.EuropeanAqiSulphurDioxide
                ));
                
                // add the nitrogen dioxide data
                listAllNewAirQualityDto.AddRange(await _airQualityPropertiesService.CreateListAirQualityValues(
                    EPollution.NitrogenDioxide,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    resultAq.Elevation!.Value,
                    propId,
                    resultAq.HourlyUnits?.NitrogenDioxide,
                    resultAq.Hourly?.Time,
                    resultAq.Hourly?.NitrogenDioxide,
                    resultAq.Hourly?.EuropeanAqiNitrogenDioxide
                ));
                
                // add and save data in the database
                foreach (var dto in listAllNewAirQualityDto)
                    await _airQualityPropertiesService.Insert(dto);
                
                if (listAllNewAirQualityDto.Count > 0)
                    resultSavedItems += await _airQualityPropertiesService.SaveContext();
            }
        }
        
        // save the data in the database
        return resultSavedItems;
    }

    /// <summary>
    /// Retrieves a collection of air quality features based on the specified key.
    /// </summary>
    /// <param name="key">The key used to query the air quality vectors.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="FeatureCollection"/> 
    /// if air quality vectors are found; otherwise, null.
    /// </returns>
    public async Task<FeatureCollection?> GetAirQualityFeatures(string? key)
    {
        // read the list of air quality vectors
        var airQualityList = await GetAirQualityVectorList(key);
        
        if (airQualityList is null || airQualityList.Count == 0)
        {
            // if the list is empty, I return null
            const string msg = "I can't read the features of the air quality vector";
            Logger.LogError(msg);
            return null;
        }
        
        // create a list of features
        var features = new List<IFeature>();
        
        // for each air quality vector
        foreach (var aq in airQualityList)
        {
            // check if the geometry is null
            if (aq.EntityVector?.Geom is null) continue;
            
            // get the properties
            var props = aq.PropertiesCollection?.Where(x => x.Date >= DateTime.UtcNow).ToList();
            
            // check if the properties are null or empty
            if (props is null || props.Count == 0) continue;
            
            // check if the feature already exists
            var feature = features.FirstOrDefault(x => x.Geometry.EqualsExact(aq.EntityVector?.Geom));
            
            if (feature is null) {
                // create a new feature from the linked OpenStreetMap geometry (EntityVector)
                var newFeature = GisUtility.CreateEmptyFeature(3857, aq.EntityVector?.Geom!);
                // if the feature does not exist, I create a new one
                newFeature.Attributes.Add("Data", props);
                newFeature.Attributes.Add("OSM", aq.EntityVector?.Properties);
                features.Add(newFeature);
                continue;
            }
            
            // if the feature exists, I add the properties to the existing feature
            var listProps = feature.Attributes["Data"] as List<AirQualityPropertiesDto>;
            listProps?.AddRange(props);
        }
        
        // create a feature collection
        return new FeatureCollection(features.ToArray());
    }
    
    /// <summary>
    /// Deletes old air quality records from the database.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the number of records deleted.
    /// </returns>
    public async Task<int> DeleteOldRecords() => await _airQualityPropertiesService.DeleteOldRecordsAsync();
}