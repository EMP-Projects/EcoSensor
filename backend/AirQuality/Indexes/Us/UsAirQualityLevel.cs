using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace EcoSensorApi.AirQuality.Indexes.Us;

/// <summary>
/// Represents the US air quality level.
/// </summary>
[Table("us_air_quality_index")]
public class UsAirQualityLevel : ModelBase, IAirQualityLevel
{
    /// <summary>
    /// Gets or sets the period of the air quality measurement.
    /// </summary>
    [Column("period")]
    public TimeSpan Period { get; set; }
    
    /// <summary>
    /// Gets or sets the minimum value of the air quality measurement.
    /// </summary>
    [Column("min")]
    public double Min { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum value of the air quality measurement.
    /// </summary>
    [Column("max")]
    public double Max { get; set; }
    
    /// <summary>
    /// Gets or sets the color associated with the air quality level.
    /// </summary>
    [Column("color"), MaxLength(100)]
    public required string Color { get; set; }
    
    /// <summary>
    /// Gets or sets the type of pollution measured.
    /// </summary>
    [Column("pollution")]
    public EPollution Pollution { get; set; }
    
    /// <summary>
    /// Gets or sets the unit of the air quality measurement.
    /// </summary>
    [Column("unit"), MaxLength(50)]
    public required string Unit { get; set; }
    
    /// <summary>
    /// Gets or sets the air quality level.
    /// </summary>
    [Column("level")]
    public EUsAirQualityLevel Level { get; set; }
}