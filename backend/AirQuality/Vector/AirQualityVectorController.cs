using EcoSensorApi.AirQuality.Properties;
using Microsoft.AspNetCore.Mvc;
using TeamSviluppo.Gis.NetCoreFw.Controllers;
namespace EcoSensorApi.AirQuality.Vector;

[ApiController]
[Route("api/v{version:apiVersion}/airQuality")]
public class AirQualityVectorController : GisCoreManyController<AirQualityVectorDto, AirQualityVectorModel, AirQualityVectorQuery, AirQualityPropertiesDto, AirQualityPropertiesModel>
{
    /// <inheritdoc />
    public AirQualityVectorController(
        ILogger<AirQualityVectorController> logger, 
        IConfiguration configuration, 
        AirQualityVectorService service) : base(logger, configuration, service)
    {
    }
}