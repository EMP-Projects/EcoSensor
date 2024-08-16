using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace EcoSensorApi.Config;

public class ConfigRequest : RequestBase, IConfig
{
    [JsonPropertyName("typeSource")]
    public ETypeSourceLayer TypeSource { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("regionField")]
    public required string RegionField { get; set; }
    
    [JsonPropertyName("regionCode")]
    public int RegionCode { get; set; }
    
    [JsonPropertyName("cityField")]
    public string? CityField { get; set; }
    
    [JsonPropertyName("cityCode")]
    public int? CityCode { get; set; }
    
    [JsonPropertyName("distance")]
    public int Distance { get; set; }
    
    [JsonPropertyName("matrixDistancePoints")]
    public int MatrixDistancePoints { get; set; }
}