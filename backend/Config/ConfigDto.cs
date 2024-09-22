using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace EcoSensorApi.Config;

/// <summary>
/// Represents configuration data for a sensor.
/// </summary>
public class ConfigDto : DtoBase, IConfigBase
{

    /// <inheritdoc />
    [JsonPropertyName("regionName")]
    public string? RegionName { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("regionCode")]
    public int? RegionCode { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("provName")]
    public string? ProvName { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("provCode")]
    public int? ProvCode { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("cityCode")]
    public int? CityCode { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("cityName")]
    public string? CityName { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("typeMonitoringData")]
    public ETypeMonitoringData? TypeMonitoringData { get; set; }
}