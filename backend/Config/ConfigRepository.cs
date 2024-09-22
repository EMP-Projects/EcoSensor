using AutoMapper;
using Gis.Net.Core.Repositories;

namespace EcoSensorApi.Config;

/// <inheritdoc />
public class ConfigRepository : RepositoryCore<ConfigModel, ConfigDto, ConfigQuery, EcoSensorDbContext>
{

    /// <inheritdoc />
    public ConfigRepository(ILogger<ConfigRepository> logger, EcoSensorDbContext context, IMapper mapper) : 
        base(logger, context, mapper)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<ConfigModel> ParseQueryParams(IQueryable<ConfigModel> query, ConfigQuery? queryByParams)
    {
        if (queryByParams?.RegionName is not null)
            query = query.Where(x => x.RegionName.ToUpper() == queryByParams.RegionName.ToUpper());
        if (queryByParams?.RegionCode is not null)
            query = query.Where(x => x.RegionCode == queryByParams.RegionCode);
        if (queryByParams?.ProvName is not null)
            query = query.Where(x => x.ProvName.ToUpper() == queryByParams.ProvName.ToUpper());
        if (queryByParams?.ProvCode is not null)
            query = query.Where(x => x.ProvCode == queryByParams.ProvCode);
        if (queryByParams?.CityCode is not null)
            query = query.Where(x => x.CityCode == queryByParams.CityCode);
        if (queryByParams?.CityName is not null)
            query = query.Where(x => x.CityName.ToUpper() == queryByParams.CityName.ToUpper());
        if (queryByParams?.TypeMonitoringData is not null)
            query = query.Where(x => x.TypeMonitoringData == queryByParams.TypeMonitoringData);
        
        return query;
    }
}