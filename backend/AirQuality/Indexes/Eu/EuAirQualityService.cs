using TeamSviluppo.Auth;
using TeamSviluppo.Exceptions;
namespace EcoSensorApi.AirQuality.Indexes.Eu;

public class EuAirQualityService : AbstractAirQualityService<EuAirQualityLevelDto, EuAirQualityLevel, EuAirQualityQuery>
{

    /// <inheritdoc />
    public EuAirQualityService(ILogger<EuAirQualityService> logger, EuAirQualityRepository repository, IAuthService authService) : 
        base(logger, repository, authService)
    {
    }
    protected override Task ClearExternalRepositoriesCache() => Task.CompletedTask;
    public override Task Validate(EuAirQualityLevelDto dto, CrudEnum crudEnum) => Task.CompletedTask;
}