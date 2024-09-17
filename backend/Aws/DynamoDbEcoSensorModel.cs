using Amazon.DynamoDBv2.DataModel;
using Gis.Net.Aws.AWSCore.DynamoDb;
using Gis.Net.Aws.AWSCore.DynamoDb.Models;
using Gis.Net.Aws.AWSCore.S3.Dto;

namespace EcoSensorApi.Aws;

/// <inheritdoc />
[DynamoDBTable("EcoSensor-Data", LowerCamelCaseProperties=true)]
public class DynamoDbEcoSensorModel : AwsDynamoDbTableBase
{
    /// <summary>
    /// Gets or sets the GeoJson feature collection.
    /// </summary>
    /// <remarks>
    /// The property is mapped to the "geoJson" attribute in the DynamoDB table.
    /// </remarks>
    [DynamoDBProperty("Data", typeof(AwsDynamoDbConverter<AwsS3ObjectDto>))]
    public AwsS3ObjectDto? Data { get; set; }
}