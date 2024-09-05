using EcoSensorApi.AirQuality.Vector;
using Gis.Net.Core.Services;

namespace EcoSensorApi.AirQuality.Properties;

/// <inheritdoc />
public class AirQualityPropertiesService : ServiceCore<AirQualityPropertiesModel, 
    AirQualityPropertiesDto, 
    AirQualityPropertiesQuery, 
    AirQualityPropertiesRequest, 
    EcoSensorDbContext>
{

    /// <inheritdoc />
    public AirQualityPropertiesService(
        ILogger<AirQualityPropertiesService> logger, 
        AirQualityPropertiesRepository propertiesRepository) : 
        base(logger, propertiesRepository)
    {
    }
    
    /// <summary>
    /// Checks if the last air quality measurement is older than one hour.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the last measurement is older than one hour.
    /// </returns>
    public async Task<bool> CheckIfLastMeasureIsOlderThanHourAsync(int hours = 1)
    {
        var lastMeasures = await List(new AirQualityPropertiesQuery());
        var lastMeasure = lastMeasures.OrderByDescending(x => x.Date).FirstOrDefault();
        return lastMeasure?.Date < DateTime.UtcNow.AddHours(hours * -1);
    }
}