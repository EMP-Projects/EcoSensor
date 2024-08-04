using TeamSviluppo.Repositories;
namespace EcoSensorApi.AirQuality.Indexes.Us;

public class UsAirQualityRepository : AbstractAirQualityLevelRepository<UsAirQualityLevelDto, UsAirQualityLevel, UsAirQualityQuery>
{

    /// <inheritdoc />
    public UsAirQualityRepository(ILogger<UsAirQualityRepository> logger, EcoSensorDbContext context, RepositoryDependencies repositoryDependencies) : base(logger, context, repositoryDependencies)
    {
    }

    protected override IQueryable<UsAirQualityLevel> ParseQueryParamsDto(IQueryable<UsAirQualityLevel> query, UsAirQualityQuery? queryByParams)
    {
        if (queryByParams?.Level is not null)
            query = query.Where(x => x.Level == queryByParams.Level);
        return base.ParseQueryParamsDto(query, queryByParams);
    }
}