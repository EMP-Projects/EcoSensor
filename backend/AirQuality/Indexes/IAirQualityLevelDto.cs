namespace EcoSensorApi.AirQuality.Indexes;

/// <summary>
/// Interface for air quality level data transfer objects.
/// </summary>
public interface IAirQualityLevelDto
{
    /// <summary>
    /// Gets or sets the period of the air quality measurement.
    /// </summary>
    TimeSpan Period { get; set; }

    /// <summary>
    /// Gets or sets the minimum value of the air quality measurement.
    /// </summary>
    double Min { get; set; }

    /// <summary>
    /// Gets or sets the maximum value of the air quality measurement.
    /// </summary>
    double Max { get; set; }

    /// <summary>
    /// Gets or sets the name of the air quality level.
    /// </summary>
    string LevelName { get; set; }

    /// <summary>
    /// Gets or sets the color associated with the air quality level.
    /// </summary>
    string Color { get; set; }

    /// <summary>
    /// Gets or sets the type of pollution measured.
    /// </summary>
    string Pollution { get; set; }

    /// <summary>
    /// Gets or sets the unit of the air quality measurement.
    /// </summary>
    string Unit { get; set; }
}