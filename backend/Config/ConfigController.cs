using AutoMapper;
using Gis.Net.Controllers;
using Gis.Net.Core.Services;
using Gis.Net.Istat;
using Gis.Net.Istat.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.Config;

/// <inheritdoc />
[ApiController]
[Route("api/v{version:apiVersion}/config")]
public class ConfigController : RootReadOnlyController<ConfigModel, ConfigDto, ConfigQuery, ConfigRequest, EcoSensorDbContext>
{
    private readonly IIStatService<IstatContext> _iStatService;
    
    /// <inheritdoc />
    public ConfigController(ILogger<ConfigController> logger, IConfiguration configuration, IMapper mapper, ConfigService serviceCore, IIStatService<IstatContext> iStatService) : base(logger, configuration, mapper, serviceCore)
    {
        _iStatService = iStatService;
    }
    
    /// <summary>
    /// Retrieves a list of cities based on the provided query parameters.
    /// </summary>
    /// <param name="queryParams">The query parameters for filtering the list of cities.</param>
    /// <returns>An <see cref="IActionResult"/> containing the list of cities or an error message.</returns>
    [HttpGet("cities")]
    public async Task<IActionResult> GetCities([FromQuery] LimitsItMunicipality queryParams)
    {
        try
        {
            var cities = await _iStatService.GetMunicipalities(queryParams);
            return Ok(cities);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error getting cities");
            return StatusCode(500, "Error getting cities");
        }
    }
}