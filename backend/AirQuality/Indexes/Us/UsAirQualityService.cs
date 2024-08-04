using TeamSviluppo.Auth;
using TeamSviluppo.Exceptions;
namespace EcoSensorApi.AirQuality.Indexes.Us;

public class UsAirQualityService : AbstractAirQualityService<UsAirQualityLevelDto, UsAirQualityLevel, UsAirQualityQuery>
{
    /// <inheritdoc />
    public UsAirQualityService(ILogger<UsAirQualityService> logger, UsAirQualityRepository repository, IAuthService authService) : base(logger, repository, authService)
    {
    }
    protected override Task ClearExternalRepositoriesCache() => Task.CompletedTask;
    public override Task Validate(UsAirQualityLevelDto dto, CrudEnum crudEnum) => Task.CompletedTask;
}