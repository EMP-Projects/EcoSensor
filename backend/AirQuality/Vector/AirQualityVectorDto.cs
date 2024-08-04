using System.Text.Json.Serialization;
using EcoSensorApi.AirQuality.Properties;
using TeamSviluppo.Gis.NetCoreFw.Dto;
namespace EcoSensorApi.AirQuality.Vector;

public class AirQualityVectorDto : GisCoreManyDto<AirQualityPropertiesDto>, IAirQualityVector
{
    [JsonPropertyName("sourceData")]
    public ESourceData SourceData { get; set; }
    
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    
    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}