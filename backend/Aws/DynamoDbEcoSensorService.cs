using Gis.Net.Aws.AWSCore.DynamoDb;

namespace EcoSensorApi.Aws;

/// <inheritdoc />
public class DynamoDbEcoSensorService : AwsDynamoDbService<DynamoDbEcoSensorModel>
{
    /// <inheritdoc />
    public DynamoDbEcoSensorService(IConfiguration configuration) : base(configuration)
    {
    }
}