using Gis.Net.Aws.AWSCore.S3.Dto;
using NetTopologySuite.Features;

namespace EcoSensorApi.Aws;

/// <summary>
/// Interface for EcoSensor AWS operations.
/// </summary>
public interface IEcoSensorAws
{
    /// <summary>
    /// Saves a FeatureCollection to DynamoDB.
    /// </summary>
    /// <param name="key">The key under which the FeatureCollection will be saved.</param>
    /// <param name="objS3">The S3 object containing the FeatureCollection data.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task SaveFeatureCollectionToDynamoDb(string key, AwsS3ObjectDto objS3);

    /// <summary>
    /// Saves a FeatureCollection to S3.
    /// </summary>
    /// <param name="bucketName">The name of the S3 bucket.</param>
    /// <param name="prefixData">The prefix for the S3 object key.</param>
    /// <param name="featureCollection">The FeatureCollection to save.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the saved S3 object.</returns>
    Task<AwsS3ObjectDto?> SaveFeatureCollectionToS3(string bucketName, string prefixData, FeatureCollection? featureCollection);
}