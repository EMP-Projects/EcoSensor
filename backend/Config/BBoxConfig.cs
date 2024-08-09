using NetTopologySuite.Geometries;

namespace EcoSensorApi.Config;

public class BBoxConfig
{
    public BBoxConfig(Geometry bBox, ConfigDto config)
    {
        BBox = bBox;
        Config = config;
    }

    public Geometry BBox { get; set; }
    public ConfigDto Config { get; set; }
}