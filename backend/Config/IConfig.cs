namespace EcoSensorApi.Config;

/// <summary>
/// Represents the configuration for a sensor.
/// </summary>
public interface IConfig
{
    /// <summary>
    /// Represents the type of source layer.
    /// </summary>
    ETypeSourceLayer TypeSource { get; set; }
    
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    string Name { get; set; }
    
    /// <summary>
    /// Represents the region code property of a configuration model.
    /// </summary>
    /// <value>
    /// The region code.
    /// </value>
    int RegionCode { get; set; }
    
    /// <summary>
    /// Represents the city code for a sensor configuration.
    /// </summary>
    int? CityCode { get; set; }
    
    /// <summary>
    /// Represents a distance value.
    /// </summary>
    int Distance { get; set; }
    
    /// <summary>
    /// Represents the matrix distance points property in the configuration.
    /// </summary>
    int MatrixDistancePoints { get; set; }
}