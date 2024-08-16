using AutoMapper;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Repositories;

namespace EcoSensorApi.AirQuality.Indexes;

public abstract class AbstractAirQualityLevelRepository<TModel, TDto, TQuery> : 
    RepositoryCore<TModel, TDto, TQuery, EcoSensorDbContext>
where TDto: DtoBase, IAirQualityLevelDto
where TModel: ModelBase, IAirQualityLevel
where TQuery: QueryBase, IAirQualityLevelQuery
{

    /// <inheritdoc />
    protected AbstractAirQualityLevelRepository(
        ILogger logger, 
        EcoSensorDbContext context, 
        IMapper mapper) : base(logger, context, mapper)
    {
    }

    protected override IQueryable<TModel> ParseQueryParams(IQueryable<TModel> query, TQuery? queryByParams)
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