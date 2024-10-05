using EcoSensorApi.AirQuality.Indexes.Us;
using Microsoft.Extensions.Caching.Memory;

namespace EcoSensorApi.Cache;

/// <inheritdoc />
public class UsAirQualityLevelCache : EcoSensorCache<UsAirQualityLevelDto>
{
    private readonly UsAirQualityLevelService _usAirQualityLevelService;
    
    /// <inheritdoc />
    public UsAirQualityLevelCache(IMemoryCache memoryCache, UsAirQualityLevelService usAirQualityLevelService) : base(memoryCache)
    {
        _usAirQualityLevelService = usAirQualityLevelService;
    }

    /// <inheritdoc />
    protected override string CacheKey { get; set; } = "UsAirQualityLevel";

    /// <inheritdoc />
    public override async Task CacheInit()
    {
        var items = (await _usAirQualityLevelService.List()).ToList();
        Set(items, null);
    }
}