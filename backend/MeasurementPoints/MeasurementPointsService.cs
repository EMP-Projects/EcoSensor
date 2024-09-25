using System.Globalization;
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
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Service to calculate measurement points for air quality
    /// </summary>
    public MeasurementPointsService(
        OsmVectorService<EcoSensorDbContext> osmVectorService, 
        AirQualityVectorService airQualityVectorService, 
        ILogger<MeasurementPointsService> logger, 
        ConfigService configService, 
        IEcoSensorAws ecoSensorAws, 
        IAwsSnsService awsSnsService, 
        IConfiguration configuration)
    {
        _osmVectorService = osmVectorService;
        _airQualityVectorService = airQualityVectorService;
        _logger = logger;
        _configService = configService;
        _ecoSensorAws = ecoSensorAws;
        _awsSnsService = awsSnsService;
        _configuration = configuration;
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
        
        // get the list of bounding boxes by air quality data types
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

    /// <inheritdoc />
    public async Task<string> GetNextTimeStamp(MeasurementsQuery query) 
        => await _airQualityVectorService.LastDateMeasureAsync(query) ?? DateTime.UtcNow.ToString("O");

    private async Task<bool> UploadFeatureCollectionAirQuality()
    {
        // read the list of configuration layers
        var layers = await _configService.List(new ConfigQuery
        {
            TypeMonitoringData = ETypeMonitoringData.AirQuality
        });
        
        var resultCreated = 0;
        var bucketName = _configuration["AWS_S3_BUCKET_NAME"] ?? "ecosensor-data";
        const string prefix = "air_quality";
        
        foreach (var layer in layers)
        {
            var keyFile = $"{layer.EntityKey.Replace(" ", "_").ToLower()}_{(int)ETypeMonitoringData.AirQuality}";
            var keyData = $"{layer.EntityKey}:{layer.TypeMonitoringData}";
            var dataFileName = $"{keyFile}_latest.json";
            var nextTsFileName = $"next_ts_{keyFile}.txt";
            
            var query = new MeasurementsQuery
            {
                EntityKey = keyData
            };

            // check if the data file and the next timestamp file exist
            var isExistDataFile = await _ecoSensorAws.IsExistFile(bucketName, prefix, dataFileName);
            var isExistNextTsFile = await _ecoSensorAws.IsExistFile(bucketName, prefix, nextTsFileName);
            
            // get the next timestamp
            var lastTs = await GetNextTimeStamp(query);
            // check if the next timestamp is less than the current timestamp
            var isNextTs = DateTime.ParseExact(lastTs, "O", CultureInfo.InvariantCulture) <= DateTime.UtcNow;
            
            if (!isNextTs && isExistDataFile && isExistNextTsFile)
            {
                _logger.LogWarning("The next timestamp is less than the current timestamp");
                continue;
            }
            
            // get the feature collection
            var featureCollection = await AirQualityFeatures(query);
            
            if (featureCollection is null || featureCollection.Count == 0)
            {
                _logger.LogWarning("The feature collection is null or empty");
                continue;
            }
            
            // save the data in S3
            // get the prefix data (Es. "rome_0_latest.json")
            await _ecoSensorAws.SaveFeatureCollectionToS3(bucketName, prefix, dataFileName, featureCollection);
            
            // log the result
            var msg = $"The feature collection was successfully uploaded to S3 with the result {dataFileName}";
            _logger.LogInformation(msg);
            
            // save the next timestamp in S3
            var nextTs = await GetNextTimeStamp(query);
        
            // save the next timestamp in S3
            await _ecoSensorAws.SaveNextTimeStampToS3(bucketName, prefix, nextTsFileName, nextTs);
            _logger.LogInformation($"The next timestamp was successfully uploaded to S3 with the result {nextTsFileName}");
            
            resultCreated++;
        }

        return resultCreated > 0;
    }
    
    /// <summary>
    /// Uploads the feature collection to either S3 or DynamoDB based on the API type specified in the configuration.
    /// </summary>
    /// <returns>A task that represents the asynchronous upload operation.</returns>
    public async Task<bool> UploadFeatureCollection()
    {
        var topicArn = _configuration["AWS_TOPIC_ARN"];
        if (string.IsNullOrEmpty(topicArn))
        {
            _logger.LogError("The AWS_TOPIC_ARN environment variable is not set");
            return false;
        }

        var resultFeatureCollection = 0;
        
        // seed the features
        resultFeatureCollection += await SeedFeatures();
        // calculate the measurement points
        resultFeatureCollection += await MeasurementPoints();
        
        // read the air quality data
        resultFeatureCollection += await AirQuality();
        
        if (resultFeatureCollection == 0)
        {
            _logger.LogWarning("No new features were found");
            return false;
        }
        
        var resultAirQuality = await UploadFeatureCollectionAirQuality();
        
        if (resultAirQuality)
        {
            await _awsSnsService.Publish(new AwsPublishDto
            {
                TopicArn = topicArn,
                Message = "Updated Air Quality feature collection"
            }, default);
        }
        
        // TODO: upload feature collection for other monitoring data types
        
        // publish the message to the SNS topic
        await _awsSnsService.Publish(new AwsPublishDto
        {
            TopicArn = topicArn,
            Message = "Data update finished"
        }, default);

        return resultAirQuality;
    }
    
    /// <inheritdoc />
    public async Task<int> DeleteOldRecords() 
        => await _airQualityVectorService.DeleteOldRecords();
}