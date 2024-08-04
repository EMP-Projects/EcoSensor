using Microsoft.AspNetCore.Mvc;
using TeamSviluppo.Controllers;
namespace EcoSensorApi.AirQuality.Properties;

[ApiController]
[Route("api/v{version:apiVersion}/measures")]
public class AirQualityPropertiesController : RootReadOnlyController<AirQualityPropertiesDto, AirQualityPropertiesModel, AirQualityPropertiesQuery>
{

    /// <inheritdoc />
    public AirQualityPropertiesController(
        ILogger<AirQualityPropertiesController> logger, 
        IConfiguration configuration, 
        AirQualityPropertiesService propertiesService) : base(logger, configuration, propertiesService)
    {
    }
}