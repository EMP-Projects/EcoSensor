using TeamSviluppo.Repositories;
namespace EcoSensorApi.AirQuality.Indexes.Eu;

public class EuAirQualityRepository : AbstractAirQualityLevelRepository<EuAirQualityLevelDto, EuAirQualityLevel, EuAirQualityQuery>
{
    /// <inheritdoc />
    public EuAirQualityRepository(
        ILogger<EuAirQualityRepository> logger, 
        EcoSensorDbContext context, 
        RepositoryDependencies repositoryDependencies) : base(logger, context, repositoryDependencies)
    {
    }

    protected override IQueryable<EuAirQualityLevel> ParseQueryParamsDto(IQueryable<EuAirQualityLevel> query, EuAirQualityQuery? queryByParams)
    {
        if (queryByParams?.Level is not null)
            query = query.Where(x => x.Level == queryByParams.Level);
        return base.ParseQueryParamsDto(query, queryByParams);
    }
}