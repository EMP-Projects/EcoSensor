using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace EcoSensorApi.AirQuality.Indexes;

/// <summary>
/// Represents an abstract data transfer object for air quality levels.
/// </summary>
public abstract class AbstractAirQualityLevelDto : DtoBase, IAirQualityLevelDto
{
    /// <summary>
    /// Gets or sets the period of the air quality measurement.
    /// </summary>
    [JsonPropertyName("period")]
    public TimeSpan Period { get; set; }
    
    /// <summary>
    /// Gets or sets the minimum value of the air quality measurement.
    /// </summary>
    [JsonPropertyName("min")]
    public double Min { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum value of the air quality measurement.
    /// </summary>
    [JsonPropertyName("max")]
    public double Max { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the air quality level.
    /// </summary>
    [JsonPropertyName("levelName")]
    public required string LevelName { get; set; }
    
    /// <summary>
    /// Gets or sets the color associated with the air quality level.
    /// </summary>
    [JsonPropertyName("color")]
    public required string Color { get; set; }
    
    /// <summary>
    /// Gets or sets the type of pollution measured.
    /// </summary>
    [JsonPropertyName("pollution")]
    public required string Pollution { get; set; }
    
    /// <summary>
    /// Gets or sets the unit of the air quality measurement.
    /// </summary>
    [JsonPropertyName("unit")]
    public required string Unit { get; set; }
}