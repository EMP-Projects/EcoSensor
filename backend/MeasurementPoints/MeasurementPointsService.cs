using EcoSensorApi.AirQuality;
using EcoSensorApi.AirQuality.Indexes.Eu;
using EcoSensorApi.AirQuality.Indexes.Us;
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
    private readonly EuAirQualityLevelService _euAirQualityLevelService;
    private readonly UsAirQualityLevelService _usAirQualityLevelService;

    /// <summary>
    /// Service to calculate measurement points for air quality
    /// </summary>
    public MeasurementPointsService(
        OsmVectorService<EcoSensorDbContext> osmVectorService, 
        AirQualityVectorService airQualityVectorService, 
        ILogger<MeasurementPointsService> logger, 
        IAirQualityService airQualityService, 
        AirQualityPropertiesService airQualityPropertiesService, 
        ConfigService configService, 
        EuAirQualityLevelService euAirQualityLevelService, 
        UsAirQualityLevelService usAirQualityLevelService)
    {
        _osmVectorService = osmVectorService;
        _airQualityVectorService = airQualityVectorService;
        _logger = logger;
        _airQualityService = airQualityService;
        _airQualityPropertiesService = airQualityPropertiesService;
        _configService = configService;
        _euAirQualityLevelService = euAirQualityLevelService;
        _usAirQualityLevelService = usAirQualityLevelService;
    }

    /// <summary>
    /// Calculate measurement points for air quality.
    /// </summary>
    /// <returns>The total number of new measurement points created.</returns>
    /// <exception cref="Exception">Generic exception.</exception>
    public async Task<int> MeasurementPoints()
    {
        // distance between the points in meters
        const int matrixDistance = 2500;
        
        // read the feature collection of all OpenStreetMap geometries
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

            // for each coordinate I select only those at a certain configured distance (2500 mt)
            foreach (var c in from c in coordinatesGeom let isExistPointWithinDistance = measurementPoints.Any(x => !x.IsWithinDistance(c, matrixDistance)) where !isExistPointWithinDistance || measurementPoints.Count <= 0 select c)
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
                    // otherwise only if greater than or equal to the distance configured for the layer (2500 mt)
                    var nearestPoint = listMeasurementPoints?.MinBy(p => p.Geom?.Distance(c));
                    if (!(bool)nearestPoint?.Geom?.IsWithinDistance(c, matrixDistance))
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

            var color = string.Empty;
            
            if (indexAq?[time.i] is not null)
            {
                var colorIndexAq = (await _euAirQualityLevelService.List(new EuAirQualityQuery
                    {
                        Pollution = pollution,
                        Value = indexAq[time.i]
                    })).FirstOrDefault();

                if (colorIndexAq is not null)
                    color = colorIndexAq.Color;
            }
            
            result.Add(new AirQualityPropertiesDto
            {
                EntityKey = $"{Pollution.GetPollutionSource(EAirQualitySource.OpenMeteo)}:{time.i}",
                TimeStamp = DateTime.UtcNow,
                Date = DateTime.Parse(time.value).ToUniversalTime(),
                PollutionText = Pollution.GetPollutionDescription(pollution),
                Pollution = pollution,
                Unit = unit,
                Color = color,
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
        // check if last data detected is less than 1 hour
        var ifLastAirQuality = await _airQualityPropertiesService.CheckIfLastMeasureIsOlderThanHourAsync();
        if (ifLastAirQuality)
        {
            _logger.LogInformation("The last air quality measurement is less than one hour old");
            return 0;
        }
        
        // read the records of the geographical coordinates of the measurement points
        var listAirQuality = (await _airQualityVectorService.List(new AirQualityVectorQuery())).ToList();;

        if (listAirQuality.Count == 0)
        {
            _logger.LogWarning("I was unable to read the features of the measurement points");
            return 0;
        }

        var listAllNewAirQualityDto = new List<AirQualityPropertiesDto>();

        foreach (var airQuality in listAirQuality)
        {
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
                _logger.LogError(msg);
                return 0;
            }
            
            // check if the coordinates are null
            if (resultAq.Longitude is null && resultAq.Latitude is null) continue;
            
            // convert the coordinates to WebMercator
            var pointWebMercator = CoordinateConverter.ConvertWgs84ToWebMercator(resultAq.Longitude!.Value, resultAq.Latitude!.Value);
            
            // read the feature ID
            var propId = airQuality.Id;
            
            // add the PM10 data
            listAllNewAirQualityDto.AddRange(await CreateListAqValues(
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
            listAllNewAirQualityDto.AddRange(await CreateListAqValues(
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
            listAllNewAirQualityDto.AddRange(await CreateListAqValues(
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
            listAllNewAirQualityDto.AddRange(await CreateListAqValues(
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
            listAllNewAirQualityDto.AddRange(await CreateListAqValues(
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
            listAllNewAirQualityDto.AddRange(await CreateListAqValues(
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
        var airQualityList = await _airQualityVectorService.List(new AirQualityVectorQuery
        {
            EntityKey = query?.City
        });
        
        if (airQualityList.Count == 0)
        {
            const string msg = "I can't read the features of the air quality vector";
            _logger.LogError(msg);
            return null;
        }

        var features = airQualityList.Select(aq =>
        {
            if (aq.EntityVector?.Geom is null) return null;
            var feature = GisUtility.CreateEmptyFeature(3857, aq.EntityVector?.Geom!);
            feature.Attributes.Add("Id", aq.Id);
            feature.Attributes.Add("Latitude", aq.Lat);
            feature.Attributes.Add("Longitude", aq.Lng);
            feature.Attributes.Add("Data", aq.PropertiesCollection);
            feature.Attributes.Add("OSM", aq.EntityVector?.Properties);
            return feature;
        }).ToArray();
        
        var featureCollection = new FeatureCollection(features);
        
        return featureCollection;
    }
}