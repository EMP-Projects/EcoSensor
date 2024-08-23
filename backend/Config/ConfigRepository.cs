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
        if (queryByParams?.Name is not null)
            query = query.Where(x => x.Name == queryByParams.Name);
        if (queryByParams?.TypeSource is not null)
            query = query.Where(x => x.TypeSource == queryByParams.TypeSource);
        if (queryByParams?.RegionCode is not null)
            query = query.Where(x => x.RegionCode == queryByParams.RegionCode);
        if (queryByParams?.CityCode is not null)
            query = query.Where(x => x.CityCode == queryByParams.CityCode);
        
        return query;
    }
}