namespace EcoSensorApi.AirQuality;

/// <summary>
/// Represents the base interface for air quality data.
/// </summary>
public interface IAirQualityBase
{
    /// <summary>
    /// Gets or sets the latitude.
    /// </summary>
    double Lat { get; set; }

    /// <summary>
    /// Gets or sets the longitude.
    /// </summary>
    double Lng { get; set; }

    /// <summary>
    /// Gets or sets the air quality value.
    /// </summary>
    double Value { get; set; }

    /// <summary>
    /// Gets or sets the unit of the air quality value.
    /// </summary>
    string Unit { get; set; }

    /// <summary>
    /// Gets or sets the date of the air quality measurement.
    /// </summary>
    DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the European Air Quality Index (AQI).
    /// </summary>
    long? EuropeanAqi { get; set; }

    /// <summary>
    /// Gets or sets the US Air Quality Index (AQI).
    /// </summary>
    long? UsAqi { get; set; }

    /// <summary>
    /// Gets or sets the elevation.
    /// </summary>
    double Elevation { get; set; }

    /// <summary>
    /// Gets or sets the source of the air quality data.
    /// </summary>
    EAirQualitySource Source { get; set; }

    /// <summary>
    /// Gets or sets the type of pollution measured.
    /// </summary>
    EPollution Pollution { get; set; }
    
    /// <summary>
    /// Gets or sets the color associated with the air quality data.
    /// </summary>
    /// <value>The color as a string.</value>
    string? Color { get; set; }
}