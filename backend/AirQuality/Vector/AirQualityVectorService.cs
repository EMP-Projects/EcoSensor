using EcoSensorApi.AirQuality.Properties;
using NetTopologySuite.Features;
using TeamSviluppo.Auth;
using TeamSviluppo.Gis.NetCoreFw.Services;
using TeamSviluppo.OpenMeteo.AirQuality;
namespace EcoSensorApi.AirQuality.Vector;

public class AirQualityVectorService : 
    GisVectorCoreManyService<AirQualityVectorDto, AirQualityVectorModel, AirQualityVectorQuery, AirQualityPropertiesModel, AirQualityPropertiesDto>
{
    /// <inheritdoc />
    public AirQualityVectorService(
        ILogger<AirQualityVectorService> logger, 
        AirQualityVectorRepository netCoreRepository, 
        IAuthService authService) : 
        base(logger, netCoreRepository, authService)
    {
        
    }

    protected override Task<Feature> OnLoadProperties(Feature feature, AirQualityVectorDto dto)
    {
        feature.Attributes.Add(NameProperties, new AirQualityLatLng(dto.Lat, dto.Lng));
        return Task.FromResult(feature);
    }

    protected override Task<long[]?> QueryParamsByProperties(AirQualityVectorQuery query) => Task.FromResult<long[]?>(null);
    
}