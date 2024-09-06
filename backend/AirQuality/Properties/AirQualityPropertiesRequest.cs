using System.Text.Json.Serialization;
using Gis.Net.Core.DTO;

namespace EcoSensorApi.AirQuality.Properties;

/// <summary>
/// Represents a request for air quality properties.
/// </summary>
public class AirQualityPropertiesRequest : RequestBase, IAirQualityPropertiesRequest
{
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
    
    /// <summary>
    /// Gets or sets the value of the air quality measurement.
    /// </summary>
    [JsonPropertyName("value")]
    public double Value { get; set; }
    
    /// <summary>
    /// Gets or sets the unit of the air quality measurement.
    /// </summary>
    [JsonPropertyName("unit")]
    public required string Unit { get; set; }
    
    /// <summary>
    /// Gets or sets the date of the air quality measurement.
    /// </summary>
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Gets or sets the European Air Quality Index (AQI).
    /// </summary>
    [JsonPropertyName("europeanAqi")]
    public long? EuropeanAqi { get; set; }
    
    /// <summary>
    /// Gets or sets the USA Air Quality Index (AQI).
    /// </summary>
    [JsonPropertyName("usaAqi")]
    public long? UsAqi { get; set; }
    
    /// <summary>
    /// Gets or sets the elevation at which the air quality measurement was taken.
    /// </summary>
    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }
    
    /// <summary>
    /// Gets or sets the source of the air quality data.
    /// </summary>
    [JsonPropertyName("source")]
    public EAirQualitySource Source { get; set; }
    
    /// <summary>
    /// Gets or sets the type of pollution.
    /// </summary>
    [JsonPropertyName("pollution")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public EPollution Pollution { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("color")]
    public string? Color { get; set; }
}