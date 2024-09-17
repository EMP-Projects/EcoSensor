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
    
    private async Task<AirQualityPropertiesDto?> LastMeasureAsync()
    {
        var lastMeasures = await List(new AirQualityPropertiesQuery());
        return lastMeasures.OrderByDescending(x => x.Date).FirstOrDefault();
    }
    
    /// <summary>
    /// Retrieves the date of the last air quality measurement.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the date of the last measurement as a string in the format "yyyy-MM-dd HH:mm:ss".
    /// </returns>
    public async Task<string?> LastDateMeasureAsync()
    {
        var lastMeasure = await LastMeasureAsync();
        // Return the date of the last measurement ISO Formatted
        return lastMeasure?.Date.ToString("O");
    }

    /// <summary>
    /// Checks if the last air quality measurement is older than one hour.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the last measurement is older than one hour.
    /// </returns>
    public async Task<bool> CheckIfLastMeasureIsOlderThanHourAsync(int hours = 1)
    {
        var lastMeasure = await LastMeasureAsync();
        return lastMeasure?.Date < DateTime.UtcNow.AddHours(hours * -1);
    }
    
    /// <summary>
    /// Deletes air quality records older than the specified number of days.
    /// </summary>
    /// <param name="days">The number of days to use as the threshold for deleting old records. Defaults to 3 days.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task<int> DeleteOldRecordsAsync(int days = 3)
    {
        // Get all records
        var oldRecords = await List(new AirQualityPropertiesQuery());
        // Filter records older than the specified number of days
        var recordsToDelete = oldRecords.Where(x => x.Date < DateTime.UtcNow.AddDays(days * -1));

        // Delete the records
        foreach (var dto in recordsToDelete)
            await Delete(dto);

        // Save changes
        return await SaveContext();
    }
}