using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.MeasurementPoints;

/// <summary>
/// Represents a query for measurements, including optional filtering by city.
/// </summary>
public class MeasurementsQuery
{
    /// <summary>
    /// Gets or sets the city to filter the measurements.
    /// </summary>
    [FromQuery(Name = "place")] public string? Place { get; set; }
}