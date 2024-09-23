using EcoSensorApi.AirQuality.Vector;
using EcoSensorApi.Aws;
using EcoSensorApi.Config;
using Gis.Net.Aws.AWSCore.SNS.Dto;
using Gis.Net.Aws.AWSCore.SNS.Services;
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
    private readonly IAwsSnsService _awsSnsService;

    /// <summary>
    /// Service to calculate measurement points for air quality
    /// </summary>
    public MeasurementPointsService(
        OsmVectorService<EcoSensorDbContext> osmVectorService, 
        AirQualityVectorService airQualityVectorService, 
        ILogger<MeasurementPointsService> logger, 
        ConfigService configService, 
        IEcoSensorAws ecoSensorAws, 
        IAwsSnsService awsSnsService)
    {
        _osmVectorService = osmVectorService;
        _airQualityVectorService = airQualityVectorService;
        _logger = logger;
        _configService = configService;
        _ecoSensorAws = ecoSensorAws;
        _awsSnsService = awsSnsService;
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
        var bboxList = new List<BBoxConfig>();
        
        // get the list of bounding boxes by airquality data types
        bboxList.AddRange(await _configService.BBoxGeometries(new ConfigQuery
        {
            TypeMonitoringData = ETypeMonitoringData.AirQuality
        }));
        
        // TODO: create bboxList for other monitoring data types for all values in the TypeMonitoringData enum
        
        var result = 0;
        var index = 0;
        for (; index < bboxList.Count; index++)
            result += await _osmVectorService.SeedGeometries(bboxList[index].BBox, bboxList[index].KeyName);
        
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
    public async Task<FeatureCollection?> AirQualityFeatures(MeasurementsQuery query) 
        => await _airQualityVectorService.GetAirQualityFeatures(query);

    private async Task<bool> UploadFeatureCollectionAirQuality()
    {
        // read the list of configuration layers
        var layers = await _configService.List(new ConfigQuery
        {
            TypeMonitoringData = ETypeMonitoringData.AirQuality
        });
        
        var resultCreated = 0;
        
        foreach (var layer in layers)
        {
            var keyFile = $"{layer.EntityKey.Replace(" ", "_").ToLower()}_{(int)ETypeMonitoringData.AirQuality}";
            var keyData = $"{layer.EntityKey}:{layer.TypeMonitoringData}";
            var query = new MeasurementsQuery
            {
                EntityKey = keyData,
                TypeMonitoringData = ETypeMonitoringData.AirQuality
            };
            
            // get the feature collection
            var featureCollection = await AirQualityFeatures(query);
            
            if (featureCollection is null)
            {
                _logger.LogWarning("The feature collection is null");
                continue;
            }
            
            // save the data in S3
            // get the prefix data (Es. "rome_0_latest.json")
            await _ecoSensorAws.SaveFeatureCollectionToS3("ecosensor", "data", $"{keyFile}_latest.json", featureCollection);
            
            // log the result
            var msg = $"The feature collection was successfully uploaded to S3 with the result {keyFile}_latest.json";
            _logger.LogInformation(msg);
            
            // save the next timestamp in S3
            var nextTs = await _airQualityVectorService.LastDateMeasureAsync(query) ?? DateTime.UtcNow.ToString("O");
        
            // save the next timestamp in S3
            await _ecoSensorAws.SaveNextTimeStampToS3("ecosensor", "data", $"next_ts_{keyFile}.txt", nextTs);
            _logger.LogInformation($"The next timestamp was successfully uploaded to S3 with the result next_ts_{keyFile}.txt");
            
            // publish the message to the SNS topic
            await _awsSnsService.Publish(new AwsPublishDto
            {
                TopicArn = _awsSnsService.TopicArnDefault,
                Message = msg
            }, default);
            
            resultCreated++;
        }

        return resultCreated > 0;
    }
    
    /// <summary>
    /// Uploads the feature collection to either S3 or DynamoDB based on the API type specified in the configuration.
    /// </summary>
    /// <returns>A task that represents the asynchronous upload operation.</returns>
    public async Task UploadFeatureCollection()
    {
        var resultAirQuality = await UploadFeatureCollectionAirQuality();
        
        // TODO: upload feature collection for other monitoring data types
        
        if (resultAirQuality) {
            // publish the message to the SNS topic
            await _awsSnsService.Publish(new AwsPublishDto
            {
                TopicArn = _awsSnsService.TopicArnDefault,
                Message = "Updated feature collection"
            }, default);
        }
    }
    
    /// <inheritdoc />
    public async Task<int> DeleteOldRecords() 
        => await _airQualityVectorService.DeleteOldRecords();
}