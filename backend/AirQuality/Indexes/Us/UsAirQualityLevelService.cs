namespace EcoSensorApi.AirQuality.Indexes.Us;

public class UsAirQualityLevelService : AbstractAirQualityLevelService<UsAirQualityLevel, UsAirQualityLevelDto, UsAirQualityQuery, UsAirQualityLevelRequest>
{
    /// <inheritdoc />
    public UsAirQualityLevelService(ILogger<UsAirQualityLevelService> logger, UsAirQualityLevelRepository levelRepository) : base(logger, levelRepository)
    {
    }
}