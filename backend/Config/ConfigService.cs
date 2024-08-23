using Gis.Net.Core.Services;
using Gis.Net.GeoJsonImport;
using Gis.Net.Vector;
using NetTopologySuite.Geometries;

namespace EcoSensorApi.Config;

/// <inheritdoc />
public class ConfigService : ServiceCore<ConfigModel, ConfigDto, ConfigQuery, ConfigRequest, EcoSensorDbContext>
{
    /// <inheritdoc />
    public ConfigService(ILogger<ConfigService> logger, ConfigRepository repository) : 
        base(logger, repository) { }

    private List<FeatureImport> FindFeatureFromSource(ConfigDto layer, GeoJsonImport geoJson)
    {
        // filtro il GeoJson per il comune e regione
        var fFiltered = geoJson.Features
            .Where(feature => feature.Properties.RegIstatCodeNum.Equals(layer.RegionCode)).ToList();

        string msg;
        if (fFiltered is null)
        {
            msg = "I can't read the GeoJson of the region geometries";
            Logger.LogError(msg);
            throw new Exception(msg);
        }
            
        if (layer.CityField is not null || layer.CityCode is not null)
            fFiltered = fFiltered.Where(feature => feature.Properties.ComIstatCodeNum.Equals(layer.CityCode)).ToList();
            
        if (fFiltered.Count != 0)
            return fFiltered;
        
        msg = "I can't read the GeoJson of the geometries";
        Logger.LogError(msg);
        throw new Exception(msg);
    }

    /// <summary>
    /// Reading the GeoJson file of the limits of the Italian regions and municipalities
    /// Info: https://github.com/openpolis/geojson-italy/blob/master/geojson/
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<BBoxConfig> BBoxGeometries()
    {

        var layers = await List(new ConfigQuery
        {
            TypeSource = ETypeSourceLayer.GeoJson
        });
        
        if (layers is null)
        {
            const string msg = "I can't read the configuration layers";
            Logger.LogError(msg);
            throw new Exception(msg);
        }

        var layersKeys = layers.Select(x => x.EntityKey).ToArray();
        var key = string.Join(":", layersKeys);
        var bboxGeom = new List<Geometry>();
        
        foreach (var layer in layers)
        {
            var geoJson = GeoJson.GetFeatureCollectionByGeoJson(layer.Name, "GeoJson");
            if (geoJson is null) continue;

            var features = FindFeatureFromSource(layer, geoJson);

            foreach (var geom in features.Select(feature => GeoJson.CreatePolygon(feature.GeometryImport.Coordinates)))
            {
                if (geom is null)
                {
                    const string msg = "I can't create geometry for the filter";
                    Logger.LogError(msg);
                    continue;
                }
    
                // with geoserver you can directly read geojson data with the same reference system as Osm EPSG:3857
                // I create a bbox from geometry in EPSG:3857 coordinates
                bboxGeom.Add(geom.Envelope);
            }
        }

        // combine the geometries
        Geometry bboxUnion = GisUtility.CreateGeometryFactory(3857).CreatePoint();
        bboxUnion = bboxGeom.Aggregate(bboxUnion, (current, bbox) => current.Union(bbox));

        // I convert the geographic coordinate reference system
        var pointMinToWebMercator = CoordinateConverter.ConvertWgs84ToWebMercator(bboxUnion.EnvelopeInternal.MinX, bboxUnion.EnvelopeInternal.MinY);
        var pointMaxToWebMercator = CoordinateConverter.ConvertWgs84ToWebMercator(bboxUnion.EnvelopeInternal.MaxX, bboxUnion.EnvelopeInternal.MaxY);
        var bboxConverted = GisUtility.CreateEnvelopeFromBBox(pointMinToWebMercator.Y, pointMinToWebMercator.X, pointMaxToWebMercator.Y, pointMaxToWebMercator.X);
        return new BBoxConfig(GisUtility.CreateGeometryFromBBox(3857, bboxConverted), key);
    }
}