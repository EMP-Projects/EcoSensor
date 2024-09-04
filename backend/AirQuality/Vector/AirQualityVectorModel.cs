using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcoSensorApi.AirQuality.Properties;
using Gis.Net.Vector.Models;

namespace EcoSensorApi.AirQuality.Vector;

/// <summary>
/// Represents the air quality vector model.
/// </summary>
[Table("air_quality")]
public class AirQualityVectorModel : GisCoreManyModel<AirQualityPropertiesModel>, IAirQualityVector
{
    /// <summary>
    /// Gets or sets the source data type.
    /// </summary>
    [Column("source_data")]
    [EnumDataType(typeof(ESourceData))]
    public ESourceData SourceData { get; set; }
    
    /// <summary>
    /// Gets or sets the latitude.
    /// </summary>
    [Column("lat")]
    public double Lat { get; set; }
    
    /// <summary>
    /// Gets or sets the longitude.
    /// </summary>
    [Column("lng")]
    public double Lng { get; set; }
}