using EcoSensorApi.AirQuality.Indexes;
using EcoSensorApi.AirQuality.Indexes.Eu;
using EcoSensorApi.AirQuality.Indexes.Us;
using EcoSensorApi.AirQuality.Properties;
using EcoSensorApi.AirQuality.Vector;
using EcoSensorApi.Config;
using Gis.Net.Osm.OsmPg;
using Gis.Net.Osm.OsmPg.Properties;
using Gis.Net.Osm.OsmPg.Vector;
using Gis.Net.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoSensorApi;

public class EcoSensorDbContext : DbContext, IOsmDbContext
{
    public DbSet<AirQualityVectorModel>? AirQualityVector { get; set; }
    public DbSet<AirQualityPropertiesModel>? AirQualityProperties { get; set; }
    public DbSet<EuAirQualityLevel>? EuAirQualityLevels { get; set; }
    public DbSet<UsAirQualityLevel>? UsAirQualityLevels { get; set; }
    public DbSet<ConfigModel>? Config { get; set; }

    public DbSet<OsmPropertiesModel>? OsmProperties { get; set; }
    public DbSet<OsmVectorModel>? OsmVector { get; set; }

    /// <inheritdoc />
    public EcoSensorDbContext(DbContextOptions<EcoSensorDbContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        // modelBuilder.AddExtensionPostGis();
        
        modelBuilder.SetTimeStamp<AirQualityPropertiesModel>();
        modelBuilder.SetTimeStamp<AirQualityVectorModel>();
        modelBuilder.SetTimeStamp<OsmPropertiesModel>();
        modelBuilder.SetTimeStamp<OsmVectorModel>();
        modelBuilder.SetTimeStamp<EuAirQualityLevel>();
        modelBuilder.SetTimeStamp<UsAirQualityLevel>();
        modelBuilder.SetTimeStamp<ConfigModel>();
        
        foreach (var entry in AirQualityIndex.EuIndexes.Select((x, i) => new { Value = x, Index = i }) )
        {
            entry.Value.Id = entry.Index + 1;
            modelBuilder.Entity<EuAirQualityLevel>().HasData(entry.Value);
        }
        
        foreach (var entry in AirQualityIndex.UsIndexes.Select((x, i) => new { Value = x, Index = i }) )
        {
            entry.Value.Id = entry.Index + 1;
            modelBuilder.Entity<UsAirQualityLevel>().HasData(entry.Value);
        }
        
        modelBuilder.Entity<ConfigModel>().HasData(
            new ConfigModel
            {
                Id = 1,
                Key = "Gioia del Colle",
                TimeStamp = DateTime.UtcNow,
                Distance = 100,
                MatrixDistancePoints = 2500,
                Name = "limits_P_72_municipalities.geojson",
                RegionField = "reg_istat_code_num",
                RegionCode = 16,
                CityField = "com_istat_code_num",
                CityCode = 72021
            });
    }
}