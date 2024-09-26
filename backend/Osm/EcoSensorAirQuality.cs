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
        options.OnBeforeQuery = query => query.Where(x => x.Landuse != null && x.Military != null && x.Amenity != null);
        options.Tags = ["residential", "industrial", "airfield", "airport"];
        options.OnAfterQuery = (query, tags) => query.Where(x => tags.Contains(x.Landuse)).ToList();
        return options;
    }

    /// <inheritdoc />
    protected override OsmOptions<PlanetOsmRoads> OsmOptionsRoads(Geometry geom)
    {
        var options = base.OsmOptionsRoads(geom);
        options.OnBeforeQuery = query => query.Where(x => x.Highway != null);
        options.Tags = OsmTag.Items(EOsmTag.Highway);
        options.OnAfterQuery = (query, tags) => query.Where(x => tags.Contains(x.Highway)).ToList();
        return options; 
    }
}