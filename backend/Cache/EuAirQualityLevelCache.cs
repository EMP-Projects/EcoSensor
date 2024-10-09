using EcoSensorApi.AirQuality;
using EcoSensorApi.AirQuality.Indexes.Eu;
using Microsoft.Extensions.Caching.Memory;

namespace EcoSensorApi.Cache;

/// <inheritdoc />
public class EuAirQualityLevelCache : EcoSensorCache<EuAirQualityLevelDto>
{
    private readonly EuAirQualityLevelService _euAirQualityLevelService;
    
    /// <inheritdoc />
    public EuAirQualityLevelCache(IMemoryCache memoryCache, EuAirQualityLevelService euAirQualityLevelService) : base(memoryCache)
    {
        _euAirQualityLevelService = euAirQualityLevelService;
    }

    /// <inheritdoc />
    protected override string CacheKey { get; set; } = "EuAirQualityLevel";

    /// <inheritdoc />
    public override async Task CacheInit()
    {
        var items = (await _euAirQualityLevelService.List()).ToList();
        Set(items, null);
    }
    
    /// <summary>
    /// Asynchronously retrieves the name of the air quality level from the cache based on the provided value.
    /// </summary>
    /// <param name="value">The value to query the air quality level.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the name of the air quality level if found; otherwise, null.</returns>
    public string? GetLevelName(double? value)
    {
        var items = Get();
        var level = items.FirstOrDefault(x => x.Min <= value && x.Max >= value);
        return level?.LevelName;
    }
    
    /// <summary>
    /// Retrieves a list of European air quality levels from the cache based on the specified pollution type.
    /// </summary>
    /// <param name="pollution">The type of pollution to filter the air quality levels.</param>
    /// <returns>A list of European air quality level DTOs that match the specified pollution type.</returns>
    public List<EuAirQualityLevelDto> GetLevels(EPollution pollution)
    {
        var colorIndexAqList = Get();
        return colorIndexAqList.Where(x => x.Pollution == pollution).ToList();
    }
}