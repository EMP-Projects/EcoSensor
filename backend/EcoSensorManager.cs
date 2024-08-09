using System.Reflection;
using EcoSensorApi.AirQuality.Indexes.Eu;
using EcoSensorApi.AirQuality.Indexes.Us;
using EcoSensorApi.AirQuality.Properties;
using EcoSensorApi.AirQuality.Vector;
using EcoSensorApi.Config;
using EcoSensorApi.MeasurementPoints;
using EcoSensorApi.Tasks.Osm;
using TeamSviluppo.Gis;
using TeamSviluppo.Gis.NetCoreFw.OsmPg;
using TeamSviluppo.Services;
namespace EcoSensorApi;

public static class EcoSensorManager
{
    
    public static WebApplicationBuilder AddEcoSensor(this WebApplicationBuilder builder)
    {
        var connection = new ConnectionPostGis(
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
        
        builder.Services.AddPostGis<EcoSensorDbContext>(connection, 
            builder.Environment.ApplicationName,
            builder.Environment.IsDevelopment());
        
        builder.Services.AddScoped<IAppDbContext, EcoSensorAppDbContext>();
        
        builder.AddOsmPostGis<EcoSensorDbContext>();

        // repository
        builder.Services.AddScoped<AirQualityPropertiesRepository>();
        builder.Services.AddScoped<AirQualityPropertiesService>();
        builder.Services.AddScoped<EuAirQualityRepository>();
        builder.Services.AddScoped<UsAirQualityRepository>();
        
        // service
        builder.Services.AddScoped<AirQualityVectorRepository>();
        builder.Services.AddScoped<AirQualityVectorService>();
        builder.Services.AddScoped<UsAirQualityService>();
        builder.Services.AddScoped<EuAirQualityService>();

        builder.Services.AddScoped<ConfigRepository>();
        builder.Services.AddScoped<ConfigService>();
        
        // mappers 
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        // servizio per creare la griglia dei punti di misurazione
        builder.Services.AddScoped<MeasurementPointsService>();
        
        // OpenStreetMap tasks
        builder.Services.AddSingleton<MeasurementPointsTasks>();
        builder.Services.AddSingleton<SeedFeaturesTasks>();
        builder.Services.AddSingleton<AirQualityTasks>();
        builder.Services.AddHostedService<OsmBackgroundTasks>();
        
        return builder;
    }
}