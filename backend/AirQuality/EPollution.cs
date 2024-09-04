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
    /// Saharan dust particles close to surface level (10 meter above ground).
    /// </summary>
    Dust,

    /// <summary>
    /// Ammonia concentration. Only available for Europe.
    /// </summary>
    Ammonia,

    /// <summary>
    /// Particulate matter with diameter smaller than 10 µm (PM10) and smaller
    /// than 2.5 µm (PM2.5) close to surface (10 meter above ground).
    /// </summary>
    Pm10,

    /// <summary>
    /// Particulate matter with diameter smaller than 2.5 µm (PM2.5) close to surface (10 meter above ground).
    /// </summary>
    Pm25,

    /// <summary>
    /// Aerosol optical depth at 550 nm of the entire atmosphere to indicate haze.
    /// </summary>
    AerosolOpticalDepth,

    /// <summary>
    /// UV index considering clouds and clear sky. Info: https://confluence.ecmwf.int/display/CUSF/CAMS+global+UV+index
    /// </summary>
    UvIndex,

    /// <summary>
    /// UV index considering clear sky.
    /// </summary>
    UvIndexClearSky,

    /// <summary>
    /// Pollen for various plants. Only available in Europe as provided by CAMS European Air Quality forecast.
    /// </summary>
    AlderPollen,

    /// <summary>
    /// Birch pollen.
    /// </summary>
    BirchPollen,

    /// <summary>
    /// Grass pollen.
    /// </summary>
    GrassPollen,

    /// <summary>
    /// Mugwort pollen.
    /// </summary>
    MugwortPollen,

    /// <summary>
    /// Olive pollen.
    /// </summary>
    OlivePollen,

    /// <summary>
    /// Ragweed pollen.
    /// </summary>
    RagweedPollen,
}
