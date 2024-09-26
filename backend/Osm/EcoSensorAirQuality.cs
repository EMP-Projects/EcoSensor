using Gis.Net.Osm;
using Gis.Net.Osm.OsmPg;
using Gis.Net.Osm.OsmPg.Models;
using NetTopologySuite.Geometries;

namespace EcoSensorApi.Osm;

/// <inheritdoc />
public class EcoSensorAirQuality : OsmService<Osm2PgsqlDbContext>
{
    /// <inheritdoc />
    public EcoSensorAirQuality(IOsmPg<PlanetOsmLine, Osm2PgsqlDbContext> lines, IOsmPg<PlanetOsmPolygon, Osm2PgsqlDbContext> polygons, IOsmPg<PlanetOsmPoint, Osm2PgsqlDbContext> points, IOsmPg<PlanetOsmRoads, Osm2PgsqlDbContext> roads) : base(lines, polygons, points, roads)
    {
    }

    /// <inheritdoc />
    protected override OsmOptions<PlanetOsmPolygon> OsmOptionsPolygon(Geometry geom)
    {
        var options = base.OsmOptionsPolygon(geom);
        string[] tags = ["residential", "industrial", "military"];
        options.OnBeforeQuery = query => query.Where(x => x.Landuse != null && tags.Contains(x.Landuse));
        options.Tags = tags;
        return options;
    }

    /// <inheritdoc />
    protected override OsmOptions<PlanetOsmRoads> OsmOptionsRoads(Geometry geom)
    {
        var options = base.OsmOptionsRoads(geom);
        var tags = OsmTag.Items(EOsmTag.Highway);
        options.OnBeforeQuery = query => query.Where(x => x.Highway != null && tags.Contains(x.Highway));
        options.Tags = tags;
        return options; 
    }
}