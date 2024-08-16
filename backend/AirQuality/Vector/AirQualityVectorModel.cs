using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcoSensorApi.AirQuality.Properties;
using Gis.Net.Vector.Models;

namespace EcoSensorApi.AirQuality.Vector;

[Table("air_quality")]
public class AirQualityVectorModel : GisCoreManyModel<AirQualityPropertiesModel>, IAirQualityVector
{
    [Column("source_data")]
    [EnumDataType(typeof(ESourceData))]
    public ESourceData SourceData { get; set; }
    
    [Column("lat")]
    public double Lat { get; set; }
    
    [Column("lng")]
    public double Lng { get; set; }

}