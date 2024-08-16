using System.Text.Json.Serialization;
using EcoSensorApi.AirQuality.Properties;
using Gis.Net.Vector.DTO;

namespace EcoSensorApi.AirQuality.Vector;

public class AirQualityVectorDto : GisVectorManyDto<AirQualityPropertiesDto>, IAirQualityVector
{
    [JsonPropertyName("sourceData")]
    public ESourceData SourceData { get; set; }
    
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    
    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}