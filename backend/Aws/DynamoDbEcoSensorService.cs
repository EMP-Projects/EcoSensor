using Gis.Net.Aws.AWSCore.DynamoDb;
using NetTopologySuite.Features;

namespace EcoSensorApi.Aws;

/// <inheritdoc />
public class DynamoDbEcoSensorService : AwsDynamoDbService<DynamoDbEcoSensorModel, FeatureCollection>
{
    
}