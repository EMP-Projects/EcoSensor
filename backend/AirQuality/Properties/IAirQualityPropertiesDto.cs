namespace EcoSensorApi.AirQuality.Properties;

public interface IAirQualityPropertiesDto : IAirQualityBase
{
    string? PollutionText { get; set; }
    string? SourceText { get; set; }
}