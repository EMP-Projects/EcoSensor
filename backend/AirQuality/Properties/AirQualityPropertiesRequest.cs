using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace EcoSensorApi.AirQuality.Properties;

public class AirQualityPropertiesRequest : RequestBase, IAirQualityPropertiesRequest
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    
    [JsonPropertyName("lng")]
    public double Lng { get; set; }
    
    [JsonPropertyName("value")]
    public double Value { get; set; }
    
    [JsonPropertyName("unit")]
    public required string Unit { get; set; }
    
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    
    [JsonPropertyName("europeanAqi")]
    public long? EuropeanAqi { get; set; }
    
    [JsonPropertyName("usaAqi")]
    public long? UsAqi { get; set; }
    
    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }
    
    [JsonPropertyName("source")]
    public EAirQualitySource Source { get; set; }
    
    [JsonPropertyName("pollution")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public EPollution Pollution { get; set; }
}