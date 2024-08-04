using TeamSviluppo.DTO;
using TeamSviluppo.Entities;
using TeamSviluppo.Repositories;
namespace EcoSensorApi.AirQuality.Indexes;

public abstract class AbstractAirQualityLevelRepository<TDto, TModel, TQuery> : Repository<TDto, TModel, TQuery>
where TDto: DtoBase, IAirQualityLevelDto
where TModel: ModelBase, IAirQualityLevel
where TQuery: QueryByParamsBase, IAirQualityLevelQuery
{

    /// <inheritdoc />
    protected AbstractAirQualityLevelRepository(
        ILogger logger, 
        EcoSensorDbContext context, 
        RepositoryDependencies repositoryDependencies) : base(logger, context, repositoryDependencies)
    {
    }

    protected override IQueryable<TModel> ParseQueryParamsDto(IQueryable<TModel> query, TQuery? queryByParams)
    {
        if (queryByParams?.Pollution is not null)
            query = query.Where(x => x.Pollution == queryByParams.Pollution);
        
        if (queryByParams?.Value is not null)
            query = query
                .Where(x => x.Min >= queryByParams.Value)
                .Where(x => x.Max <= queryByParams.Value);
        
        return query;
    }
}