namespace EcoSensorApi.AirQuality;

public enum EPollution
{
    CarbonMonoxide,         // Atmospheric gases close to surface (10 meter above ground)
    NitrogenDioxide,
    SulphurDioxide,
    Ozone,
    
    Dust,                   // Saharan dust particles close to surface level (10 meter above ground).
    
    Ammonia,                // Ammonia concentration. Only available for Europe.
    
    Pm10,                   // Particulate matter with diameter smaller than 10 µm (PM10) and smaller
                            // than 2.5 µm (PM2.5) close to surface (10 meter above ground)
    Pm25,
    
    AerosolOpticalDepth,    // Aerosol optical depth at 550 nm of the entire atmosphere to indicate haze.
    
    UvIndex,                // UV index considering clouds and clear sky. Info: https://confluence.ecmwf.int/display/CUSF/CAMS+global+UV+index
    UvIndexClearSky,
    
    AlderPollen,            // Pollen for various plants.
                            // Only available in Europe as provided by CAMS European Air Quality forecast.
    BirchPollen,
    GrassPollen,
    MugwortPollen,
    OlivePollen,
    RagweedPollen,
}
