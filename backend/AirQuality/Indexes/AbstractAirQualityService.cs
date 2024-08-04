using TeamSviluppo.Auth;
using TeamSviluppo.DTO;
using TeamSviluppo.Entities;
using TeamSviluppo.Repositories;
using TeamSviluppo.Services;
namespace EcoSensorApi.AirQuality.Indexes;

public abstract class AbstractAirQualityService<TDto, TModel, TQuery> : Service<TDto, TModel, TQuery>
    where TDto: DtoBase, IAirQualityLevelDto
    where TModel: ModelBase, IAirQualityLevel
    where TQuery: QueryByParamsBase, IAirQualityLevelQuery, new()
{

    /// <inheritdoc />
    protected AbstractAirQualityService(ILogger logger, IRepository<TDto, TModel, TQuery> repository, IAuthService authService) : base(logger, repository, authService)
    {
    }
}