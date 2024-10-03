using System.Text.Json.Serialization;
using EcoSensorApi.AirQuality.Vector;
using Gis.Net.Core.DTO;
using Gis.Net.Vector.DTO;

namespace EcoSensorApi.AirQuality.Properties;

/// <summary>
/// Represents a data transfer object for air quality properties.
/// </summary>
/// <inheritdoc />
public class AirQualityPropertiesDto : DtoBase, IAirQualityPropertiesDto, IGisPropertiesDto<AirQualityVectorDto, AirQualityPropertiesDto>
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
    /// Gets or sets the elevation at which the air quality measurement was taken.
    /// </summary>
    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    /// <summary>
    /// Gets or sets the European Air Quality Index (AQI).
    /// </summary>
    [JsonPropertyName("europeanAqi")]
    public long? EuropeanAqi { get; set; }
    
    /// <summary>
    /// Gets or sets the USA Air Quality Index (AQI).
    /// </summary>
    [JsonPropertyName("usAqi")]
    public long? UsAqi { get; set; }
    
    /// <summary>
    /// Gets or sets the text description of the pollution.
    /// </summary>
    [JsonPropertyName("pollutionText"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PollutionText { get; set; }
    
    /// <summary>
    /// Gets or sets the text description of the source.
    /// </summary>
    [JsonPropertyName("sourceText"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SourceText { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("europeanAqiText"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EuropeanAqiText { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("usAqiText"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UsAqiText { get; set; }

    /// <summary>
    /// Gets or sets the source of the air quality data.
    /// </summary>
    [JsonPropertyName("source")]
    public EAirQualitySource Source { get; set; }

    /// <summary>
    /// Gets or sets the type of pollution.
    /// </summary>
    [JsonPropertyName("pollution")]
    public EPollution Pollution { get; set; }
    
    /// <summary>
    /// Gets or sets the GIS identifier.
    /// </summary>
    [JsonPropertyName("gisId")]
    public long GisId { get; set; }
    
    /// <summary>
    /// Gets or sets the GIS vector data.
    /// </summary>
    [JsonPropertyName("gis"), JsonIgnore]
    public AirQualityVectorDto? Gis { get; set; }
    
    /// <summary>
    /// Gets or sets the color associated with the air quality data.
    /// </summary>
    /// <value>The color as a string.</value>
    [JsonPropertyName("color")]
    public string? Color { get; set; }
    
    /// <inheritdoc />
    [JsonPropertyName("typeMonitoringData")]
    public ETypeMonitoringData TypeMonitoringData { get; set; }
    
    
}