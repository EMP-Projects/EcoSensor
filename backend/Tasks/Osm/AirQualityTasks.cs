using EcoSensorApi.MeasurementPoints;
namespace EcoSensorApi.Tasks.Osm;

/// <inheritdoc />
public class AirQualityTasks : OsmTasks<MeasurementPointsService>
{

    /// <inheritdoc />
    public AirQualityTasks(IServiceProvider serviceProvider, ILogger<OsmTasks<MeasurementPointsService>> logger) : base(serviceProvider, logger)
    {
    }
    
    public override string Name => $"{nameof(AirQualityTasks)} Task";
    public override TimeSpan? Period { get; set; } = TimeSpan.FromHours(1);
    public override TimeSpan? DueTime { get; set; } = TimeSpan.FromMinutes(5);

    public override async Task HandleNotificationsAsync()
    {
        var result = await Service().AirQuality();
        Logger.LogInformation($"Sono stati registrati {result} nuovi dati di qualità dell'aria");
    }
}