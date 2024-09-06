using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.AirQuality.Indexes.Eu;

/// <summary>
/// Represents a query for European air quality levels.
/// </summary>
public class EuAirQualityQuery : QueryBase, IAirQualityLevelQuery
{
    /// <summary>
    /// Gets or sets the type of pollution measured.
    /// </summary>
    [FromQuery(Name="pollution")]
    public EPollution? Pollution { get; set; }
    
    /// <summary>
    /// Gets or sets the air quality level.
    /// </summary>
    [FromQuery(Name="level")]
    public EEuAirQualityLevel? Level { get; set; }
    
    /// <summary>
    /// Gets or sets the value of the air quality measurement.
    /// </summary>
    [FromQuery(Name="value")]
    public double? Value { get; set; }
}