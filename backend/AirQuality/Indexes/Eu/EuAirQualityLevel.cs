using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace EcoSensorApi.AirQuality.Indexes.Eu;

/// <summary>
/// Represents the European air quality level.
/// </summary>
[Table("eu_air_quality_index")]
public class EuAirQualityLevel : ModelBase, IAirQualityLevel
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
    [Column("color")]
    public required string Color { get; set; }
    
    /// <summary>
    /// Gets or sets the type of pollution measured.
    /// </summary>
    [Column("pollution")]
    public EPollution Pollution { get; set; }
    
    /// <summary>
    /// Gets or sets the unit of the air quality measurement.
    /// </summary>
    [Column("unit")]
    public required string Unit { get; set; }
    
    /// <summary>
    /// Gets or sets the air quality level.
    /// </summary>
    [Column("level")]
    public EEuAirQualityLevel Level { get; set; }
}