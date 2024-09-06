namespace EcoSensorApi.AirQuality.Indexes;

/// <summary>
/// Interface for querying air quality levels.
/// </summary>
public interface IAirQualityLevelQuery
{
    /// <summary>
    /// Gets or sets the type of pollution measured.
    /// </summary>
    EPollution? Pollution { get; set; }

    /// <summary>
    /// Gets or sets the value of the air quality measurement.
    /// </summary>
    double? Value { get; set; }
}