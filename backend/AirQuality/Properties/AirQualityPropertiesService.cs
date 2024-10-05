using EcoSensorApi.Cache;
using EcoSensorApi.MeasurementPoints;
using Gis.Net.Core.Repositories;
using Gis.Net.Core.Services;
using Gis.Net.Vector;
using NetTopologySuite.Geometries;

namespace EcoSensorApi.AirQuality.Properties;

/// <inheritdoc />
public class AirQualityPropertiesService : ServiceCore<AirQualityPropertiesModel, 
    AirQualityPropertiesDto, 
    AirQualityPropertiesQuery, 
    AirQualityPropertiesRequest, 
    EcoSensorDbContext>
{

    private readonly EuAirQualityLevelCache _euAirQualityLevelCache;
    
    /// <inheritdoc />
    public AirQualityPropertiesService(
        ILogger<AirQualityPropertiesService> logger, 
        AirQualityPropertiesRepository propertiesRepository, 
        EuAirQualityLevelCache euAirQualityLevelCache) : 
        base(logger, propertiesRepository)
    {
        _euAirQualityLevelCache = euAirQualityLevelCache;
    }
    
    /// <summary>
    /// Retrieves the date of the last air quality measurement.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the date of the last measurement as a string in the format "yyyy-MM-dd HH:mm:ss".
    /// </returns>
    public async Task<string?> LastDateMeasureAsync(MeasurementsQuery query, EPollution pollution)
    {
        var queryProps = new AirQualityPropertiesQuery { EntityKey = query.EntityKey, TypeMonitoringData = query.TypeMonitoringData, Pollution = pollution};
        // Get the last measures
        var lastMeasures = await List(queryProps);
        // Get the last date measure
        var lastMeasure = lastMeasures.MaxBy(x => x.Date);
        // Return the date of the last measurement ISO Formatted
        return lastMeasure?.Date.ToString("O");
    }
    
    /// <summary>
    /// Retrieves air quality properties from the current time onwards for the specified type of monitoring data.
    /// </summary>
    /// <param name="typeData">The type of monitoring data.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an enumerable of <see cref="AirQualityPropertiesDto"/> objects.
    /// </returns>
    public async Task<IEnumerable<AirQualityPropertiesDto>> GetAirQualityPropertiesFromNowAsync(ETypeMonitoringData typeData)
    {
        return (await List(new AirQualityPropertiesQuery
        {
            TypeMonitoringData = typeData
        })).Where(x => x.Date > DateTime.UtcNow);
    }
    
    /// <summary>
    /// Retrieves air quality points in WGS84 coordinates from the current time onwards for the specified type of monitoring data.
    /// </summary>
    /// <param name="typeData">The type of monitoring data.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an enumerable of geographical points in WGS84 coordinates.
    /// </returns>
    public async Task<IEnumerable<Point>> GetAirQualityPointsFromNowAsync(ETypeMonitoringData typeData)
    {
        return (await GetAirQualityPropertiesFromNowAsync(typeData))
            .Select(x => GisUtility.CreatePoint(3857, new Coordinate(x.Lng, x.Lat)));
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
    /// Retrieves the IDs of air quality properties based on the specified pollution type and query parameters.
    /// </summary>
    /// <param name="query">The query parameters for retrieving air quality properties.</param>
    /// <param name="pollution">The type of pollution to filter the air quality properties.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an array of IDs of the air quality properties that match the specified criteria.
    /// </returns>
    public async Task<long[]> GetListIdByPollution(MeasurementsQuery query, EPollution pollution)
    {
        return (await List(new AirQualityPropertiesQuery { Pollution = pollution, EntityKey = query.EntityKey, TypeMonitoringData = query.TypeMonitoringData })).Select(x => x.Id).ToArray();
    }

    /// <summary>
    /// Creates a list of air quality values based on the provided parameters.
    /// </summary>
    /// <param name="pollution">The type of pollution.</param>
    /// <param name="lat">The latitude of the location.</param>
    /// <param name="lng">The longitude of the location.</param>
    /// <param name="elevation">The elevation of the location.</param>
    /// <param name="gisId">The GIS identifier.</param>
    /// <param name="key"></param>
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
        string key,
        string? unit,
        List<string?>? times,
        List<double?>? values,
        List<long?>? indexAq)
    {
        var result = new List<AirQualityPropertiesDto>();
        if (times is null) return result;
        
        // get the color index for the air quality
        var colorIndexAqList = _euAirQualityLevelCache.GetLevels(pollution);
        
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
            var europeanAqiText = string.Empty;
            if (indexAq?[time.i] is not null)
            {
                // get the color based on the index
                var colorIndexAq = colorIndexAqList.FirstOrDefault(x => x.Min <= indexAq[time.i] && x.Max >= indexAq[time.i]);
                // check if the color index is null
                if (colorIndexAq is not null) color = colorIndexAq.Color;
                europeanAqiText = airQualityPropsList.FirstOrDefault(x => x.EuropeanAqi >= indexAq[time.i] && x.EuropeanAqi <= indexAq[time.i])?.EuropeanAqiText ?? string.Empty;
            }
            
            // create the air quality properties
            result.Add(new AirQualityPropertiesDto
            {
                EntityKey = key,
                TimeStamp = DateTime.UtcNow,
                Date = DateTime.Parse(time.value).ToUniversalTime(),
                PollutionText = Pollution.GetPollutionDescription(pollution),
                TypeMonitoringData = ETypeMonitoringData.AirQuality,
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
                Elevation = elevation,
                UsAqiText = string.Empty,
                EuropeanAqiText = europeanAqiText
            });
        }

        return result;
    }

    /// <inheritdoc />
    protected override ListOptions<AirQualityPropertiesModel, AirQualityPropertiesDto, AirQualityPropertiesQuery>
        GetRowsOptions(AirQualityPropertiesQuery q)
    {
        var options = base.GetRowsOptions(q);
        options.OnExtraMapping = (model, dto) => dto.EuropeanAqiText = _euAirQualityLevelCache.GetLevelName(model.EuropeanAqi);
        return options;
    }
}