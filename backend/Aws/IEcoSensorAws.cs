using EcoSensorApi.AirQuality;
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
    /// <param name="pollution"></param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task SaveAitQualityToDynamoDb(string key, AwsS3ObjectDto objS3, EPollution pollution);

    /// <summary>
    /// Saves a FeatureCollection to S3.
    /// </summary>
    /// <param name="bucketName">The name of the S3 bucket.</param>
    /// <param name="prefix">The prefix for the S3 object key.</param>
    /// <param name="key">The file name to upload S3</param>
    /// <param name="featureCollection">The FeatureCollection to save.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the saved S3 object.</returns>
    Task<AwsS3ObjectDto?> SaveFeatureCollectionToS3(string bucketName, string prefix, string key, FeatureCollection? featureCollection);
    
    /// <summary>
    /// Saves an object to an S3 bucket.
    /// </summary>
    /// <typeparam name="T">The type of the object to be saved.</typeparam>
    /// <param name="bucketName">The name of the S3 bucket.</param>
    /// <param name="prefix">The prefix for the S3 object key.</param>
    /// <param name="key">The key for the S3 object.</param>
    /// <param name="obj">The object to be saved.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the S3 object DTO if the operation is successful; otherwise, null.
    /// </returns>
    Task<AwsS3ObjectDto?> SaveObjectToS3<T>(string bucketName, string prefix, string key, T obj);
    
    /// <summary>
    /// Saves the next timestamp to S3.
    /// </summary>
    /// <param name="bucketName">The name of the S3 bucket.</param>
    /// <param name="prefix">The prefix for the S3 object key.</param>
    /// <param name="key">The file name to upload to S3.</param>
    /// <param name="nextTs">The next timestamp to save.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the saved S3 object.</returns>
    Task<AwsS3ObjectDto?> SaveNextTimeStampToS3(string bucketName, string prefix, string key, string nextTs);
    
    /// <summary>
    /// Checks if a file exists in the specified S3 bucket and prefix.
    /// </summary>
    /// <param name="bucketName">The name of the S3 bucket.</param>
    /// <param name="prefix">The prefix for the S3 object key.</param>
    /// <param name="fileName">The name of the file to check for existence.</param>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a boolean indicating whether the file exists.
    /// </returns>
    Task<bool> IsExistFile(string bucketName, string prefix, string fileName);
    
}