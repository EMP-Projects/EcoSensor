using System.ComponentModel.DataAnnotations;
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
    [Column("region_name"), MaxLength(100)]
    public required string RegionName { get; init; }
    
    /// <summary>
    /// Gets or sets the region code.
    /// </summary>
    [Column("region_code")]
    public int RegionCode { get; init; }
    
    /// <summary>
    /// Gets or sets the name of the province.
    /// </summary>
    [Column("prov_name"), MaxLength(100)]
    public required string ProvName { get; init; }
    
    /// <summary>
    /// Gets or sets the province code.
    /// </summary>
    [Column("prov_code")]
    public required int ProvCode { get; init; }
    
    /// <summary>
    /// Gets or sets the city code.
    /// </summary>
    [Column("city_code")]
    public required int CityCode { get; init; }
    
    /// <summary>
    /// Gets or sets the name of the city.
    /// </summary>
    [Column("city_name"), MaxLength(150)]
    public required string CityName { get; init; }
    
    /// <summary>
    /// Gets or sets the type of monitoring data.
    /// </summary>
    /// <value>The type of monitoring data.</value>
    [Column("type_monitoring_data")]
    public required ETypeMonitoringData TypeMonitoringData { get; init; }
}