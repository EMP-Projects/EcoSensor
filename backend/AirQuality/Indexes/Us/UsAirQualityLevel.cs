using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace EcoSensorApi.AirQuality.Indexes.Us;

[Table("us_air_quality_index")]
public class UsAirQualityLevel : ModelBase, IAirQualityLevel
{
    [Column("period")]
    public TimeSpan Period { get; set; }
    
    [Column("min")]
    public double Min { get; set; }
    
    [Column("max")]
    public double Max { get; set; }
    
    [Column("color"), MaxLength(100)]
    public required string Color { get; set; }
    
    [Column("pollution")]
    public EPollution Pollution { get; set; }
    
    [Column("unit"), MaxLength(50)]
    public required string Unit { get; set; }
    
    [Column("level")]
    public EUsAirQualityLevel Level { get; set; }
}