namespace EcoSensorApi.Config;

public interface IConfig
{
    ETypeSourceLayer TypeSource { get; set; }
    string Name { get; set; }
    string RegionField { get; set; }
    int RegionCode { get; set; }
    string? CityField { get; set; }
    int? CityCode { get; set; }
    int Distance { get; set; }
    int MatrixDistancePoints { get; set; }
}