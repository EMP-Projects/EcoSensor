using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.Config;

/// <summary>
/// Represents a query object used for retrieving configuration information.
/// </summary>
public class ConfigQuery : QueryBase
{
    /// <summary>
    /// Represents the type of source layer for a configuration.
    /// </summary>
    [FromQuery(Name = "typeSource")]
    public ETypeSourceLayer? TypeSource { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <remarks>
    /// This property represents the name of the object.
    /// </remarks>
    [FromQuery(Name = "name")]
    public string? Name { get; set; }

    /// <summary>
    /// Represents a property for specifying the region field in a <see cref="ConfigQuery"/> object.
    /// </summary>
    [FromQuery(Name = "regionField")]
    public string? RegionField { get; set; }

    /// <summary>
    /// Gets or sets the region code.
    /// </summary>
    /// <remarks>
    /// This property represents the code of the region.
    /// </remarks>
    [FromQuery(Name = "regionCode")]
    public int? RegionCode { get; set; }

    /// <summary>
    /// Represents the city field in the configuration query.
    /// </summary>
    [FromQuery(Name = "cityField")]
    public string? CityField { get; set; }

    /// <summary>
    /// Represents a property that holds the city code.
    /// </summary>
    /// <value>The city code.</value>
    [FromQuery(Name = "cityCode")]
    public int? CityCode { get; set; }
}