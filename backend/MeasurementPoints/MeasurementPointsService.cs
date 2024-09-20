using EcoSensorApi.AirQuality.Vector;
using EcoSensorApi.Aws;
using EcoSensorApi.Config;
using Gis.Net.Osm.OsmPg.Vector;
using NetTopologySuite.Features;

namespace EcoSensorApi.MeasurementPoints;

/// <summary>
/// Service to calculate measurement points for air quality
/// </summary>
public class MeasurementPointsService : IMeasurementPointsService 
{
    private readonly OsmVectorService<EcoSensorDbContext> _osmVectorService;
    private readonly AirQualityVectorService _airQualityVectorService;
    private readonly ILogger<MeasurementPointsService> _logger;
    private readonly ConfigService _configService;
    private readonly IEcoSensorAws _ecoSensorAws;

    /// <summary>
    /// Service to calculate measurement points for air quality
    /// </summary>
    public MeasurementPointsService(
        OsmVectorService<EcoSensorDbContext> osmVectorService, 
        AirQualityVectorService airQualityVectorService, 
        ILogger<MeasurementPointsService> logger, 
        ConfigService configService, 
        IEcoSensorAws ecoSensorAws)
    {
        _osmVectorService = osmVectorService;
        _airQualityVectorService = airQualityVectorService;
        _logger = logger;
        _configService = configService;
        _ecoSensorAws = ecoSensorAws;
    }

    /// <summary>
    /// Calculate measurement points for air quality.
    /// </summary>
    /// <returns>The total number of new measurement points created.</returns>
    /// <exception cref="Exception">Generic exception.</exception>
    public async Task<int> MeasurementPoints() 
        => await _airQualityVectorService.CreateMeasurementPoints();

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

    /// <summary>
    /// Reads air quality values from the API and saves them in the database.
    /// </summary>
    /// <returns>The total number of new air quality data recorded.</returns>
    public async Task<int> AirQuality() 
        => await _airQualityVectorService.CreateAirQuality();
    
    /// <summary>
    /// Retrieves the air quality features based on the provided query parameters.
    /// </summary>
    /// <param name="query">The query parameters for filtering the air quality features.</param>
    /// <returns>A <see cref="FeatureCollection"/> containing the air quality features or null if an error occurs.</returns>
    public async Task<FeatureCollection?> AirQualityFeatures(MeasurementsQuery? query = null) 
        => await _airQualityVectorService.GetAirQualityFeatures(query?.Place);
    
    /// <summary>
    /// Uploads the feature collection to either S3 or DynamoDB based on the API type specified in the configuration.
    /// </summary>
    /// <returns>A task that represents the asynchronous upload operation.</returns>
    public async Task UploadFeatureCollection()
    {
        // read the list of configuration layers
        var layers = await _configService.List(new ConfigQuery());
        
        foreach (var layer in layers)
        {
            // get the feature collection
            var featureCollection = await AirQualityFeatures(new MeasurementsQuery { Place = layer.EntityKey });
            
            if (featureCollection is null)
            {
                const string msg = "The feature collection is null";
                _logger.LogWarning(msg);
                continue;
            }
            
            // save the data in S3
            // get the prefix data (Es. "rome_latest.json")
            var key = $"{layer.EntityKey.Replace(" ", "_").ToLower()}_latest.json";
            var objS3 = await _ecoSensorAws.SaveFeatureCollectionToS3("ecosensor", "data", key, featureCollection);
            _logger.LogInformation("The feature collection was successfully uploaded to S3 with the result: {0}", objS3?.FileName);
        }
        
        // save the next timestamp in S3
        const string keyNextTs = "next_ts.txt";
        var nextTs = await _airQualityVectorService.LastDateMeasureAsync();
        
        if (nextTs is null)
        {
            const string msg = "The next timestamp is null";
            _logger.LogWarning(msg);
            return;
        }
        
        var objNextTsS3 = await _ecoSensorAws.SaveNextTimeStampToS3("ecosensor", "data", keyNextTs, nextTs);
        _logger.LogInformation("The next timestamp was successfully uploaded to S3 with the result: {0}", objNextTsS3?.FileName);
    }
    
    /// <inheritdoc />
    public async Task<int> DeleteOldRecords() 
        => await _airQualityVectorService.DeleteOldRecords();
}