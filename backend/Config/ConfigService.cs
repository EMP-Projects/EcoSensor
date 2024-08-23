using Gis.Net.Core.Services;
using Gis.Net.GeoJsonImport;
using Gis.Net.Vector;

namespace EcoSensorApi.Config;

/// <inheritdoc />
public class ConfigService : ServiceCore<ConfigModel, ConfigDto, ConfigQuery, ConfigRequest, EcoSensorDbContext>
{
    /// <inheritdoc />
    public ConfigService(ILogger<ConfigService> logger, ConfigRepository repository) : 
        base(logger, repository) { }

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
            var geoJson = GeoJson.GetFeatureCollectionByGeoJson(layer.Name, "GeoJson");

            if (geoJson is null)
                continue;
            
            // filtro il GeoJson per il comune e regione
            var fFiltered = geoJson.Features
                .Where(feature => feature.Properties.RegIstatCodeNum.Equals(layer.RegionCode)).ToList();
                                                                     
            if (fFiltered is null)
            {
                const string msg = "Non riesco a leggere il GeoJson delle geometrie delle regioni";
                Logger.LogError(msg);
                throw new Exception(msg);
            }
            
            if (layer.CityField is not null || layer.CityCode is not null)
                fFiltered = fFiltered.Where(feature => feature.Properties.ComIstatCodeNum.Equals(layer.CityCode)).ToList();
            
            var fColl = fFiltered.FirstOrDefault();
            
            if (fColl is null)
            {
                const string msg = "Non riesco a leggere il GeoJson delle geometrie";
                Logger.LogError(msg);
                throw new Exception(msg);
            }
            
            // creo la geometria per eseguire il filtro sulle feature di OSM
            var geom = GeoJson.CreatePolygon(fColl.GeometryImport.Coordinates);
            if (geom is null)
            {
                const string msg = "Non riesco a creare la geometria per il filtro";
                Logger.LogError(msg);
                throw new Exception(msg);
            }
    
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
}