namespace EcoSensorApi.AirQuality.Indexes.Us;

/// <summary>
/// Represents the US air quality levels.
/// </summary>
public enum EUsAirQualityLevel
{
    /// <summary>
    /// Indicates good air quality.
    /// </summary>
    Good,

    /// <summary>
    /// Indicates moderate air quality.
    /// </summary>
    Moderate,

    /// <summary>
    /// Indicates air quality that is unhealthy for sensitive groups.
    /// </summary>
    UnhealthyForSensitiveGroups,

    /// <summary>
    /// Indicates unhealthy air quality.
    /// </summary>
    Unhealthy,

    /// <summary>
    /// Indicates very unhealthy air quality.
    /// </summary>
    VeryUnhealthy,

    /// <summary>
    /// Indicates hazardous air quality.
    /// </summary>
    Hazardous
}