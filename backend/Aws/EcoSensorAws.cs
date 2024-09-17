using System.Text;
using EcoSensorApi.AirQuality.Properties;
using Gis.Net.Aws.AWSCore.Exceptions;
using Gis.Net.Aws.AWSCore.S3.Dto;
using Gis.Net.Aws.AWSCore.S3.Services;
using Gis.Net.Vector;
using NetTopologySuite.Features;

namespace EcoSensorApi.Aws;

/// <inheritdoc />
public class EcoSensorAws : IEcoSensorAws
{
    private readonly AirQualityPropertiesService _airQualityPropertiesService;
    private readonly IAwsBucketService _awsBucketService;
    private readonly DynamoDbEcoSensorService _dynamoDbEcoSensorService;
    private readonly ILogger<EcoSensorAws> _logger;

    public EcoSensorAws(AirQualityPropertiesService airQualityPropertiesService,
        IAwsBucketService awsBucketService, 
        DynamoDbEcoSensorService dynamoDbEcoSensorService, 
        ILogger<EcoSensorAws> logger)
    {
        _airQualityPropertiesService = airQualityPropertiesService;
        _awsBucketService = awsBucketService;
        _dynamoDbEcoSensorService = dynamoDbEcoSensorService;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task SaveFeatureCollectionToDynamoDb(string key, AwsS3ObjectDto objS3)
    {
        try
        {
            var nextTs = await _airQualityPropertiesService.LastDateMeasureAsync() ?? DateTime.UtcNow.ToString("O");
            
            var item = new DynamoDbEcoSensorModel
            {
                Key = key,
                Data = objS3,
                NextTimeStamp = nextTs
            };
        
            await _dynamoDbEcoSensorService.InsertOrUpdateAsync(item);
        }
        catch (Exception ex)
        {
            // Log the exception
            var msg = $"An error occurred while save object to DynamoDb - {ex.Message}";
            _logger.LogError(msg);
        }
    }

    /// <inheritdoc />
    public async Task<AwsS3ObjectDto?> SaveFeatureCollectionToS3(string bucketName, string prefixData, FeatureCollection? featureCollection)
    {
        // Serialize the FeatureCollection to GeoJSON
        var geoJson = GisUtility.SerializeFeatureCollection(featureCollection);
        
        // Create a memory stream from the GeoJSON string
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(geoJson));
        
        _dynamoDbEcoSensorService.AddConverter<AwsS3ObjectDto>();
            
        try
        {
            // Upload the FeatureCollection to S3
            var resultS3 = await _awsBucketService.Upload(new AwsS3BucketUploadDto(memoryStream)
            {
                BucketName = bucketName,
                Prefix = prefixData,
                Replace = true
            }, default);

            _logger.LogInformation("The FeatureCollection was successfully uploaded to S3 with the result: {0}", resultS3.FileName);

            return resultS3;
        } catch (AwsExceptions awsEx)
        {
            // Log the exception
            var msg =
                $"An error occurred while serializing and uploading the FeatureCollection to S3 - {awsEx.Message}";
            _logger.LogError(msg);
            return null;
        }
    }
}