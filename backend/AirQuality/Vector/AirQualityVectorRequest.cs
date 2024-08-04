using System.Text.Json.Serialization;
using TeamSviluppo.Gis.NetCoreFw.Dto;
namespace EcoSensorApi.AirQuality.Vector;

public class AirQualityVectorRequest : GisCoreRequest, IAirQualityVector
{
    [JsonPropertyName("sourceData")]
    public ESourceData SourceData { get; set; }
    
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    
    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}