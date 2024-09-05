using AutoMapper;
using Gis.Net.Controllers;
using Gis.Net.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.Config;

/// <inheritdoc />
[ApiController]
[Route("api/v{version:apiVersion}/config")]
public class ConfigController : RootReadOnlyController<ConfigModel, ConfigDto, ConfigQuery, ConfigRequest, EcoSensorDbContext>
{
    /// <inheritdoc />
    public ConfigController(ILogger<ConfigController> logger, IConfiguration configuration, IMapper mapper, IServiceCore<ConfigModel, ConfigDto, ConfigQuery, ConfigRequest, EcoSensorDbContext> serviceCore) : base(logger, configuration, mapper, serviceCore)
    {
    }
}