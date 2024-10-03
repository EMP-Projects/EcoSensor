
namespace EcoSensorApi.AirQuality.Indexes.Eu;

/// <inheritdoc />
public class EuAirQualityLevelService : AbstractAirQualityLevelService<EuAirQualityLevel, EuAirQualityLevelDto, EuAirQualityQuery, EuAirQualityLevelRequest>
{

    /// <inheritdoc />
    public EuAirQualityLevelService(ILogger<EuAirQualityLevelService> logger, EuAirQualityLevelRepository levelRepository) : 
        base(logger, levelRepository)
    {
    }
    
    public async Task<string?> GetLevelNameAsync(double? value)
    {
        var level = (await List(new EuAirQualityQuery { Value = value })).FirstOrDefault();
        return level?.LevelName;
    }
    
}