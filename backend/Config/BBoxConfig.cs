using NetTopologySuite.Geometries;

namespace EcoSensorApi.Config;

/// <summary>
/// Represents a bounding box configuration.
/// </summary>
public class BBoxConfig
{
    /// <summary>
    /// Represents a configuration for a bounding box (BBox) and its corresponding configuration data.
    /// </summary>
    public BBoxConfig(Geometry bBox, string keyName, string keyTag)
    {
        BBox = bBox;
        KeyName = keyName;
        KeyTag = keyTag;
    }

    /// <summary>
    /// Represents a bounding box configuration for the EcoSensorApi.
    /// </summary>
    public Geometry BBox { get; set; }
    /// <summary>
    /// Represents a configuration for a bounding box and its related settings.
    /// </summary>
    public string KeyName { get; set; }
    
    /// <summary>
    /// Gets or sets the key tag associated with the bounding box configuration.
    /// </summary>
    /// <value>The key tag associated with the bounding box configuration.</value>
    public string? KeyTag { get; set; }
}