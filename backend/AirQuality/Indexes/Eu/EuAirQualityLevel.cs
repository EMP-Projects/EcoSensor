using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace EcoSensorApi.AirQuality.Indexes.Eu;

[Table("eu_air_quality_index")]
public class EuAirQualityLevel : ModelBase, IAirQualityLevel
{
    [Column("period")]
    public TimeSpan Period { get; set; }
    
    [Column("min")]
    public double Min { get; set; }
    
    [Column("max")]
    public double Max { get; set; }
    
    [Column("color")]
    public required string Color { get; set; }
    
    [Column("pollution")]
    public EPollution Pollution { get; set; }
    
    [Column("unit")]
    public required string Unit { get; set; }
    
    [Column("level")]
    public EEuAirQualityLevel Level { get; set; }
}