using Amazon.DynamoDBv2.DataModel;
using Gis.Net.Aws.AWSCore.DynamoDb.Models;
using NetTopologySuite.Features;

namespace EcoSensorApi.Aws;

/// <inheritdoc />
[DynamoDBTable("ecosensor-data")]
public class DynamoDbEcoSensorModel : AwsDynamoDbTable<FeatureCollection>
{
    
}