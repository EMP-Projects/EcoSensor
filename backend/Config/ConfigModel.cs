using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gis.Net.Core.Entities;

namespace EcoSensorApi.Config;

/// <summary>
/// Represents a configuration model.
/// </summary>
[Table("layers")]
public class ConfigModel : ModelBase, IConfig
{
    /// <summary>
    /// Represents the type of source layer for configuration.
    /// </summary>
    [Column("type")]
    public ETypeSourceLayer TypeSource { get; set; }

    /// <summary>
    /// Gets or sets the name of the configuration.
    /// </summary>
    /// <remarks>
    /// This property is used to specify the name of a configuration in the EcoSensorApi.
    /// </remarks>
    [Column("name"), MaxLength(255)]
    public required string Name { get; set; }

    /// <summary>
    /// Represents the region code of a configuration.
    /// </summary>
    [Column("region_code")]
    public int RegionCode { get; set; }

    /// <summary>
    /// Gets or sets the city code.
    /// </summary>
    /// <remarks>
    /// This property represents the code of the city associated with the config model.
    /// </remarks>
    /// <value>The city code.</value>
    [Column("city_code")]
    public int? CityCode { get; set; }

    /// <summary>
    /// Represents the distance property.
    /// </summary>
    [Column("distance_mt")]
    public int Distance { get; set; }

    /// <summary>
    /// Represents the matrix distance points property of a configuration model.
    /// </summary>
    /// <remarks>
    /// The matrix distance points property specifies the maximum number of points to consider
    /// when calculating distances using a matrix algorithm.
    /// </remarks>
    [Column("max_distance_points_mt")]
    public int MatrixDistancePoints { get; set; }
}