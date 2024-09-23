using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.MeasurementPoints;

/// <inheritdoc />
[ApiController]
[Route("api/v{version:apiVersion}/measurements")]
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
    /// Retrieves the measurement points based on the provided query parameters.
    /// </summary>
    /// <param name="query">The query parameters for filtering the measurement points.</param>
    /// <returns>An <see cref="IActionResult"/> containing the measurement points or an error message.</returns>
    [HttpGet]
    public async Task<IActionResult> GetMeasurements([FromQuery] MeasurementsQuery query)
    {
        try
        {
            var result = await _measurementPointsService.AirQualityFeatures(query);
            if (result == null)
                return NotFound("No measurement points found");
            return Ok(result);
        }
        catch (Exception e)
        {
            const string msg = "Error while getting measurement points";
            _logger.LogError(e, msg);
            return StatusCode(500, msg);
        }
    }
    
    /// <summary>
    /// Retrieves the next timestamp based on the provided measurements query.
    /// </summary>
    /// <param name="query">The query parameters used to determine the next timestamp.</param>
    /// <returns>An <see cref="IActionResult"/> containing the next timestamp or an error message.</returns>
    [HttpGet("next-ts")]
    public async Task<IActionResult> GetNextTs([FromQuery] MeasurementsQuery query)
    {
        try
        {
            var result = await _measurementPointsService.GetNextTimeStamp(query);
            return Ok(result);
        }
        catch (Exception e)
        {
            const string msg = "Error while getting the next timestamp";
            _logger.LogError(e, msg);
            return StatusCode(500, msg);
        }
    }
}