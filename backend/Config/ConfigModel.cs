using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace EcoSensorApi.Config;

/// <summary>
/// Represents a configuration model.
/// </summary>
[Table("layers")]
public class ConfigModel : ModelBase
{
    [Column("region_name")]
    public string? RegionName { get; set; }
    
    [Column("region_code")]
    public int? RegionCode { get; set; }
    
    [Column("prov_name")]
    public string? ProvName { get; set; }
    
    [Column("prov_code")]
    public int? ProvCode { get; set; }
    
    [Column("city_code")]
    public int? CityCode { get; set; }
    
    [Column("city_name")]
    public string? CityName { get; set; }
}