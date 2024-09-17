using EcoSensorApi.AirQuality.Properties;
using EcoSensorApi.Config;
using Gis.Net.Core.Repositories;
using Gis.Net.Osm.OsmPg.Vector;
using Gis.Net.Vector;
using Gis.Net.Vector.Services;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

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
    private readonly ConfigService _configService;
    
    /// <inheritdoc />
    public AirQualityVectorService(
        ILogger<AirQualityVectorService> logger, 
        AirQualityVectorRepository netCoreRepository, 
        OsmVectorService<EcoSensorDbContext> osmVectorService, 
        ConfigService configService) : 
        base(logger, netCoreRepository)
    {
        _osmVectorService = osmVectorService;
        _configService = configService;
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
    
    private async Task<ICollection<OsmVectorDto>?> GetOsmVectorList(string keyName)
    {
        // read the list of OSM vectors
        var listOsm = await _osmVectorService.List(new OsmVectorQuery
        {
            SrCode = 3857,
            EntityKey = keyName
        });

        if (listOsm.Count != 0) return listOsm;
        
        const string msg = "I can't read geometries from OpenStreetMap";
        Logger.LogError(msg);
        return null;
    }
    
    /// <summary>
    /// Retrieves a list of air quality vectors based on the specified key name.
    /// </summary>
    /// <param name="keyName">The key name used to query the air quality vectors.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of 
    /// <see cref="AirQualityVectorDto"/> objects if found; otherwise, null.
    /// </returns>
    public async Task<ICollection<AirQualityVectorDto>?> GetAirQualityVectorList(string keyName)
    {
        // read the list of air quality vectors
        var listAq = await List(new AirQualityVectorQuery
        {
            SrCode = 3857,
            EntityKey = keyName
        });

        if (listAq.Count != 0) return listAq;
    
        const string msg = "I can't read geometries from AirQuality";
        Logger.LogError(msg);
        return null;
    }
    
    /// <summary>
    /// Calculate measurement points for air quality.
    /// </summary>
    /// <returns>The total number of new measurement points created.</returns>
    /// <exception cref="Exception">Generic exception.</exception>
    public async Task<int> CreateMeasurementPoints()
    {
        // distance between the points in meters
        const double matrixDistance = 2500;
        
        // read the list of configuration layers
        var layers = await _configService.List(new ConfigQuery());

        foreach (var layer in layers)
        {
            // read the list of OSM vectors
            var listOsm = await GetOsmVectorList(layer.EntityKey);
            if (listOsm is null) continue;
            
            // read the list of air quality vectors
            var listMeasurementPoints = await GetAirQualityVectorList(layer.EntityKey);
            
            // create a list of points
            var measurementPoints = new List<Point>();
        
            // read all the points created from the geometry coordinates
            foreach (var osm in listOsm)
            {
                // read all the coordinates
                var coordinates = osm.Geom?.Coordinates;
                // I create a collection of geographic points from coordinates
                var coordinatesGeom = coordinates?.Select(coordinate => GisUtility.CreatePoint(3857, coordinate)).ToList();

                if (coordinatesGeom is null)
                {
                    Logger.LogWarning("I can't read the coordinates of the geometry");
                    continue;
                }

                // I select only the first point
                foreach (var coords in coordinatesGeom)
                {
                    // if the list is empty, I add the first point
                    if (measurementPoints.Count == 0)
                        measurementPoints.Add(coords);
                    else
                    {
                        // otherwise only if greater than or equal to the distance configured for the layer (2500 mt)
                        var nearestPoint = measurementPoints.MinBy(p => p.Distance(coords));
                        var distance = nearestPoint?.Distance(coords) ?? 0;
                        if (distance >= matrixDistance)
                            measurementPoints.Add(coords);
                        else
                        {
                            Logger.LogWarning("The distance between the points is less than the configured value");
                            continue;
                        }
                    }
                    
                    // create a new air quality point
                    var aqPoint = new AirQualityVectorDto
                    {
                        SourceData = ESourceData.Osm,
                        Geom = coords,
                        TimeStamp = DateTime.UtcNow,
                        Guid = Guid.NewGuid(),
                        EntityKey = layer.EntityKey,
                        EntityVectorId = osm.Id,
                        Lat = coords.Y,
                        Lng = coords.X
                    };
                    await Insert(aqPoint);
                }
            }
        }
        
        // save the data in the database
        return await SaveContext();
    }
}