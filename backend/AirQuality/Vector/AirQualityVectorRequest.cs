using System.Text.Json.Serialization;
using Gis.Net.Vector.DTO;

namespace EcoSensorApi.AirQuality.Vector;

public class AirQualityVectorRequest : GisVectorRequest, IAirQualityVector
{
    [JsonPropertyName("sourceData")]
    public ESourceData SourceData { get; set; }
    
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    
    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}