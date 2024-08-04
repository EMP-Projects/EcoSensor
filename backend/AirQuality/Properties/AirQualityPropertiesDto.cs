using System.Text.Json.Serialization;
using EcoSensorApi.AirQuality.Vector;
using TeamSviluppo.DTO;
using TeamSviluppo.Gis.NetCoreFw.Dto;
namespace EcoSensorApi.AirQuality.Properties;

/// <inheritdoc />
public class AirQualityPropertiesDto : DtoBase, IAirQualityPropertiesDto, IGisPropertiesDto<AirQualityVectorDto, AirQualityPropertiesDto>
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
    
    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("europeanAqi")]
    public long? EuropeanAqi { get; set; }
    
    [JsonPropertyName("usAqi")]
    public long? UsAqi { get; set; }
    
    [JsonPropertyName("pollutionText")]
    public required string PollutionText { get; set; }
    
    [JsonPropertyName("sourceText")]
    public required string SourceText { get; set; }
    
    [JsonPropertyName("source")]
    public EAirQualitySource Source { get; set; }

    [JsonPropertyName("pollution")]
    public EPollution Pollution { get; set; }
    
    [JsonPropertyName("gisId")]
    public long GisId { get; set; }
    
    [JsonPropertyName("gis")]
    public AirQualityVectorDto? Gis { get; set; }
}