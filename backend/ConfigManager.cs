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
                EntityKey = "Gioia_del_Colle".ToLower(),
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
                EntityKey = "Siena".ToLower(),
                TimeStamp = DateTime.UtcNow,
                RegionCode = 9,
                RegionName = "Toscana",
                ProvCode = 52,
                ProvName = "Siena",
                CityCode = 52032,
                CityName = "Siena",
                TypeMonitoringData = ETypeMonitoringData.AirQuality
            });
        
        modelBuilder.Entity<ConfigModel>().HasData(
            new ConfigModel
            {
                Id = 3,
                EntityKey = "Matera".ToLower(),
                TimeStamp = DateTime.UtcNow,
                RegionCode = 17,
                RegionName = "Basilicata",
                ProvCode = 77,
                ProvName = "Matera",
                CityCode = 77014,
                CityName = "Matera",
                TypeMonitoringData = ETypeMonitoringData.AirQuality
            });
        
        modelBuilder.Entity<ConfigModel>().HasData(
            new ConfigModel
            {
                Id = 4,
                EntityKey = "Bari".ToLower(),
                TimeStamp = DateTime.UtcNow,
                RegionCode = 16,
                RegionName = "Puglia",
                ProvCode = 72,
                ProvName = "Bari",
                CityCode = 72006,
                CityName = "Bari",
                TypeMonitoringData = ETypeMonitoringData.AirQuality
            });
        
        modelBuilder.Entity<ConfigModel>().HasData(
            new ConfigModel
            {
                Id = 5,
                EntityKey = "Taranto".ToLower(),
                TimeStamp = DateTime.UtcNow,
                RegionCode = 16,
                RegionName = "Puglia",
                ProvCode = 73,
                ProvName = "Taranto",
                CityCode = 73027,
                CityName = "Taranto",
                TypeMonitoringData = ETypeMonitoringData.AirQuality
            });
        
        modelBuilder.Entity<ConfigModel>().HasData(
            new ConfigModel
            {
                Id = 6,
                EntityKey = "Statte".ToLower(),
                TimeStamp = DateTime.UtcNow,
                RegionCode = 16,
                RegionName = "Puglia",
                ProvCode = 73,
                ProvName = "Taranto",
                CityCode = 73029,
                CityName = "Statte",
                TypeMonitoringData = ETypeMonitoringData.AirQuality
            });

        return modelBuilder;
    }
}