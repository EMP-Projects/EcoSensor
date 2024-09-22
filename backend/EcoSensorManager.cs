using System.Reflection;
using EcoSensorApi.AirQuality.Indexes.Eu;
using EcoSensorApi.AirQuality.Indexes.Us;
using EcoSensorApi.AirQuality.Properties;
using EcoSensorApi.AirQuality.Vector;
using EcoSensorApi.Aws;
using EcoSensorApi.Config;
using EcoSensorApi.MeasurementPoints;
using EcoSensorApi.Osm;
using EcoSensorApi.Tasks.Osm;
using Gis.Net.Aws.AWSCore.DynamoDb;
using Gis.Net.Aws.AWSCore.S3;
using Gis.Net.Aws.AWSCore.SNS;
using Gis.Net.Core;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Tasks;
using Gis.Net.Istat;
using Gis.Net.OpenMeteo;
using Gis.Net.Osm.OsmPg;
using Microsoft.AspNetCore.HttpLogging;

namespace EcoSensorApi;

/// <summary>
/// Provides extension methods for configuring EcoSensor services.
/// </summary>
public static class EcoSensorManager
{
    /// <summary>
    /// Configures and starts the EcoSensor services.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to configure.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder"/>.</returns>
    public static WebApplicationBuilder StartEcoSensor(this WebApplicationBuilder builder)
    {
        // Add notification service
        builder.Services.AddNotificationService();

        // Connection to the PostGis database and registration of services, repositories, and mappers
        builder.AddEcoSensor();

        // Add air quality services
        builder.Services.AddAirQuality(builder.Configuration["OpenMeteo:Url"]);

        // Add AWS S3 bucket services
        builder.AddAwsBucketS3();
        builder.AddAwsSns();

        // Add controllers
        builder.Services.AddControllers();

        // Add HTTP logging with all logging fields
        builder.Services.AddHttpLogging(opt => opt.LoggingFields = HttpLoggingFields.All);
    
        // Configure Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddVersion(1, 0);
    
        return builder;
    }
    
    /// <summary>
    /// Adds EcoSensor services to the specified <see cref="WebApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to configure.</param>
    /// <returns>The configured <see cref="WebApplicationBuilder"/>.</returns>
    private static WebApplicationBuilder AddEcoSensor(this WebApplicationBuilder builder)
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
        
        var connectionOsm = new ConnectionPgSql(
            builder.Configuration["POSTGRES_OSM_HOST"]!,
            builder.Configuration["POSTGRES_OSM_PORT"]!,
            builder.Configuration["POSTGRES_OSM_DB"]!,
            builder.Configuration["POSTGRES_OSM_USER"]!,
            builder.Configuration["POSTGRES_OSM_PASS"]!
        )
        {
            ReadBufferSize = 24000,
            WriteBufferSize = 24000
        };
        
        // Add OSM PostGIS services for EcoSensorDbContext
        builder.AddOsmPostGis<EcoSensorDbContext>(connectionOsm);
        
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
        
        // Add Istat Gis services
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
        builder.Services.AddScoped<IOsmPgService, EcoSensorAirQuality>();
        
        // Register tasks
        builder.Services.AddSingleton<MeasurementPointsTasks>();
        builder.Services.AddSingleton<SeedFeaturesTasks>();
        builder.Services.AddSingleton<AirQualityTasks>();
        builder.Services.AddSingleton<DeleteOldDataTasks>();
        builder.Services.AddSingleton<CreateGeoJson>();
        builder.Services.AddHostedService<OsmBackgroundTasks>();

        builder.Services.AddScoped<IEcoSensorAws, EcoSensorAws>();
        builder.Services.AddScoped<EcoSensorAddDbContext>();
        builder.Services.AddScoped<DynamoDbEcoSensorService>();
        
        return builder;
    }
}