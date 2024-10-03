namespace EcoSensorApi.AirQuality.Properties;

/// <summary>
/// Represents a data transfer object for air quality properties.
/// </summary>
public interface IAirQualityPropertiesDto : IAirQualityBase
{
    /// <summary>
    /// Gets or sets the text description of the pollution.
    /// </summary>
    string? PollutionText { get; set; }

    /// <summary>
    /// Gets or sets the text description of the source.
    /// </summary>
    string? SourceText { get; set; }
    
    string? EuropeanAqiText { get; set; }
    string? UsAqiText { get; set; }
}