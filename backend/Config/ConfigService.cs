using Gis.Net.Core.Services;
using Gis.Net.GeoJsonImport;
using Gis.Net.Istat;
using Gis.Net.Istat.Models;
using Gis.Net.Vector;
using Geometry = NetTopologySuite.Geometries.Geometry;

namespace EcoSensorApi.Config;

/// <inheritdoc />
public class ConfigService : ServiceCore<ConfigModel, ConfigDto, ConfigQuery, ConfigRequest, EcoSensorDbContext>
{
    private readonly IIStatService<IstatContext> _istatService;
    
    /// <inheritdoc />
    public ConfigService(ILogger<ConfigService> logger, ConfigRepository repository, IIStatService<IstatContext> istatService) : 
        base(logger, repository)
    {
        _istatService = istatService;
    }

    /// <summary>
    /// Reading the GeoJson file of the limits of the Italian regions and municipalities
    /// Info: https://github.com/openpolis/geojson-italy/blob/master/geojson/
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<BBoxConfig>> BBoxGeometries()
    {
        var layers = await List(new ConfigQuery());
        
        if (layers is null)
        {
            const string msg = "I can't read the configuration layers";
            Logger.LogError(msg);
            throw new Exception(msg);
        }

        var layersKeys = layers.Select(x => x.EntityKey).ToArray();
        var key = string.Join(":", layersKeys);
        var resultBboxConfigList = new List<BBoxConfig>();

        foreach (var layer in layers)
        {
            var istat = await _istatService.GetMunicipalities(new LimitsItMunicipality
            {
                RegName = layer.RegionName,
                RegIstatCodeNum = layer.RegionCode,
                ProvName = layer.ProvName,
                ProvIstatCodeNum = layer.ProvCode,
                ComIstatCodeNum = layer.CityCode,
                Name = layer.CityName
            });
            
            if (istat is null)
            {
                const string msg = "I can't read the Istat data";
                Logger.LogError(msg);
                throw new Exception(msg);
            }

            var bboxGeometries = istat.Where(x => x.WkbGeometry?.EnvelopeInternal is not null).Select(x => GisUtility.CreateGeometryFromBBox(3857, x.WkbGeometry?.EnvelopeInternal!)).ToList();
            var bboxConfigList = bboxGeometries.Select(bbox => new BBoxConfig(bbox, key, layer.Distance)).ToList();
            resultBboxConfigList.AddRange(bboxConfigList);
        }

        return resultBboxConfigList;
    }
}