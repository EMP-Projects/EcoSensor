namespace EcoSensorApi.AirQuality.Vector;

/// <summary>
/// Represents an interface for air quality vector data.
/// </summary>
public interface IAirQualityVector
{
    /// <summary>
    /// Gets or sets the source data type.
    /// </summary>
    ESourceData SourceData { get; set; }

    /// <summary>
    /// Gets or sets the latitude.
    /// </summary>
    double Lat { get; set; }

    /// <summary>
    /// Gets or sets the longitude.
    /// </summary>
    double Lng { get; set; }
    
    /// <summary>
    /// Gets or sets the entity vector ID.
    /// </summary>
    long EntityVectorId { get; set; }
}