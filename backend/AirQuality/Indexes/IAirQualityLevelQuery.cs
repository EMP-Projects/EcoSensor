namespace EcoSensorApi.AirQuality.Indexes;

public interface IAirQualityLevelQuery
{
    EPollution Pollution { get; set; }
    double Value { get; set; }
}