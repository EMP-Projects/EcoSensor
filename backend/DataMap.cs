using System.Text.Json.Serialization;

namespace EcoSensorApi;

/// <inheritdoc />
public abstract class DataMap : IDataMap
{
    /// <inheritdoc />
    [JsonPropertyName("bucketName")]
    public string BucketName { get; set; } = "ecosensor-data";

    /// <inheritdoc />
    [JsonPropertyName("prefix")]
    public string Prefix { get; set; } = "data";

    /// <inheritdoc />
    [JsonPropertyName("data")]
    public string Data { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("entityKey")]
    public string EntityKey { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("lastUpdated")]
    public string LastUpdated { get; set; } = DateTime.UtcNow.ToString("O");

    /// <inheritdoc />
    [JsonPropertyName("typeMonitoringData")]
    public int TypeMonitoringData { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("center")]
    public double[]? Center { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataMap"/> class with the specified entity key.
    /// </summary>
    /// <param name="entityKey">The entity key to be used for initializing the data map.</param>
    /// <param name="typeMonitoringData"></param>
    protected DataMap(string entityKey, ETypeMonitoringData typeMonitoringData)
    {
        Data = $"{entityKey.ToLower()}.json";
        EntityKey = entityKey;
        TypeMonitoringData = (int)typeMonitoringData;
    }
}