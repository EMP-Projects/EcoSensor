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

/// <summary>
/// Represents the database context for the EcoSensorApi application.
/// </summary>
public class EcoSensorDbContext : DbContext, IOsmDbContext
{
    /// <summary>
    /// Represents an air quality vector.
    /// </summary>
    public DbSet<AirQualityVectorModel>? AirQualityVector { get; set; }
    
    /// <summary>
    /// Represents the properties of air quality measures.
    /// </summary>
    public DbSet<AirQualityPropertiesModel>? AirQualityProperties { get; set; }
    
    /// <summary>
    /// Represents the EU air quality level.
    /// </summary>
    public DbSet<EuAirQualityLevel>? EuAirQualityLevels { get; set; }
    
    /// <summary>
    /// Represents the air quality levels in the United States.
    /// </summary>
    public DbSet<UsAirQualityLevel>? UsAirQualityLevels { get; set; }
    
    /// <summary>
    /// Represents the configuration settings for the EcoSensor application.
    /// </summary>
    public DbSet<ConfigModel>? Config { get; set; }

    /// <summary>
    /// Represents the OsmProperties table in the EcoSensorDbContext.
    /// </summary>
    public DbSet<OsmPropertiesModel>? OsmProperties { get; set; }
    
    /// <summary>
    /// Represents the OsmVector entity in the EcoSensorDbContext.
    /// OsmVector is used to store the OpenStreetMap vector data.
    /// </summary>
    public DbSet<OsmVectorModel>? OsmVector { get; set; }

    /// <summary>
    /// Represents the database context for the EcoSensor API.
    /// </summary>
    public EcoSensorDbContext(DbContextOptions<EcoSensorDbContext> options) : base(options) {}

    /// <summary>
    /// Override method called by Entity Framework to configure the database model that is used for this context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
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
                EntityKey = "Gioia del Colle",
                TimeStamp = DateTime.UtcNow,
                Distance = 100,
                MatrixDistancePoints = 2500,
                RegionCode = 16,
                CityCode = 72021,
                CityName = "Gioia del Colle",
            });
    }
}