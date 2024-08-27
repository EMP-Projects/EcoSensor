namespace EcoSensorApi.Config;

/// <summary>
/// Represents the configuration for a sensor.
/// </summary>
public interface IConfig : IConfigBase
{
    /// <summary>
    /// Represents a distance value.
    /// </summary>
    int Distance { get; set; }
    
    /// <summary>
    /// Represents the matrix distance points property in the configuration.
    /// </summary>
    int MatrixDistancePoints { get; set; }
}