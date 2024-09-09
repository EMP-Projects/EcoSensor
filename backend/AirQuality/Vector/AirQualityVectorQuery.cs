using Gis.Net.Vector.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.AirQuality.Vector;

/// <summary>
/// Represents a query for air quality vector data.
/// </summary>
public class AirQualityVectorQuery : GisVectorQuery
{
    /// <summary>
    /// Gets or sets the source data type.
    /// </summary>
    /// <value>The source data type.</value>
    [FromQuery(Name = "sourceData")]
    public ESourceData SourceData { get; set; }
    
    /// <summary>
    /// Gets or sets the entity vector ID.
    /// </summary>
    /// <value>The entity vector ID.</value>
    [FromQuery(Name = "entityVectorId")]
    public long? EntityVectorId { get; set; }
}