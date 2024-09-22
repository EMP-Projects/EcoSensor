using System.Text.Json.Serialization;
using EcoSensorApi.AirQuality.Properties;
using Gis.Net.Osm.OsmPg.Vector;
using Gis.Net.Vector.DTO;

namespace EcoSensorApi.AirQuality.Vector;

/// <summary>
/// Represents a data transfer object for air quality vector data.
/// </summary>
public class AirQualityVectorDto : GisVectorManyDto<AirQualityPropertiesDto>, IAirQualityVector
{
    /// <summary>
    /// Gets or sets the source data of the air quality vector.
    /// </summary>
    [JsonPropertyName("sourceData")]
    public ESourceData SourceData { get; set; }
    
    /// <summary>
    /// Gets or sets the latitude of the air quality vector.
    /// </summary>
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    
    /// <summary>
    /// Gets or sets the longitude of the air quality vector.
    /// </summary>
    [JsonPropertyName("lng")]
    public double Lng { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("entityVectorId")]
    public long EntityVectorId { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("typeMonitoringData")]
    public ETypeMonitoringData TypeMonitoringData { get; set; }

    /// <summary>
    /// Gets or sets the entity vector.
    /// </summary>
    /// <value>The entity vector.</value>
    [JsonPropertyName("entityVector"), JsonIgnore]
    public OsmVectorDto? EntityVector { get; set; }
}