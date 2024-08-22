using AutoMapper;
using EcoSensorApi.AirQuality.Properties;
using Gis.Net.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.AirQuality.Vector;

[ApiController]
[Route("api/v{version:apiVersion}/airQuality")]
public class AirQualityVectorController : GisRootVectorManyController<AirQualityVectorModel, 
    AirQualityVectorDto, 
    AirQualityVectorQuery, 
    AirQualityVectorRequest, 
    EcoSensorDbContext, 
    AirQualityPropertiesModel, 
    AirQualityPropertiesDto>
{
    
    public AirQualityVectorController(
        ILogger<AirQualityVectorController> logger, 
        IConfiguration configuration, 
        IMapper mapper,
        AirQualityVectorService service) : base(logger, configuration, mapper, service)
    {
    }
}