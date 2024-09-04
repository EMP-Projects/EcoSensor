using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Repositories;
using Gis.Net.Core.Services;

namespace EcoSensorApi.AirQuality.Indexes;

/// <inheritdoc />
public abstract class AbstractAirQualityLevelService<TModel, TDto, TQuery, TRequest> : 
    ServiceCore<TModel, TDto, TQuery, TRequest, EcoSensorDbContext>
    where TDto: DtoBase, IAirQualityLevelDto
    where TModel: ModelBase, IAirQualityLevel
    where TQuery: QueryBase, IAirQualityLevelQuery, new()
    where TRequest: RequestBase
{

    /// <inheritdoc />
    protected AbstractAirQualityLevelService(ILogger logger, IRepositoryCore<TModel, TDto, TQuery, EcoSensorDbContext> levelRepository) : base(logger, levelRepository)
    {
    }
}