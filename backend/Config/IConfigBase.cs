namespace EcoSensorApi.Config;

public interface IConfigBase
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    string? RegionName { get; set; }
    
    /// <summary>
    /// Represents the region code property of a configuration model.
    /// </summary>
    /// <value>
    /// The region code.
    /// </value>
    int? RegionCode { get; set; }
    
    string? ProvName { get; set; }
    int? ProvCode { get; set; }
    
    /// <summary>
    /// Represents the city code for a sensor configuration.
    /// </summary>
    int? CityCode { get; set; }
    string? CityName { get; set; }
}