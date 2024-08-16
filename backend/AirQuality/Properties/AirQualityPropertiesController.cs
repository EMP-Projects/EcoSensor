using AutoMapper;
using Gis.Net.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.AirQuality.Properties;

[ApiController]
[Route("api/v{version:apiVersion}/measures")]
public class AirQualityPropertiesController : RootReadOnlyController<AirQualityPropertiesModel, AirQualityPropertiesDto, AirQualityPropertiesQuery, AirQualityPropertiesRequest, EcoSensorDbContext>
{

    /// <inheritdoc />
    public AirQualityPropertiesController(
        ILogger<AirQualityPropertiesController> logger, 
        IMapper mapper, 
        IConfiguration configuration, 
        AirQualityPropertiesService propertiesService) : base(logger, configuration, mapper, propertiesService)
    {
    }
}