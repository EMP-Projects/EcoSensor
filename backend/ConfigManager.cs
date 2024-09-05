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
    public static ModelBuilder Add_GioiadelColle(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfigModel>().HasData(
            new ConfigModel
            {
                Id = 1,
                EntityKey = "Gioia del Colle",
                TimeStamp = DateTime.UtcNow,
                RegionCode = 16,
                CityCode = 72021,
                CityName = "Gioia del Colle",
            });

        return modelBuilder;
    }
}