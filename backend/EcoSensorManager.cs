using System.Reflection;
using EcoSensorApi.AirQuality.Indexes.Eu;
using EcoSensorApi.AirQuality.Indexes.Us;
using EcoSensorApi.AirQuality.Properties;
using EcoSensorApi.AirQuality.Vector;
using EcoSensorApi.Config;
using EcoSensorApi.MeasurementPoints;
using EcoSensorApi.Tasks.Osm;
using Gis.Net.Core.Entities;
using Gis.Net.Istat;
using Gis.Net.Osm.OsmPg;

namespace EcoSensorApi;

/// <summary>
/// Provides extension methods for configuring EcoSensor services.
/// </summary>
public static class EcoSensorManager
{
    /// <summary>
    /// Adds EcoSensor services to the specified <see cref="WebApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to configure.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder"/>.</returns>
    public static WebApplicationBuilder AddEcoSensor(this WebApplicationBuilder builder)
    {
        // Configure PostgreSQL connection for EcoSensor
        var connection = new ConnectionPgSql(
            builder.Configuration["POSTGRES_HOST"]!,
            builder.Configuration["POSTGRES_PORT"]!,
            builder.Configuration["POSTGRES_DB"]!,
            builder.Configuration["POSTGRES_USER"]!,
            builder.Configuration["POSTGRES_PASS"]!
        )
        {
            ReadBufferSize = 24000,
            WriteBufferSize = 24000
        };
        
        // Add PostGIS services for EcoSensorDbContext
        builder.Services.AddPostGis<EcoSensorDbContext>(connection, 
            builder.Environment.ApplicationName,
            builder.Environment.IsDevelopment());
        
        // Add OSM PostGIS services for EcoSensorDbContext
        builder.AddOsmPostGis<EcoSensorDbContext>(connection);
        
        // Configure PostgreSQL connection for ISTAT
        var connectionIstat = new ConnectionPgSql(
            builder.Configuration["POSTGRES_ISTAT_HOST"]!,
            builder.Configuration["POSTGRES_ISTAT_PORT"]!,
            builder.Configuration["POSTGRES_ISTAT_DB"]!,
            builder.Configuration["POSTGRES_ISTAT_USER"]!,
            builder.Configuration["POSTGRES_ISTAT_PASS"]!
        )
        {
            ReadBufferSize = 24000,
            WriteBufferSize = 24000
        };
        
        // Add ISTAT GIS services
        builder.AddIstatGis(connectionIstat);

        // Register repositories
        builder.Services.AddScoped<AirQualityPropertiesRepository>();
        builder.Services.AddScoped<AirQualityPropertiesService>();
        builder.Services.AddScoped<EuAirQualityLevelRepository>();
        builder.Services.AddScoped<UsAirQualityLevelRepository>();
        
        // Register services
        builder.Services.AddScoped<AirQualityVectorRepository>();
        builder.Services.AddScoped<AirQualityVectorService>();
        builder.Services.AddScoped<UsAirQualityLevelService>();
        builder.Services.AddScoped<EuAirQualityLevelService>();

        builder.Services.AddScoped<ConfigRepository>();
        builder.Services.AddScoped<ConfigService>();
        
        // Register AutoMapper with the executing assembly
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        // Register service for creating measurement points grid
        builder.Services.AddScoped<MeasurementPointsService>();
        
        // Register OpenStreetMap tasks
        builder.Services.AddSingleton<MeasurementPointsTasks>();
        builder.Services.AddSingleton<SeedFeaturesTasks>();
        builder.Services.AddSingleton<AirQualityTasks>();
        builder.Services.AddHostedService<OsmBackgroundTasks>();
        
        return builder;
    }
}