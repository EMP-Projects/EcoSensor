using EcoSensorApi.AirQuality.Properties;
using TeamSviluppo.Gis.NetCoreFw.Repositories;
using TeamSviluppo.Repositories;
using TeamSviluppo.Services;
namespace EcoSensorApi.AirQuality.Vector;

public class AirQualityVectorRepository : 
    GisVectorCoreManyRepository<AirQualityVectorDto, AirQualityVectorModel, AirQualityVectorQuery, AirQualityPropertiesModel, AirQualityPropertiesDto>
{
    /// <inheritdoc />
    public AirQualityVectorRepository(
        ILogger<AirQualityVectorRepository> logger, 
        IAppDbContext context, 
        RepositoryDependencies repositoryDependencies) : 
        base(logger, context.GetDbContext(), repositoryDependencies)
    {
    }

    protected override IQueryable<AirQualityVectorModel> ParseQueryParamsDto(IQueryable<AirQualityVectorModel> query, AirQualityVectorQuery? queryByParams)
    {
        if (queryByParams?.SourceData != null) 
            query = query.Where(x => x.SourceData.Equals(queryByParams.SourceData));
        
        return base.ParseQueryParamsDto(query, queryByParams);
    }
}