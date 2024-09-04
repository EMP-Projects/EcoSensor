namespace EcoSensorApi.Config;

/// <summary>
/// Represents the base configuration interface for the EcoSensor API.
/// </summary>
public interface IConfigBase
{
    /// <summary>
    /// Gets or sets the name of the region.
    /// </summary>
    /// <value>The name of the region.</value>
    string? RegionName { get; set; }
    
    /// <summary>
    /// Gets or sets the region code.
    /// </summary>
    /// <value>The region code.</value>
    int? RegionCode { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the province.
    /// </summary>
    /// <value>The name of the province.</value>
    string? ProvName { get; set; }
    
    /// <summary>
    /// Gets or sets the province code.
    /// </summary>
    /// <value>The province code.</value>
    int? ProvCode { get; set; }
    
    /// <summary>
    /// Gets or sets the city code for a sensor configuration.
    /// </summary>
    /// <value>The city code.</value>
    int? CityCode { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the city.
    /// </summary>
    /// <value>The name of the city.</value>
    string? CityName { get; set; }
}