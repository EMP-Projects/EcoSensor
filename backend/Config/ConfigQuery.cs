using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.Config;

/// <summary>
/// Represents a query object used for retrieving configuration information.
/// </summary>
public class ConfigQuery : QueryBase, IConfigBase
{
    /// <inheritdoc />
    [FromQuery(Name = "regionName")]
    public string? RegionName { get; set; }

    /// <inheritdoc />
    [FromQuery(Name = "regionCode")]
    public int? RegionCode { get; set; }

    /// <inheritdoc />
    [FromQuery(Name = "provName")]
    public string? ProvName { get; set; }

    /// <inheritdoc />
    [FromQuery(Name = "provCode")]
    public int? ProvCode { get; set; }

    /// <inheritdoc />
    [FromQuery(Name = "cityCode")]
    public int? CityCode { get; set; }

    /// <inheritdoc />
    [FromQuery(Name = "cityName")]
    public string? CityName { get; set; }
}