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
    [FromQuery(Name = "entityKey")] public string? EntityKey { get; set; }
    
    /// <summary>
    /// Gets or sets the type of monitoring data.
    /// </summary>
    /// <value>The type of monitoring data.</value>
    [FromQuery(Name = "typeMonitoringData")]
    public ETypeMonitoringData TypeMonitoringData { get; set; }
}