namespace EcoSensorApi.AirQuality.Indexes;

public interface IAirQualityLevel
{
    TimeSpan Period { get; set; }
    double Min { get; set; }
    double Max { get; set; }
    string Color { get; set; }
    EPollution Pollution { get; set; }
    string Unit { get; set; }
}