namespace EcoSensorApi.AirQuality.Properties;

public interface IAirQualityPropertiesModel : IAirQualityBase
{
    EPollution Pollution { get; set; }
    EAirQualitySource Source { get; set; }
}