using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace EcoSensorApi.Config;

/// <summary>
/// Represents a configuration request.
/// </summary>
public class ConfigRequest : RequestBase, IConfig
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
    [JsonPropertyName("distance")]
    public int Distance { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("matrixDistancePoints")]
    public int MatrixDistancePoints { get; set; }
}