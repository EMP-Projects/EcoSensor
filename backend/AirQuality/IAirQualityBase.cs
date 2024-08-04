namespace EcoSensorApi.AirQuality;

public interface IAirQualityBase
{
    double Lat { get; set; }
    double Lng { get; set; }
    double Value { get; set; }
    string Unit { get; set; }
    DateTime Date { get; set; }
    long? EuropeanAqi { get; set; }
    long? UsAqi { get; set; }
    
    double Elevation { get; set; }
    
    EAirQualitySource Source { get; set; }

    EPollution Pollution { get; set; }
}