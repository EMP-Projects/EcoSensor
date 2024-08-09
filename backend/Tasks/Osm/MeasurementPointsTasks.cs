using EcoSensorApi.MeasurementPoints;
namespace EcoSensorApi.Tasks.Osm;

public class MeasurementPointsTasks : OsmTasks<MeasurementPointsService>
{

    /// <inheritdoc />
    public MeasurementPointsTasks(
        IServiceProvider serviceProvider, 
        ILogger<MeasurementPointsTasks> logger) : base(serviceProvider, logger)
    {
    }

    public override string Name => $"{nameof(MeasurementPointsTasks)} Task";

    public override async Task HandleNotificationsAsync()
    {
        var result = await Service().MeasurementPoints();
        Logger.LogInformation($"Sono stati trovati {result} nuovi punti di misurazione");
    }

    public override TimeSpan? DueTime { get; set; } = TimeSpan.FromMinutes(2);
}