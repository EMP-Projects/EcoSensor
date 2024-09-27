using EcoSensorApi.AirQuality;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.MeasurementPoints;

/// <inheritdoc />
[ApiController]
[Route("api/v{version:apiVersion}/measurements/air-quality")]
public class MeasurementAirQualityController : ControllerBase
{
    private readonly MeasurementPointsService _measurementPointsService;
    private readonly ILogger<MeasurementAirQualityController> _logger;

    /// <inheritdoc />
    public MeasurementAirQualityController(MeasurementPointsService measurementPointsService, ILogger<MeasurementAirQualityController> logger)
    {
        _measurementPointsService = measurementPointsService;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves the measurement points based on the provided query parameters.
    /// </summary>
    /// <param name="query">The query parameters for filtering the measurement points.</param>
    /// <param name="pollution"></param>
    /// <returns>An <see cref="IActionResult"/> containing the measurement points or an error message.</returns>
    [HttpGet]
    public async Task<IActionResult> GetMeasurements([FromQuery] MeasurementsQuery query, [FromQuery] EPollution pollution)
    {
        try
        {
            var result = await _measurementPointsService.AirQualityFeatures(query, pollution);
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
    /// <param name="pollution"></param>
    /// <returns>An <see cref="IActionResult"/> containing the next timestamp or an error message.</returns>
    [HttpGet("next-ts")]
    public async Task<IActionResult> GetNextTs([FromQuery] MeasurementsQuery query, EPollution pollution)
    {
        try
        {
            var result = await _measurementPointsService.GetNextTimeStamp(query, pollution);
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