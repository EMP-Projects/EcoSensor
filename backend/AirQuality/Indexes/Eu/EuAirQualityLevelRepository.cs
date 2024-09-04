using AutoMapper;

namespace EcoSensorApi.AirQuality.Indexes.Eu;

/// <inheritdoc />
public class EuAirQualityLevelRepository : AbstractAirQualityLevelRepository<EuAirQualityLevel, EuAirQualityLevelDto, EuAirQualityQuery>
{
    /// <inheritdoc />
    public EuAirQualityLevelRepository(
        ILogger<EuAirQualityLevelRepository> logger, 
        EcoSensorDbContext context, 
        IMapper mapper) : base(logger, context, mapper)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<EuAirQualityLevel> ParseQueryParams(IQueryable<EuAirQualityLevel> query, EuAirQualityQuery? queryByParams)
    {
        if (queryByParams?.Level is not null)
            query = query.Where(x => x.Level == queryByParams.Level);
        return base.ParseQueryParams(query, queryByParams);
    }
}