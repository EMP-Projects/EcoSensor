using EcoSensorApi.AirQuality;
using EcoSensorApi.AirQuality.Properties;
using EcoSensorApi.AirQuality.Vector;
using EcoSensorApi.Config;
using Gis.Net.OpenMeteo.AirQuality;
using Gis.Net.Osm.OsmPg.Vector;
using Gis.Net.Vector;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace EcoSensorApi.MeasurementPoints;

/// <summary>
/// Service to calculate measurement points for air quality
/// </summary>
public class MeasurementPointsService : IMeasurementPointsService 
{
    private readonly OsmVectorService<EcoSensorDbContext> _osmVectorService;
    private readonly AirQualityVectorService _airQualityVectorService;
    private readonly AirQualityPropertiesService _airQualityPropertiesService;
    private readonly ILogger<MeasurementPointsService> _logger;
    private readonly IAirQualityService _airQualityService;
    private readonly ConfigService _configService;

    /// <summary>
    /// Service to calculate measurement points for air quality
    /// </summary>
    public MeasurementPointsService(
        OsmVectorService<EcoSensorDbContext> osmVectorService, 
        AirQualityVectorService airQualityVectorService, 
        ILogger<MeasurementPointsService> logger, 
        IAirQualityService airQualityService, 
        AirQualityPropertiesService airQualityPropertiesService, 
        ConfigService configService)
    {
        _osmVectorService = osmVectorService;
        _airQualityVectorService = airQualityVectorService;
        _logger = logger;
        _airQualityService = airQualityService;
        _airQualityPropertiesService = airQualityPropertiesService;
        _configService = configService;
    }

    /// <summary>
    /// Calculate measurement points for air quality.
    /// </summary>
    /// <returns>The total number of new measurement points created.</returns>
    /// <exception cref="Exception">Generic exception.</exception>
    public async Task<int> MeasurementPoints()
    {
        // leggo i records di vettori
        var listOsm = await _osmVectorService.List(new OsmVectorQuery
        {
            SrCode = 3857
        });
        
        if (listOsm.Count == 0)
        {
            const string msg = "I can't read geometries from OpenStreetMap";
            _logger.LogError(msg);
            return 0;
        }

        // read the feature collection of all measurement points to check for any duplicates
        var listMeasurementPoints = await _airQualityVectorService.List(new AirQualityVectorQuery
        {
            SrCode = 3857
        });
        
        var measurementPoints = new List<Point>();
        
        // read all the points created from the geometry coordinates
        foreach (var osm in listOsm)
        {
            // read all the coordinates
            var coordinates = osm.Geom?.Coordinates;
            // I create a collection of geographic points from coordinates
            var coordinatesGeom = coordinates?.Select(coordinate => GisUtility.CreatePoint(3857, coordinate)).ToList();
            
            if (coordinatesGeom is null)
                continue;

            // for each coordinate I select only those at a certain configured distance
            foreach (var c in from c in coordinatesGeom let isExistPointWithinDistance = measurementPoints.Any(x => x.Distance(c) <= 2500) where !isExistPointWithinDistance || measurementPoints.Count <= 0 select c)
            {
                measurementPoints.Add(c);

                var aqPoint = new AirQualityVectorDto
                {
                    SourceData = ESourceData.Osm,
                    Geom = c,
                    TimeStamp = DateTime.UtcNow,
                    Guid = Guid.NewGuid(),
                    EntityKey = osm.EntityKey,
                    EntityVectorId = osm.Id,
                    Lat = c.Y,
                    Lng = c.X
                };

                if (listMeasurementPoints?.Count == 0)
                    // if no other points exist in the database
                    await _airQualityVectorService.Insert(aqPoint);
                else
                {
                    // otherwise only if greater than or equal to the distance configured for the layer
                    var nearestPoint = listMeasurementPoints?.MinBy(p => p.Geom?.Distance(c));
                    if (nearestPoint?.Geom?.Distance(c) >= 2500)
                        await _airQualityVectorService.Insert(aqPoint);
                }
            }
        }

        // save the data in the database
        return await _airQualityVectorService.SaveContext();
    }

    /// <summary>
    /// Seeds the features in the database using a list of geometries.
    /// </summary>
    /// <returns>The number of features that were successfully seeded.</returns>
    /// <exception cref="Exception">Thrown if there is an error while seeding the features.</exception>
    public async Task<int> SeedFeatures()
    {
        var bboxList = await _configService.BBoxGeometries();
        var result = 0;
        var index = 0;
        for (; index < bboxList.Count; index++)
        {
            var bbox = bboxList[index];
            result += await _osmVectorService.SeedGeometries(bbox.BBox, bbox.KeyName);
        }

        return result;
    }

