using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace EcoSensorApi.Config;

/// <summary>
/// Represents configuration data for a sensor.
/// </summary>
public class ConfigDto : DtoBase, IConfig
{
    /// <summary>
    /// Represents a configuration data transfer object (DTO) for EcoSensorApi.
    /// </summary>
    [JsonPropertyName("typeSource")]
    public ETypeSourceLayer TypeSource { get; set; }

    /// <summary>
    /// Gets or sets the name of the configuration.
    /// </summary>
    /// <remarks>
    /// This property is defined in the <see cref="ConfigDto"/> class.
    /// </remarks>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Represents a property for specifying the region field in the configuration.
    /// </summary>
    [JsonPropertyName("regionField")]
    public required string RegionField { get; set; }

    /// <summary>
    /// Represents the region code for a configuration.
    /// </summary>
    [JsonPropertyName("regionCode")]
    public int RegionCode { get; set; }

    /// <summary>
    /// Represents a city field in the configuration.
    /// </summary>
    [JsonPropertyName("cityField")]
    public string? CityField { get; set; }

    /// <summary>
    /// Represents the city code associated with a configuration.
    /// </summary>
    [JsonPropertyName("cityCode")]
    public int? CityCode { get; set; }

    /// <summary>
    /// Represents the distance property of a configuration object.
    /// </summary>
    [JsonPropertyName("distance")]
    public int Distance { get; set; }

    /// <summary>
    /// Represents the matrix distance points configuration.
    /// </summary>
    [JsonPropertyName("matrixDistancePoints")]
    public int MatrixDistancePoints { get; set; }
}