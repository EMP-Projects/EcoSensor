using NetTopologySuite.Geometries;
using TeamSviluppo.Auth;
using TeamSviluppo.Exceptions;
using TeamSviluppo.Gis;
using TeamSviluppo.Services;

namespace EcoSensorApi.Config;

public class ConfigService : Service<ConfigDto, ConfigModel, ConfigQuery>
{
    /// <inheritdoc />
    public ConfigService(ILogger<ConfigService> logger, ConfigRepository repository, IAuthService authService) : 
        base(logger, repository, authService) { }

    /// <summary>
    /// Lettura del file GeoJson dei limiti delle regioni e comuni italiani
    /// Info: https://github.com/openpolis/geojson-italy/blob/master/geojson/
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<BBoxConfig>> BBoxGeometries()
    {

        var layers = await List(new ConfigQuery
        {
            TypeSource = ETypeSourceLayer.GeoJson
        });
        
        if (layers is null)
        {
            const string msg = "Non riesco a leggere i layers di configurazione";
            Logger.LogError(msg);
            throw new Exception(msg);
        }

        var resultBBox = new List<BBoxConfig>();
        
        foreach (var layer in layers)
        {
            var featuresCollection = GisUtility.GetFeatureCollectionByGeoJson(layer.Name);
            // filtro il GeoJson per il comune e regione
            var fFiltered = featuresCollection.Where(x => (long)x.Attributes.GetOptionalValue(layer.RegionField) == layer.RegionCode).ToList();
                                                                     
            if (fFiltered is null)
            {
                const string msg = "Non riesco a leggere il GeoJson delle geometrie delle regioni";
                Logger.LogError(msg);
                throw new Exception(msg);
            }
            
            if (layer.CityField is not null || layer.CityCode is not null)
                fFiltered = fFiltered.Where(x => (long)x.Attributes.GetOptionalValue(layer.CityField) == layer.CityCode).ToList();
            
            var fColl = fFiltered.FirstOrDefault();
            
            if (fColl is null)
            {
                const string msg = "Non riesco a leggere il GeoJson delle geometrie";
                Logger.LogError(msg);
                throw new Exception(msg);
            }
            
            // creo la geometria per eseguire il filtro sulle feature di OSM
            var geom = GisUtility.CreateGeometryFactory(3857).CreateGeometry(fColl.Geometry);
    
            // con geoserver si possono leggere direttamente i dati geojson con lo stesso sistema di riferimento di Osm EPSG:3857
            // creo un bbox dalla geometria nelle coordinate EPSG:3857
            var bbox = geom.Envelope;
            var pointMinToWebMercator = CoordinateConverter.ConvertWgs84ToWebMercator(bbox.EnvelopeInternal.MinX, bbox.EnvelopeInternal.MinY);
            var pointMaxToWebMercator = CoordinateConverter.ConvertWgs84ToWebMercator(bbox.EnvelopeInternal.MaxX, bbox.EnvelopeInternal.MaxY);
            var bboxConverted = GisUtility.CreateEnvelopeFromBBox(pointMinToWebMercator.Y, pointMinToWebMercator.X, pointMaxToWebMercator.Y, pointMaxToWebMercator.X);
            resultBBox.Add(new BBoxConfig(GisUtility.CreateGeometryFromBBox(3857, bboxConverted), layer));
        }
        
        // TODO: leggere i layers da un server GeoServer

        return resultBBox;
    }

    protected override Task ClearExternalRepositoriesCache() => Task.CompletedTask;
    public override Task Validate(ConfigDto dto, CrudEnum crudEnum) => Task.CompletedTask;
}