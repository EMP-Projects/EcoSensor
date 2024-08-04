namespace EcoSensorApi.AirQuality.Properties;

public interface IAirQualityPropertiesQuery
{
    long? GisId { get; set; }
    EPollution? Pollution { get; set; }
    DateTime? Begin { get; set; }
    DateTime? End { get; set; }
    EAirQualitySource? Source { get; set; }
    
    double? Latitude { get; set; }
    double? Longitude { get; set; }
}