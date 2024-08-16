using AutoMapper;

namespace EcoSensorApi.AirQuality.Indexes.Us;

public class UsAirQualityLevelRepository : AbstractAirQualityLevelRepository<UsAirQualityLevel, UsAirQualityLevelDto, UsAirQualityQuery>
{

    /// <inheritdoc />
    public UsAirQualityLevelRepository(
        ILogger<UsAirQualityLevelRepository> logger, 
        EcoSensorDbContext context, 
        IMapper mapper) : base(logger, context, mapper)
    {
    }

    protected override IQueryable<UsAirQualityLevel> ParseQueryParams(IQueryable<UsAirQualityLevel> query, UsAirQualityQuery? queryByParams)
    {
        if (queryByParams?.Level is not null)
            query = query.Where(x => x.Level == queryByParams.Level);
        return base.ParseQueryParams(query, queryByParams);
    }
}