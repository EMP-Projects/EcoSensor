using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace EcoSensorApi.Config;

/// <summary>
/// Represents a configuration request.
/// </summary>
public class ConfigRequest : RequestBase, IConfig
{
    /// <summary>
    /// Represents the type of data source for a configuration request.
    /// </summary>
    [JsonPropertyName("typeSource")]
    public ETypeSourceLayer TypeSource { get; set; }

    /// <summary>
    /// Represents the name of the configuration.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Represents a configuration request for a region field.
    /// </summary>
    [JsonPropertyName("regionField")]
    public required string RegionField { get; set; }

    /// <summary>
    /// Represents the region code for a configuration request.
    /// </summary>
    [JsonPropertyName("regionCode")]
    public int RegionCode { get; set; }

    /// <summary>
    /// Represents a city field in a configuration request.
    /// </summary>
    [JsonPropertyName("cityField")]
    public string? CityField { get; set; }

    /// <summary>
    /// Represents the city code specified in the <see cref="ConfigRequest"/> object.
    /// </summary>
    /// <remarks>
    /// The <see cref="CityCode"/> property is an optional property that represents the code of a city.
    /// It is used in the context of the <see cref="ConfigRequest"/> object to specify a city code for configuration purposes.
    /// </remarks>
    [JsonPropertyName("cityCode")]
    public int? CityCode { get; set; }

    /// <summary>
    /// Represents the distance property of the ConfigRequest class.
    /// </summary>
    /// <remarks>
    /// This property specifies the distance value to be used for a certain operation.
    /// </remarks>
    [JsonPropertyName("distance")]
    public int Distance { get; set; }

    /// <summary>
    /// Represents the number of matrix distance points.
    /// The matrix distance points determine how many points are included in the matrix distance calculation.
    /// </summary>
    [JsonPropertyName("matrixDistancePoints")]
    public int MatrixDistancePoints { get; set; }
}