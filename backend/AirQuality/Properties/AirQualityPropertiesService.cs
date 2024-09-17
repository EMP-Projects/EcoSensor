using EcoSensorApi.AirQuality.Indexes.Eu;
using Gis.Net.Core.Services;

namespace EcoSensorApi.AirQuality.Properties;

/// <inheritdoc />
public class AirQualityPropertiesService : ServiceCore<AirQualityPropertiesModel, 
    AirQualityPropertiesDto, 
    AirQualityPropertiesQuery, 
    AirQualityPropertiesRequest, 
    EcoSensorDbContext>
{

    private readonly EuAirQualityLevelService _euAirQualityLevelService;
    
    /// <inheritdoc />
    public AirQualityPropertiesService(
        ILogger<AirQualityPropertiesService> logger, 
        AirQualityPropertiesRepository propertiesRepository, 
        EuAirQualityLevelService euAirQualityLevelService) : 
        base(logger, propertiesRepository)
    {
        _euAirQualityLevelService = euAirQualityLevelService;
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
    
    /// <summary>
    /// Creates a list of air quality values based on the provided parameters.
    /// </summary>
    /// <param name="pollution">The type of pollution.</param>
    /// <param name="lat">The latitude of the location.</param>
    /// <param name="lng">The longitude of the location.</param>
    /// <param name="elevation">The elevation of the location.</param>
    /// <param name="gisId">The GIS identifier.</param>
    /// <param name="unit">The unit of measurement.</param>
    /// <param name="times">The list of times for the measurements.</param>
    /// <param name="values">The list of values for the measurements.</param>
    /// <param name="indexAq">The list of air quality indices.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="AirQualityPropertiesDto"/> objects.</returns>
    public async Task<List<AirQualityPropertiesDto>> CreateListAirQualityValues(
        EPollution pollution,
        double lat,
        double lng,
        double elevation,
        long gisId,
        string? unit,
        List<string?>? times,
        List<double?>? values,
        List<long?>? indexAq)
    {
        var result = new List<AirQualityPropertiesDto>();
        if (times is null) return result;
        
        // get the color index for the air quality
        var colorIndexAqList = await _euAirQualityLevelService.List(new EuAirQualityQuery
        {
            Pollution = pollution
        });
        
        // get the air quality properties
        var airQualityPropsList = await List(new AirQualityPropertiesQuery
        {
            Pollution = pollution,
            GisId = gisId,
            Source = EAirQualitySource.OpenMeteo,
            Longitude = lng,
            Latitude = lat
        });
        
        // for each time, I create the air quality properties
        foreach (var time in times.Select((value, i) => new { i, value}))
        {
            // check if the values are null
            if (time.value is null) continue;
            if (values?[time.i] is null) continue;
            if (unit is null) continue;
            
            // get the air quality properties
            var airQualityProps = airQualityPropsList.FirstOrDefault(x => x.Date.Equals(DateTime.Parse(time.value).ToUniversalTime()));
            // check if the air quality properties already exist
            if (airQualityProps != null) continue;

            // get the color index for the air quality
            var color = string.Empty;
            if (indexAq?[time.i] is not null)
            {
                // get the color based on the index
                var colorIndexAq = colorIndexAqList.FirstOrDefault(x => x.Min <= indexAq[time.i] && x.Max >= indexAq[time.i]);
                // check if the color index is null
                if (colorIndexAq is not null) color = colorIndexAq.Color;
            }
            
            // create the air quality properties
            result.Add(new AirQualityPropertiesDto
            {
                EntityKey = $"{Pollution.GetPollutionSource(EAirQualitySource.OpenMeteo)}:{time.i}",
                TimeStamp = DateTime.UtcNow,
                Date = DateTime.Parse(time.value).ToUniversalTime(),
                PollutionText = Pollution.GetPollutionDescription(pollution),
                Pollution = pollution,
                Unit = unit,
                Color = color,
                Value = values[time.i]!.Value,
                EuropeanAqi = indexAq?[time.i],
                Lat = lat,
                Lng = lng,
                GisId = gisId,
                SourceText = Pollution.GetPollutionSource(EAirQualitySource.OpenMeteo),
                Source = EAirQualitySource.OpenMeteo,
                Elevation = elevation
            });
        }

        return result;
    }
}