using Microsoft.EntityFrameworkCore;
using TeamSviluppo.Repositories;

namespace EcoSensorApi.Config;

public class ConfigRepository : Repository<ConfigDto, ConfigModel, ConfigQuery>
{

    /// <inheritdoc />
    public ConfigRepository(ILogger<ConfigRepository> logger, EcoSensorDbContext context, RepositoryDependencies repositoryDependencies) : base(logger, context, repositoryDependencies)
    {
    }

    protected override IQueryable<ConfigModel> ParseQueryParamsDto(IQueryable<ConfigModel> query, ConfigQuery? queryByParams)
    {
        if (queryByParams?.Name is not null)
            query = query.Where(x => x.Name == queryByParams.Name);
        if (queryByParams?.TypeSource is not null)
            query = query.Where(x => x.TypeSource == queryByParams.TypeSource);
        if (queryByParams?.RegionField is not null)
            query = query.Where(x => x.RegionField == queryByParams.RegionField);
        if (queryByParams?.RegionCode is not null)
            query = query.Where(x => x.RegionCode == queryByParams.RegionCode);
        if (queryByParams?.CityField is not null)
            query = query.Where(x => x.CityField == queryByParams.CityField);
        if (queryByParams?.CityCode is not null)
            query = query.Where(x => x.CityCode == queryByParams.CityCode);
        return query;
    }
}