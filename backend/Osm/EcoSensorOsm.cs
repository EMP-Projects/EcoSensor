using Gis.Net.Osm;
using Gis.Net.Osm.OsmPg;
using Gis.Net.Osm.OsmPg.Models;
using NetTopologySuite.Geometries;

namespace EcoSensorApi.Osm;

/// <inheritdoc />
public class EcoSensorOsm : OsmService<Osm2PgsqlDbContext>
{
    /// <inheritdoc />
    public EcoSensorOsm(IOsmPg<PlanetOsmLine, Osm2PgsqlDbContext> lines, IOsmPg<PlanetOsmPolygon, Osm2PgsqlDbContext> polygons, IOsmPg<PlanetOsmPoint, Osm2PgsqlDbContext> points, IOsmPg<PlanetOsmRoads, Osm2PgsqlDbContext> roads) : base(lines, polygons, points, roads)
    {
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