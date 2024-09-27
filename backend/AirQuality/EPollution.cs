namespace EcoSensorApi.AirQuality;

/// <summary>
/// Represents various types of pollution and related measurements.
/// </summary>
public enum EPollution
{
    /// <summary>
    /// Atmospheric gases close to surface (10 meter above ground).
    /// </summary>
    CarbonMonoxide,

    /// <summary>
    /// Nitrogen Dioxide.
    /// </summary>
    NitrogenDioxide,

    /// <summary>
    /// Sulphur Dioxide.
    /// </summary>
    SulphurDioxide,

    /// <summary>
    /// Ozone.
    /// </summary>
    Ozone,

    /// <summary>
    /// Particulate matter with diameter smaller than 10 µm (PM10) and smaller
    /// than 2.5 µm (PM2.5) close to surface (10 meter above ground).
    /// </summary>
    Pm10,

    /// <summary>
    /// Particulate matter with diameter smaller than 2.5 µm (PM2.5) close to surface (10 meter above ground).
    /// </summary>
    Pm25
}
