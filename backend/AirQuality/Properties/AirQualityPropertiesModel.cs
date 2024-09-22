using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcoSensorApi.AirQuality.Vector;
using Gis.Net.Vector.Models;

namespace EcoSensorApi.AirQuality.Properties;

/// <summary>
/// Represents the air quality properties model.
/// </summary>
[Table("air_quality_measures")]
public class AirQualityPropertiesModel : GisManyPropertiesModel<AirQualityVectorModel, AirQualityPropertiesModel>, IAirQualityBase
{
    /// <summary>
    /// Gets or sets the latitude.
    /// </summary>
    [Column("lat")]
    public required double Lat { get; set; }
    
    /// <summary>
    /// Gets or sets the longitude.
    /// </summary>
    [Column("lng")]
    public required double Lng { get; set; }
    
    /// <summary>
    /// Gets or sets the air quality value.
    /// </summary>
    [Column("value")]
    public required double Value { get; set; }
    
    /// <summary>
    /// Gets or sets the unit of the air quality value.
    /// </summary>
    [Column("unit"), MaxLength(10)]
    public required string Unit { get; set; }
    
    /// <summary>
    /// Gets or sets the date of the air quality measurement.
    /// </summary>
    [Column("date")]
    public required DateTime Date { get; set; }
    
    /// <summary>
    /// Gets or sets the European Air Quality Index (AQI).
    /// </summary>
    [Column("european_aqi")]
    public long? EuropeanAqi { get; set; }
    
    /// <summary>
    /// Gets or sets the US Air Quality Index (AQI).
    /// </summary>
    [Column("us_aqi")]
    public long? UsAqi { get; set; }
    
    /// <summary>
    /// Gets or sets the elevation.
    /// </summary>
    [Column("elevation")]
    public double Elevation { get; set; }

    /// <summary>
    /// Gets or sets the source of the air quality data.
    /// </summary>
    [Column("source")]
    public EAirQualitySource Source { get; set; }

    /// <summary>
    /// Gets or sets the type of pollution measured.
    /// </summary>
    [Column("pollution")]
    public EPollution Pollution { get; set; }
    
    /// <summary>
    /// Gets or sets the color associated with the air quality data.
    /// </summary>
    /// <value>The color as a string.</value>
    [Column("color"), MaxLength(20)]
    public string? Color { get; set; }
    
    /// <inheritdoc />
    [Column("type_monitoring_data")]
    public ETypeMonitoringData TypeMonitoringData { get; set; }
}