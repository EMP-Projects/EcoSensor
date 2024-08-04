using TeamSviluppo.Auth;
using TeamSviluppo.Exceptions;
using TeamSviluppo.Services;
namespace EcoSensorApi.AirQuality.Properties;

public class AirQualityPropertiesService : Service<AirQualityPropertiesDto, AirQualityPropertiesModel, AirQualityPropertiesQuery>
{

    /// <inheritdoc />
    public AirQualityPropertiesService(
        ILogger<AirQualityPropertiesService> logger, 
        AirQualityPropertiesRepository propertiesRepository, 
        IAuthService authService) : 
        base(logger, propertiesRepository, authService)
    {
    }
    
    protected override Task ClearExternalRepositoriesCache() => Task.CompletedTask;
    public override Task Validate(AirQualityPropertiesDto propertiesDto, CrudEnum crudEnum) => Task.CompletedTask;
}