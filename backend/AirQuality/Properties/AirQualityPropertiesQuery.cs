using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.AirQuality.Properties;

/// <summary>
/// Represents a query for air quality properties.
/// </summary>
public class AirQualityPropertiesQuery : QueryBase, IAirQualityPropertiesQuery
{
    /// <summary>
    /// Gets or sets the type of pollution measured.
    /// </summary>
    [FromQuery(Name="pollution")]
    public EPollution? Pollution { get; set; }
    
    /// <summary>
    /// Gets or sets the source of the air quality data.
    /// </summary>
    [FromQuery(Name="source")]
    public EAirQualitySource? Source { get; set; }
    
    /// <summary>
    /// Gets or sets the latitude for the query.
    /// </summary>
    [FromQuery(Name="latitude")]
    public double? Latitude { get; set; }
    
    /// <summary>
    /// Gets or sets the longitude for the query.
    /// </summary>
    [FromQuery(Name="longitude")]
    public double? Longitude { get; set; }

    /// <summary>
    /// Gets or sets the GIS ID for the query.
    /// </summary>
    [FromQuery(Name="gisId")]
    public long? GisId { get; set; }
    
    /// <summary>
    /// Gets or sets the start date for the query.
    /// </summary>
    [FromQuery(Name="begin")]
    public DateTime? Begin { get; set; }
    
    /// <summary>
    /// Gets or sets the end date for the query.
    /// </summary>
    [FromQuery(Name="end")]
    public DateTime? End { get; set; }
}