using EcoSensorApi.AirQuality.Properties;
using Gis.Net.Core.Repositories;
using Gis.Net.Osm.OsmPg.Vector;
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
    private readonly OsmVectorService<EcoSensorDbContext> _osmVectorService;
    
    /// <inheritdoc />
    public AirQualityVectorService(
        ILogger<AirQualityVectorService> logger, 
        AirQualityVectorRepository netCoreRepository, 
        OsmVectorService<EcoSensorDbContext> osmVectorService) : 
        base(logger, netCoreRepository)
    {
        _osmVectorService = osmVectorService;
    }

    /// <inheritdoc />
    public override string? NameProperties { get; set; } = "AirQuality";

    /// <inheritdoc />
    protected override ListOptions<AirQualityVectorModel, AirQualityVectorDto, AirQualityVectorQuery> GetRowsOptions(
        AirQualityVectorQuery q) => new(q)
    {
        OnExtraMappingAsync = async (model, dto) =>
        {
            dto.EntityVector = await _osmVectorService.Find(model.EntityVectorId);
        }
    };

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