using EcoSensorApi.AirQuality.Properties;
using Gis.Net.OpenMeteo.AirQuality;
using Gis.Net.Vector.Services;
using NetTopologySuite.Features;

namespace EcoSensorApi.AirQuality.Vector;

/// <inheritdoc />
public class AirQualityVectorService : 
    GisVectorCoreManyService<AirQualityVectorModel, 
        AirQualityVectorDto, 
        AirQualityVectorQuery, 
        AirQualityVectorRequest, 
        EcoSensorDbContext, 
        AirQualityPropertiesModel, 
        AirQualityPropertiesDto>
{
    /// <inheritdoc />
    public AirQualityVectorService(
        ILogger<AirQualityVectorService> logger, 
        AirQualityVectorRepository netCoreRepository) : 
        base(logger, netCoreRepository)
    {
        
    }

    /// <inheritdoc />
    protected override Task<Feature> OnLoadProperties(Feature feature, AirQualityVectorDto dto)
    {
        feature.Attributes.Add(NameProperties, new AirQualityLatLng(dto.Lat, dto.Lng));
        return Task.FromResult(feature);
    }

    /// <inheritdoc />
    protected override Task<long[]?> QueryParamsByProperties(AirQualityVectorQuery query) => Task.FromResult<long[]?>(null);
    
}