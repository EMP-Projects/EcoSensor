using System.Text.Json.Serialization;
using Gis.Net.Vector.DTO;

namespace EcoSensorApi.AirQuality.Vector;

/// <summary>
/// Represents a request for air quality vector data.
/// </summary>
public class AirQualityVectorRequest : GisVectorRequest, IAirQualityVector
{
    /// <summary>
    /// Gets or sets the source data type.
    /// </summary>
    [JsonPropertyName("sourceData")]
    public ESourceData SourceData { get; set; }
    
    /// <summary>
    /// Gets or sets the latitude.
    /// </summary>
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    
    /// <summary>
    /// Gets or sets the longitude.
    /// </summary>
    [JsonPropertyName("lng")]
    public double Lng { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("entityVectorId")]
    public long EntityVectorId { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("typeMonitoringData")]
    public ETypeMonitoringData TypeMonitoringData { get; set; }
}