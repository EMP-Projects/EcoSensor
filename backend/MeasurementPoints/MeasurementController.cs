using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.MeasurementPoints;

/// <inheritdoc />
[ApiController]
[Route("api/v{version:apiVersion}/measurement")]
public class MeasurementController : ControllerBase
{
    private readonly MeasurementPointsService _measurementPointsService;
    private readonly ILogger<MeasurementController> _logger;

    /// <inheritdoc />
    public MeasurementController(MeasurementPointsService measurementPointsService, ILogger<MeasurementController> logger)
    {
        _measurementPointsService = measurementPointsService;
        _logger = logger;
    }
    
    /// <summary>
    /// Retrieves the measurement points for a given city.
    /// </summary>
    /// <param name="city">The name of the city to filter the measurement points.</param>
    /// <returns>An <see cref="IActionResult"/> containing the measurement points or an error message.</returns>
    [HttpGet("{city}")]
    public async Task<IActionResult> GetMeasurementPoints(string? city)
    {
        try
        {
            var result = await _measurementPointsService.AirQualityFeatures(city);
            if (result == null)
                return NotFound("No measurement points found");
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting measurement points");
            return StatusCode(500, "Error while getting measurement points");
        }
    }
}