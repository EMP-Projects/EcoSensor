namespace EcoSensorApi.AirQuality.Vector;

public interface IAirQualityVector
{
    ESourceData SourceData { get; set; }
    double Lat { get; set; }
    double Lng { get; set; }
}