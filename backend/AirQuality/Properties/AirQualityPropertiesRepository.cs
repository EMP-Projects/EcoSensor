using AutoMapper;
using Gis.Net.Core.Repositories;

namespace EcoSensorApi.AirQuality.Properties;

/// <inheritdoc />
public class AirQualityPropertiesRepository : RepositoryCore<AirQualityPropertiesModel, 
    AirQualityPropertiesDto, 
    AirQualityPropertiesQuery, 
    EcoSensorDbContext>
{

    /// <inheritdoc />
    public AirQualityPropertiesRepository(
        ILogger<AirQualityPropertiesRepository> logger, 
        EcoSensorDbContext context, 
        IMapper mapper) : 
        base(logger, context, mapper)
    {
    }

    /// <inheritdoc />
    protected override IQueryable<AirQualityPropertiesModel> ParseQueryParams(IQueryable<AirQualityPropertiesModel> query, AirQualityPropertiesQuery? queryByParams)
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
        
        if (queryByParams?.TypeMonitoringData is not null)
            query = query.Where(x => x.TypeMonitoringData == (ETypeMonitoringData)queryByParams.TypeMonitoringData);

        if (queryByParams?.EuropeanAqi is not null)
            query = query.Where(x => x.EuropeanAqi >= queryByParams.EuropeanAqi && x.EuropeanAqi <= queryByParams.EuropeanAqi);
        
        if (queryByParams?.UsAqi is not null)
            query = query.Where(x => x.UsAqi >= queryByParams.UsAqi && x.UsAqi <= queryByParams.UsAqi);
        
        return query;
    }
}