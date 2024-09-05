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
    public override string? NameProperties { get; set; } = "AirQuality";

    /// <inheritdoc />
    protected override Task<Feature> OnLoadProperties(Feature feature, AirQualityVectorDto dto)
    {
        // Load the properties of the feature from the DTO object and add them to the feature attributes collection.
        var properties = dto.PropertiesCollection?.Where(x => x.Date >= DateTime.UtcNow).ToList();
        if (properties != null && properties.Count != 0)
            feature.Attributes.Add(NameProperties, properties);
        return Task.FromResult(feature);
    }

    /// <inheritdoc />
    protected override Task<long[]?> QueryParamsByProperties(AirQualityVectorQuery query) => Task.FromResult<long[]?>(null);
    
}