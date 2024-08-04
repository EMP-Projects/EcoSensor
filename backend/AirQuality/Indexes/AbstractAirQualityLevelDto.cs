using System.Text.Json.Serialization;
using TeamSviluppo.DTO;
namespace EcoSensorApi.AirQuality.Indexes;

public abstract class AbstractAirQualityLevelDto : DtoBase, IAirQualityLevelDto
{
    [JsonPropertyName("period")]
    public TimeSpan Period { get; set; }
    
    [JsonPropertyName("min")]
    public double Min { get; set; }
    
    [JsonPropertyName("max")]
    public double Max { get; set; }
    
    [JsonPropertyName("levelName")]
    public required string LevelName { get; set; }
    
    [JsonPropertyName("color")]
    public required string Color { get; set; }
    
    [JsonPropertyName("pollution")]
    public required string Pollution { get; set; }
    
    [JsonPropertyName("unit")]
    public required string Unit { get; set; }
}