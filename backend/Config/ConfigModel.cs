using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace EcoSensorApi.Config;

/// <summary>
/// Represents a configuration model.
/// </summary>
[Table("layers")]
public class ConfigModel : ModelBase
{
    /// <summary>
    /// Gets or sets the name of the region.
    /// </summary>
    [Column("region_name")]
    public string? RegionName { get; set; }
    
    /// <summary>
    /// Gets or sets the region code.
    /// </summary>
    [Column("region_code")]
    public int? RegionCode { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the province.
    /// </summary>
    [Column("prov_name")]
    public string? ProvName { get; set; }
    
    /// <summary>
    /// Gets or sets the province code.
    /// </summary>
    [Column("prov_code")]
    public int? ProvCode { get; set; }
    
    /// <summary>
    /// Gets or sets the city code.
    /// </summary>
    [Column("city_code")]
    public int? CityCode { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the city.
    /// </summary>
    [Column("city_name")]
    public string? CityName { get; set; }
}