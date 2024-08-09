using System.ComponentModel.DataAnnotations.Schema;
using TeamSviluppo.Entities;

namespace EcoSensorApi.Config;

[Table("layers")]
public class ConfigModel : ModelBase, IConfig
{
    [Column("type")]
    public ETypeSourceLayer TypeSource { get; set; }
    
    [Column("name")]
    public required string Name { get; set; }
    
    [Column("region_field")]
    public required string RegionField { get; set; }
    
    [Column("region_code")]
    public int RegionCode { get; set; }
    
    [Column("city_field")]
    public string? CityField { get; set; }
    
    [Column("city_code")]
    public int? CityCode { get; set; }
    
    [Column("distance_mt")]
    public int Distance { get; set; }
    
    [Column("max_distance_points_mt")]
    public int MatrixDistancePoints { get; set; }
}