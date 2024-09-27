namespace EcoSensorApi;

/// <summary>
/// Interface representing a map.
/// </summary>
public interface IDataMap
{
    /// <summary>
    /// Gets or sets the name of the bucket.
    /// </summary>
    /// <value>The name of the bucket.</value>
    string BucketName { get; set; }

    /// <summary>
    /// Gets or sets the prefix.
    /// </summary>
    /// <value>The prefix.</value>
    string Prefix { get; set; }
    
    /// <summary>
    /// Gets or sets the data associated with the map.
    /// </summary>
    /// <value>The data associated with the map.</value>
    string Data { get; set; }

    /// <summary>
    /// Gets or sets the entity key.
    /// </summary>
    /// <value>The entity key.</value>
    string EntityKey { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    /// <value>The last updated timestamp.</value>
    string LastUpdated { get; set; }

    /// <summary>
    /// Gets or sets the type of monitoring data.
    /// </summary>
    /// <value>The type of monitoring data.</value>
    int TypeMonitoringData { get; set; }

    /// <summary>
    /// Gets or sets the center coordinates.
    /// </summary>
    /// <value>The center coordinates.</value>
    double[]? Center { get; set; }
}