    private async Task<List<AirQualityPropertiesDto>> CreateListAqValues(
        EPollution pollution,
        double lat,
        double lng,
        double elevation,
        long gisId,
        string? unit,
        List<string?>? times,
        List<double?>? values,
        List<long?>? indexAq)
    {
        var result = new List<AirQualityPropertiesDto>();
        if (times is null) return result;
        
        foreach (var time in times.Select((value, i) => new { i, value}))
        {
            if (time.value is null) continue;
            if (values?[time.i] is null) continue;
            if (unit is null) continue;
            
            var airQualityProps = await _airQualityPropertiesService.List(new AirQualityPropertiesQuery
            {
                Begin = DateTime.Parse(time.value).ToUniversalTime(),
                Pollution = pollution,
                GisId = gisId,
                Source = EAirQualitySource.OpenMeteo,
                Longitude = lng,
                Latitude = lat
            });
            
            if (airQualityProps.Count != 0) continue;
            
            result.Add(new AirQualityPropertiesDto
            {
                EntityKey = $"{Pollution.GetPollutionSource(EAirQualitySource.OpenMeteo)}:{time.i}",
                TimeStamp = DateTime.UtcNow,
                Date = DateTime.Parse(time.value).ToUniversalTime(),
                PollutionText = Pollution.GetPollutionDescription(pollution),
                Pollution = pollution,
                Unit = unit,
                Value = values[time.i]!.Value,
                EuropeanAqi = indexAq?[time.i],
                Lat = lat,
                Lng = lng,
                GisId = gisId,
                SourceText = Pollution.GetPollutionSource(EAirQualitySource.OpenMeteo),
                Source = EAirQualitySource.OpenMeteo,
                Elevation = elevation
            });
        }

        return result;
    }

    /// <summary>
    /// Reads air quality values from the API and saves them in the database.
    /// </summary>
    /// <returns>The total number of new air quality data recorded.</returns>
    public async Task<int> AirQuality()
    {
        // controllo se ultimo dato rilevato è inferiore a 1 ora
        var ifLastAirQuality = await _airQualityPropertiesService.CheckIfLastMeasureIsOlderThanHourAsync();
        if (ifLastAirQuality)
        {
            _logger.LogInformation("The last air quality measurement is less than one hour old");
            return 0;
        }
        
        // read the records of the geographical coordinates of the measurement points
        var pointsFeatures = await _airQualityVectorService.FeatureCollection();

        if (pointsFeatures is null)
        {
            _logger.LogWarning("I was unable to read the features of the measurement points");
            return 0;
        }

        // extract the coordinates and make the API call to OpenMeteo eliminating the null ones
        var coordinates = pointsFeatures
            .Select(features => features.Attributes.GetOptionalValue(_airQualityVectorService.NameProperties) as AirQualityPropertiesDto)
            .Where(x => x is not null)
            .ToArray();

        if (coordinates.Length == 0)
        {
            const string msg = "I was unable to read the coordinates of the measurement points";
            _logger.LogError(msg);
            return 0;
        }

        const int step = 10;
        var listAllNewAirQualityDto = new List<AirQualityPropertiesDto>();

        for (var c = 0; c < coordinates.Length; c += step)
        {
            var nextItem = c + step - 1;
            
            // I take the coordinates in chunks of 10
            var chunkCoordinates = coordinates[c..(nextItem < coordinates.Length ? nextItem : coordinates.Length - 1)]
                .Select(coords =>
                {
                    var p = CoordinateConverter.ConvertWebMercatorToWgs84(coords!.Lng, coords.Lat);
                    return new AirQualityLatLng(p.Y, p.X);
                }).ToArray();
            
            // create the options for the OpenMeteo API
            var openMeteoOptions = new AirQualityOptions(chunkCoordinates);
            
            // read the data from the OpenMeteo API
            var resultAq = await _airQualityService.AirQuality(openMeteoOptions);
            if (resultAq is null)
            {
                const string msg = "I was unable to read from the OpenMeteo API";
                _logger.LogError(msg);
                return 0;
            }

            foreach (var r in resultAq)
            {
                if (r.Longitude is null && r.Latitude is null) continue;
                
                // convert the coordinates to WebMercator
                var pointWebMercator = CoordinateConverter.ConvertWgs84ToWebMercator(r.Longitude!.Value, r.Latitude!.Value);
                
                // I take the feature closest to the response coordinates from the OpenMeteo API
                var nearestPoint = pointsFeatures?.MinBy(x => x.Geometry.Distance(pointWebMercator));
                
                // read the feature ID
                var propId = nearestPoint?.Attributes.GetOptionalValue("Id").ToString();
                if (propId is null)
                {
                    const string msg = "I cannot read the measuring point ID";
                    _logger.LogError(msg);
                    return 0;
                }
                
                // add the PM10 data
                listAllNewAirQualityDto.AddRange(await CreateListAqValues(
                        EPollution.Pm10,
                        pointWebMercator.Y,
                        pointWebMercator.X,
                        r.Elevation!.Value,
                        long.Parse(propId),
                        
                        r.HourlyUnits?.Pm10,
                        r.Hourly?.Time,
                        r.Hourly?.Pm10,
                        r.Hourly?.EuropeanAqiPm10
                    ));
                
                // add the PM2.5 data
                listAllNewAirQualityDto.AddRange(await CreateListAqValues(
                    EPollution.Pm25,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    r.Elevation!.Value,
                    long.Parse(propId),
                    r.HourlyUnits?.Pm25,
                    r.Hourly?.Time,
                    r.Hourly?.Pm25,
                    r.Hourly?.EuropeanAqiPm25
                ));
                
                // add the carbon monoxide data
                listAllNewAirQualityDto.AddRange(await CreateListAqValues(
                    EPollution.CarbonMonoxide,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    r.Elevation!.Value,
                    long.Parse(propId),
                    r.HourlyUnits?.CarbonMonoxide,
                    r.Hourly?.Time,
                    r.Hourly?.CarbonMonoxide,
                    r.Hourly?.EuropeanAqi
                ));
                
                // add the ozone data
                listAllNewAirQualityDto.AddRange(await CreateListAqValues(
                    EPollution.Ozone,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    r.Elevation!.Value,
                    long.Parse(propId),
                    r.HourlyUnits?.Ozone,
                    r.Hourly?.Time,
                    r.Hourly?.Ozone,
                    r.Hourly?.EuropeanAqiOzone
                ));
                
                // add the sulphur dioxide data
                listAllNewAirQualityDto.AddRange(await CreateListAqValues(
                    EPollution.SulphurDioxide,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    r.Elevation!.Value,
                    long.Parse(propId),
                    r.HourlyUnits?.SulphurDioxide,
                    r.Hourly?.Time,
                    r.Hourly?.SulphurDioxide,
                    r.Hourly?.EuropeanAqiSulphurDioxide
                ));
                
                // add the nitrogen dioxide data
                listAllNewAirQualityDto.AddRange(await CreateListAqValues(
                    EPollution.NitrogenDioxide,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    r.Elevation!.Value,
                    long.Parse(propId),
                    r.HourlyUnits?.NitrogenDioxide,
                    r.Hourly?.Time,
                    r.Hourly?.NitrogenDioxide,
                    r.Hourly?.EuropeanAqiNitrogenDioxide
                ));
            }
        }
        
        // add and save data in the database
        foreach (var dto in listAllNewAirQualityDto)
            await _airQualityPropertiesService.Insert(dto);
        
        // save the data in the database
        return await _airQualityPropertiesService.SaveContext();
    }
    
