using EcoSensorApi.AirQuality.Properties;
using EcoSensorApi.Config;
using EcoSensorApi.MeasurementPoints;
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
    /// <param name="query"></param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of 
    /// <see cref="AirQualityVectorDto"/> objects if found; otherwise, null.
    /// </returns>
    private async Task<ICollection<AirQualityVectorDto>?> GetAirQualityVectorList(MeasurementsQuery query)
    {
        var queryAq = new AirQualityVectorQuery
        {
            SrCode = 3857,
            EntityKey = query.EntityKey,
            TypeMonitoringData = query.TypeMonitoringData
        };
        
        // read the list of air quality vectors
        var listAq = await List(new ListOptions<AirQualityVectorModel, AirQualityVectorDto, AirQualityVectorQuery>(queryAq)
        {
            WithApplyInclude = false
        });

        if (listAq.Count != 0) return listAq;
    
        const string msg = "I can't read geometries from AirQuality";
        Logger.LogError(msg);
        return null;
    }
    
    private async Task<List<Point?>?> GetAirQualityPointsAsync(MeasurementsQuery query)
    {
        // read the list of air quality vectors
        var list = await GetAirQualityVectorList(query);
        if (list?.Count != 0) 
            return list?.Select(x => x.Geom as Point).Where(x => x is not null).ToList();
        
        Logger.LogWarning("The list of air quality vectors is empty");
        return [];
    }
    
    /// <summary>
    /// Retrieves a list of air quality vectors that do not have corresponding points in WGS84 coordinates from the current time onwards.
    /// </summary>
    /// <param name="query">The query parameters for retrieving air quality vectors.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of <see cref="AirQualityVectorDto"/> objects
    /// that do not have corresponding points in WGS84 coordinates from the current time onwards.
    /// </returns>
    private async Task<List<AirQualityVectorDto>?> GetAirQualityByNewPointsAsync(MeasurementsQuery query)
    {
        // read the list of air quality vectors
        var list = await GetAirQualityVectorList(query);
        // read the list of air quality points in WGS84 coordinates
        var points = (await _airQualityPropertiesService.GetAirQualityPointsFromNowAsync(query.TypeMonitoringData)).ToList();
        if (points.Count != 0) return list?.Where(x => !points.Any(y => y.EqualsExact(x.Geom))).ToList();
        Logger.LogWarning("The list of air quality points properties is empty");
        return list?.ToList();
    }

    private async Task<AirQualityVectorModel?> AddNewPoint(Point coords, OsmVectorDto osm, MeasurementsQuery query)
    {
        var aqPoint = new AirQualityVectorDto
        {
            SourceData = ESourceData.Osm,
            Geom = coords,
            TimeStamp = DateTime.UtcNow,
            Guid = Guid.NewGuid(),
            EntityKey = query.EntityKey!,
            EntityVectorId = osm.Id,
            TypeMonitoringData = query.TypeMonitoringData,
            Lat = coords.Y,
            Lng = coords.X
        };
        
        Logger.LogInformation("Insert new air quality point - {0} | {1} | [{2},{3}]", osm.Id, query.EntityKey, coords.Y, coords.X);
                
        // insert new air quality point
        return await Insert(aqPoint);
    }

    private async Task<MatrixPoints> CreateMatrixPoints(MeasurementsQuery query, ICollection<OsmVectorDto> listOsm, MatrixPoints previuos)
    {
        // create a list of points
        var measurementPoints = previuos.Points;
        
        // read all the points created from the geometry coordinates
        foreach (var osm in listOsm)
        {
            
            // read all the coordinates
            var coordinates = osm.Geom?.Coordinates;
            // I create a collection of geographic points from coordinates
            var coordinatesGeom = coordinates?.Select(coordinate => GisUtility.CreatePoint(3857, coordinate)).ToList();
            
            if (coordinatesGeom is null)
            {
                Logger.LogWarning("I can't read the coordinates of the geometry - {0} - {1}", osm.Id, query.EntityKey);
                continue;
            }

            foreach (var coords in coordinatesGeom)
            {
                if (measurementPoints.Count > 0 && measurementPoints.Any(p => p != null && p.EqualsExact(coords)))
                {
                    Logger.LogWarning("The center point already exists - {0} | {1} | [{2},{3}]", osm.Id, query.EntityKey, coords.Coordinate.X, coords.Coordinate.Y);
                    continue;
                }
                
                if (measurementPoints.Count == 0) 
                    measurementPoints.Add(coords);
                else
                {
                    // otherwise only if greater than or equal to the distance configured for the layer (2500 mt)
                    var nearestPoint = measurementPoints.MinBy(p => p?.Distance(coords));
                    var distance = nearestPoint?.Distance(coords) ?? 0;
                    var matrixDistance = osm.Geom is not null && (GisGeometries.IsPolygon(osm.Geom) || GisGeometries.IsLineString(osm.Geom)) ? 2500 : 0;
                    if (distance >= matrixDistance)
                        measurementPoints.Add(coords);
                    else
                    {
                        Logger.LogWarning("The distance between the points is less than the configured value - {0} - {1}", osm.Id, query.EntityKey);
                        continue;
                    }
                }
                
                await AddNewPoint(coords, osm, query);
            }
        }
        
        // save the data in the database
        var resultSaved = await SaveContext() + previuos.Count;
        
        return new MatrixPoints
        {
            Count = resultSaved,
            Points = measurementPoints
        };
    }
    
    /// <summary>
    /// Calculate measurement points for air quality.
    /// </summary>
    /// <returns>The total number of new measurement points created.</returns>
    /// <exception cref="Exception">Generic exception.</exception>
    public async Task<int> CreateMeasurementPoints()
    {
        var layers = new List<ConfigDto>();
        
        // read the list of configuration layers
        layers.AddRange(await _configService.List(new ConfigQuery
        {
            TypeMonitoringData = ETypeMonitoringData.AirQuality
        }));

        // TODO: create configuration lists for other monitoring data types for all values in the TypeMonitoringData enumeration
        
        var resultMatrixPoints = new MatrixPoints();
        foreach (var key in layers.Select(layer => $"{layer.EntityKey}:{layer.TypeMonitoringData}"))
        {
            // read the list of OSM vectors
            var listOsm = await GetOsmVectorList(key);
            if (listOsm is null)
            {
                Logger.LogWarning("I can't read the geometries from OpenStreetMap - {0}", key);
                continue;
            }
            
            var measurementQuery = new MeasurementsQuery
            {
                EntityKey = key,
                TypeMonitoringData = ETypeMonitoringData.AirQuality
            };
            
            // read the list of air quality vectors
            resultMatrixPoints.Points = await GetAirQualityPointsAsync(measurementQuery) ?? [];
            resultMatrixPoints = await CreateMatrixPoints(measurementQuery, listOsm, resultMatrixPoints);
        }
        
        // save the data in the database
        return resultMatrixPoints.Count;
    }

    /// <summary>
    /// Reads air quality values from the API and saves them in the database.
    /// </summary>
    /// <returns>The total number of new air quality data recorded.</returns>
    public async Task<int> CreateAirQuality()
    {
        // read the list of configuration layers
        var layers = await _configService.List(new ConfigQuery
        {
            TypeMonitoringData = ETypeMonitoringData.AirQuality
        });
        
        // create a variable to store the number of saved items
        var resultSavedItems = 0;
        
        foreach (var layer in layers)
        {
            var key = $"{layer.EntityKey}:{ETypeMonitoringData.AirQuality}";
            // read the records of the geographical coordinates of the measurement points
            var listAirQuality = await GetAirQualityByNewPointsAsync(new MeasurementsQuery
            {
                EntityKey = key,
                TypeMonitoringData = ETypeMonitoringData.AirQuality
            });

            if (listAirQuality is null)
            {
                Logger.LogWarning("I was unable to read the features of the measurement points by {0}", key);
                continue;
            }
            
            foreach (var airQuality in listAirQuality)
            {
                // create a list of air quality properties
                var listAllNewAirQualityDto = new List<AirQualityPropertiesDto>();
                
                // convert the coordinates to WGS84
                var pointWgs84 = CoordinateConverter.ConvertWebMercatorToWgs84(airQuality.Lng, airQuality.Lat);
                
                // and create the options for the OpenMeteo API
                var openMeteoOptions = new AirQualityOptions(new AirQualityLatLng(pointWgs84.Y, pointWgs84.X));
                
                // read the data from the OpenMeteo API
                var resultAq = await _airQualityService.AirQuality(openMeteoOptions);
                
                // check if the result and coordinate is null
                if (resultAq is null || (resultAq.Longitude is null && resultAq.Latitude is null))
                {
                    const string msg = "I was unable to read from the OpenMeteo API";
                    Logger.LogError(msg);
                    continue;
                }
                
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
                        key,
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
                    key,
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
                    key,
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
                    key,
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
                    key,
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
                    key,
                    resultAq.HourlyUnits?.NitrogenDioxide,
                    resultAq.Hourly?.Time,
                    resultAq.Hourly?.NitrogenDioxide,
                    resultAq.Hourly?.EuropeanAqiNitrogenDioxide
                ));
                
                Logger.LogInformation("Insert {0} new air quality properties", listAllNewAirQualityDto.Count);
                
                // add and save data in the database
                foreach (var dto in listAllNewAirQualityDto)
                    await _airQualityPropertiesService.Insert(dto);
                
                if (listAllNewAirQualityDto.Count > 0)
                    resultSavedItems += await _airQualityPropertiesService.SaveContext();
                
                Logger.LogInformation("Saved {0} new air quality properties", listAllNewAirQualityDto.Count);
            }
        }
        
        // save the data in the database
        return resultSavedItems;
    }

    /// <summary>
    /// Retrieves a collection of air quality features based on the specified key.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="FeatureCollection"/> 
    /// if air quality vectors are found; otherwise, null.
    /// </returns>
    public async Task<FeatureCollection?> GetAirQualityFeatures(MeasurementsQuery query)
    {
        
        if (string.IsNullOrEmpty(query.EntityKey))
        {
            const string msg = "The key name is null or empty";
            Logger.LogError(msg);
            return null;
        }
        
        var listOsm = await GetOsmVectorList(query.EntityKey);
        
        if (listOsm is null || listOsm.Count == 0)
        {
            // if the list is empty, I return null
            const string msg = "I can't read the features of the OpenStreetMap";
            Logger.LogError(msg);
            return null;
        }
        
        // create a list of features
        var features = new List<IFeature>();
        
        // read the air quality properties
        var listAirQuality = (await List(new AirQualityVectorQuery
        {
            EntityKey = query.EntityKey,
            TypeMonitoringData = query.TypeMonitoringData
        })).ToList();
        
        if (listAirQuality.Count == 0)
        {
            // if the list is empty, I return null
            const string msg = "I can't read the features of the AirQuality";
            Logger.LogError(msg);
            return null;
        }
        
        // for each air quality vector
        foreach (var osm in listOsm)
        {
            // check if the geometry is null
            if (osm.Geom is null) continue;
            
            // get the properties
            var aq = listAirQuality.MinBy(x => osm.Geom.Distance(x.Geom));
            
            // check if the properties are null or empty
            if (aq is null)
            {
                Logger.LogWarning("The air quality data are null or empty");
                continue;
            };

            if (aq.PropertiesCollection is null || aq.PropertiesCollection?.Count == 0)
            {
                Logger.LogWarning("The air quality properties are null");
                continue;
            }
            
            // get the properties from now
            var props = aq.PropertiesCollection?.Where(x => x.Date >= DateTime.UtcNow).ToList();
            if (props is null || props.Count == 0)
            {
                Logger.LogWarning("The air quality properties from now are null or empty");
                continue;
            }
            
            // check if the feature already exists
            var feature = features.FirstOrDefault(x => x.Geometry.EqualsExact(osm.Geom));

            if (feature is null)
            {
                // create a new feature from the linked OpenStreetMap geometry (EntityVector)
                var newFeature = GisUtility.CreateEmptyFeature(3857, osm.Geom!);
                // if the feature does not exist, I create a new one
                newFeature.Attributes.Add("Data", props);
                newFeature.Attributes.Add("OSM", osm.Properties);
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
    public async Task<int> DeleteOldRecords() => await _airQualityPropertiesService.DeleteOldRecordsAsync(1);
    
    /// <summary>
    /// Retrieves the last date of measurement from the air quality properties service.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the last date of measurement as a string if found; otherwise, null.
    /// </returns>
    public async Task<string?> LastDateMeasureAsync(MeasurementsQuery query) => await _airQualityPropertiesService.LastDateMeasureAsync(query);
}