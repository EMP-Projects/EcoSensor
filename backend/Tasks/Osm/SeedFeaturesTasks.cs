using EcoSensorApi.MeasurementPoints;
namespace EcoSensorApi.Tasks.Osm;

public class SeedFeaturesTasks : OsmTasks<MeasurementPointsService>
{

    /// <inheritdoc />
    public SeedFeaturesTasks(IServiceProvider serviceProvider, ILogger<OsmTasks<MeasurementPointsService>> logger) : base(serviceProvider, logger)
    {
    }
    
    public override async Task HandleNotificationsAsync()
    {
        var result = await Service().SeedFeatures();
        var msg = $"Sono stati trovati {result} nuove features geografiche";
        Logger.LogInformation(msg);
    }

    public override TimeSpan? DueTime { get; set; } = TimeSpan.FromSeconds(5);

    public override string Name => $"{nameof(SeedFeaturesTasks)} Task";
}