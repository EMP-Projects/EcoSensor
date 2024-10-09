
using EcoSensorApi.Cache;

namespace EcoSensorApi.AirQuality.Indexes.Eu;

/// <inheritdoc />
public class EuAirQualityLevelService : AbstractAirQualityLevelService<EuAirQualityLevel, EuAirQualityLevelDto, EuAirQualityQuery, EuAirQualityLevelRequest>
{

    /// <inheritdoc />
    public EuAirQualityLevelService(ILogger<EuAirQualityLevelService> logger, EuAirQualityLevelRepository levelRepository) : 
        base(logger, levelRepository)
    {
        
    }
    
    /// <summary>
    /// Asynchronously retrieves the name of the air quality level based on the provided value.
    /// </summary>
    /// <param name="value">The value to query the air quality level.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the name of the air quality level if found; otherwise, null.</returns>
    public async Task<string?> GetLevelNameAsync(double? value)
    {
        var level = (await List(new EuAirQualityQuery { Value = value })).FirstOrDefault();
        return level?.LevelName;
    }
    
    
    
}