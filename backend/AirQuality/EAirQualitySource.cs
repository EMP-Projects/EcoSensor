namespace EcoSensorApi.AirQuality;

/// <summary>
/// Represents the source of air quality data.
/// </summary>
public enum EAirQualitySource
{
    /// <summary>
    /// Data sourced from OpenMeteo.
    /// </summary>
    OpenMeteo,

    /// <summary>
    /// Data sourced from IoT devices.
    /// </summary>
    Iot
}