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
            query = query.Where(x => x.RegionName == queryByParams.RegionName);
        if (queryByParams?.RegionCode is not null)
            query = query.Where(x => x.RegionCode == queryByParams.RegionCode);
        if (queryByParams?.ProvName is not null)
            query = query.Where(x => x.ProvName == queryByParams.ProvName);
        if (queryByParams?.ProvCode is not null)
            query = query.Where(x => x.ProvCode == queryByParams.ProvCode);
        if (queryByParams?.CityCode is not null)
            query = query.Where(x => x.CityCode == queryByParams.CityCode);
        if (queryByParams?.CityName is not null)
            query = query.Where(x => x.CityName == queryByParams.CityName);
        
        return query;
    }
}