    /// <summary>
    /// Retrieves the air quality features based on the provided query parameters.
    /// </summary>
    /// <param name="query">The query parameters for filtering the air quality features.</param>
    /// <returns>A <see cref="FeatureCollection"/> containing the air quality features or null if an error occurs.</returns>
    public async Task<FeatureCollection?> AirQualityFeatures(MeasurementsQuery? query)
    {
        // read the features openstreetmap vector
        var osmFeatures = await _osmVectorService.FeatureCollection(new OsmVectorQuery
        {
            SrCode = 3857,
            EntityKey = query?.City
        });

        if (osmFeatures is null)
        {
            const string msg = "I can't read the features of the OpenStreetMap vector";
            _logger.LogError(msg);
            return null;
        }

        foreach (var feature in osmFeatures)
        {
            var osmId = feature.Attributes.GetOptionalValue("Id");
            // read the air quality features
            var airQualityFeatures = await _airQualityVectorService.FeatureCollection(new AirQualityVectorQuery
            {
                SrCode = 3857,
                EntityVectorId = osmId is not null ? long.Parse(osmId.ToString()!) : null
            });
    
            if (airQualityFeatures is null)
            {
                const string msg = "I can't read the features of the air quality vector";
                _logger.LogError(msg);
                continue;
            }

            var properties = new List<AirQualityPropertiesDto>();
            foreach (var airQualityFeature in airQualityFeatures)
            {
                if (airQualityFeature.Attributes.GetOptionalValue(_airQualityVectorService.NameProperties) is
                    not List<AirQualityPropertiesDto> airQualityProperties) continue;
                
                // add the properties to the feature
                if (airQualityProperties.Count > 0)
                    properties.AddRange(airQualityProperties);
            }
    
            // add the properties to the feature
            if (properties.Count > 0)
                feature.Attributes.Add(_airQualityVectorService.NameProperties, properties);
        }

        return osmFeatures;
    }
}