namespace EcoSensorApi.AirQuality.Indexes;

public interface IAirQualityLevelDto
{
    TimeSpan Period { get; set; }
    double Min { get; set; }
    double Max { get; set; }
    string LevelName { get; set; }
    string Color { get; set; }
    string Pollution { get; set; }
    string Unit { get; set; }
}