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
    public BBoxConfig(Geometry bBox, string keyName, int? distance)
    {
        BBox = bBox;
        KeyName = keyName;
        Distance = distance ?? 100;
    }

    /// <summary>
    /// Represents a bounding box configuration for the EcoSensorApi.
    /// </summary>
    public Geometry BBox { get; set; }
    /// <summary>
    /// Represents a configuration for a bounding box and its related settings.
    /// </summary>
    public string KeyName { get; set; }
    
    public int Distance { get; set; }
}