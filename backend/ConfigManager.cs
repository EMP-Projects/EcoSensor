using EcoSensorApi.Config;
using Microsoft.EntityFrameworkCore;

namespace EcoSensorApi;

/// <summary>
/// Provides configuration management for the EcoSensor API.
/// </summary>
public static class ConfigManager
{
    /// <summary>
    /// Adds configuration data for Gioia del Colle to the model builder.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> to configure.</param>
    /// <returns>The configured <see cref="ModelBuilder"/>.</returns>
    public static ModelBuilder AddInitConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfigModel>().HasData(
            new ConfigModel
            {
                Id = 1,
                EntityKey = Guid.NewGuid().ToString(),
                TimeStamp = DateTime.UtcNow,
                RegionCode = 16,
                RegionName = "Puglia",
                ProvCode = 72,
                ProvName = "Bari",
                CityCode = 72021,
                CityName = "Gioia del Colle",
                TypeMonitoringData = ETypeMonitoringData.AirQuality
            });
        
        modelBuilder.Entity<ConfigModel>().HasData(
            new ConfigModel
            {
                Id = 2,
                EntityKey = "Siena",
                TimeStamp = DateTime.UtcNow,
                RegionCode = 9,
                RegionName = "Toscana",
                ProvCode = 52,
                ProvName = "Siena",
                CityCode = 52032,
                CityName = "Siena",
                TypeMonitoringData = ETypeMonitoringData.AirQuality
            });

        return modelBuilder;
    }
}