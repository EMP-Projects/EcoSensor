using System.Globalization;
using TeamSviluppo.Repositories;
namespace EcoSensorApi.AirQuality.Properties;

public class AirQualityPropertiesRepository : Repository<AirQualityPropertiesDto, AirQualityPropertiesModel, AirQualityPropertiesQuery>
{

    /// <inheritdoc />
    public AirQualityPropertiesRepository(
        ILogger<AirQualityPropertiesRepository> logger, 
        EcoSensorDbContext context, 
        RepositoryDependencies repositoryDependencies) : 
        base(logger, context, repositoryDependencies)
    {
    }
    
    protected override IQueryable<AirQualityPropertiesModel> ParseQueryParamsDto(IQueryable<AirQualityPropertiesModel> query, AirQualityPropertiesQuery? queryByParams)
    {
        if (queryByParams?.Pollution is not null)
            query = query.Where(x => x.Pollution == (EPollution)queryByParams.Pollution);
        
        if (queryByParams?.GisId is not null)
            query = query.Where(x => x.GisId == queryByParams.GisId);
        
        if (queryByParams?.Source is not null)
            query = query.Where(x => x.Source == (EAirQualitySource)queryByParams.Source);

        if (queryByParams?.Begin is not null)
            query = query.Where(x => x.Date >= queryByParams.Begin);
        
        if (queryByParams?.End is not null)
            query = query.Where(x => x.Date <= queryByParams.End);
        
        if (queryByParams?.Latitude is not null)
            query = query.Where(x => x.Lat.Equals(queryByParams.Latitude));
        
        if (queryByParams?.Longitude is not null)
            query = query.Where(x => x.Lng.Equals(queryByParams.Longitude));

        return query;
    }
}