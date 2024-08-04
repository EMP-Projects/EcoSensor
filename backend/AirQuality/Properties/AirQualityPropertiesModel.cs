using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcoSensorApi.AirQuality.Vector;
using TeamSviluppo.Gis.NetCoreFw.Models;
namespace EcoSensorApi.AirQuality.Properties;

[Table("air_quality_measures")]
public class AirQualityPropertiesModel : GisManyPropertiesModel<AirQualityVectorModel, AirQualityPropertiesModel>, IAirQualityPropertiesModel
{
    [Column("lat")]
    public required double Lat { get; set; }
    
    [Column("lng")]
    public required double Lng { get; set; }
    
    [Column("value")]
    public required double Value { get; set; }
    
    [Column("unit"), MaxLength(10)]
    public required string Unit { get; set; }
    
    [Column("date")]
    public required DateTime Date { get; set; }
    
    [Column("european_aqi")]
    public long? EuropeanAqi { get; set; }
    
    [Column("us_aqi")]
    public long? UsAqi { get; set; }
    
    [Column("elevation")]
    public double Elevation { get; set; }

    [Column("source")]
    public EAirQualitySource Source { get; set; }

    [Column("pollution")]
    public EPollution Pollution { get; set; }
}