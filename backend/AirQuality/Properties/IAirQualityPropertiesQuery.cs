namespace EcoSensorApi.AirQuality.Properties;

/// <summary>
/// Represents a query for air quality properties.
/// </summary>
public interface IAirQualityPropertiesQuery
{
    /// <summary>
    /// Gets or sets the GIS identifier.
    /// </summary>
    long? GisId { get; set; }

    /// <summary>
    /// Gets or sets the type of pollution.
    /// </summary>
    EPollution? Pollution { get; set; }

    /// <summary>
    /// Gets or sets the start date and time for the query.
    /// </summary>
    DateTime? Begin { get; set; }

    /// <summary>
    /// Gets or sets the end date and time for the query.
    /// </summary>
    DateTime? End { get; set; }

    /// <summary>
    /// Gets or sets the source of the air quality data.
    /// </summary>
    EAirQualitySource? Source { get; set; }

    /// <summary>
    /// Gets or sets the latitude for the query.
    /// </summary>
    double? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude for the query.
    /// </summary>
    double? Longitude { get; set; }
}