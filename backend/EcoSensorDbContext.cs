using EcoSensorApi.AirQuality.Indexes;
using EcoSensorApi.AirQuality.Indexes.Eu;
using EcoSensorApi.AirQuality.Indexes.Us;
using EcoSensorApi.AirQuality.Properties;
using EcoSensorApi.AirQuality.Vector;
using Microsoft.EntityFrameworkCore;
using TeamSviluppo.Gis.NetCoreFw;
using TeamSviluppo.Gis.NetCoreFw.OsmPg;
using TeamSviluppo.Gis.NetCoreFw.OsmPg.Properties;
using TeamSviluppo.Gis.NetCoreFw.OsmPg.Vector;

namespace EcoSensorApi;

public class EcoSensorDbContext : DbContext, IOsmDbContext
{
    public DbSet<AirQualityVectorModel>? AirQualityVector { get; set; }
    public DbSet<AirQualityPropertiesModel>? AirQuality { get; set; }
    public DbSet<EuAirQualityLevel>? EuAirQualityLevels { get; set; }
    public DbSet<UsAirQualityLevel>? UsAirQualityLevels { get; set; }

    public DbSet<OsmPropertiesModel>? OsmProperties { get; set; }
    public DbSet<OsmVectorModel>? OsmVector { get; set; }

    /// <inheritdoc />
    public EcoSensorDbContext(DbContextOptions<EcoSensorDbContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigVectorCoreDbContext<AirQualityVectorModel>();
        modelBuilder.ConfigVectorCoreDbContext<OsmVectorModel>();
        
        var listEuAirQualityIndexes = AirQualityIndex.EuIndexes;
        var listUsAirQualityIndexes = AirQualityIndex.UsIndexes;
        
        foreach (var entry in listEuAirQualityIndexes.Select((x, i) => new { Value = x, Index = i }) )
        {
            entry.Value.Id = entry.Index + 1;
            modelBuilder.Entity<EuAirQualityLevel>().HasData(entry.Value);
        }
        
        foreach (var entry in listUsAirQualityIndexes.Select((x, i) => new { Value = x, Index = i }) )
        {
            entry.Value.Id = entry.Index + 1;
            modelBuilder.Entity<UsAirQualityLevel>().HasData(entry.Value);
        }
    }
}