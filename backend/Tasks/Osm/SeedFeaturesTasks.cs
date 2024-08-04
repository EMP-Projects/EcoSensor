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
        Logger.LogInformation($"Sono stati trovati {result} nuove features geografiche");
    }

    public override TimeSpan? DueTime { get; set; } = TimeSpan.FromMinutes(5);

    public override string Name => $"{nameof(SeedFeaturesTasks)} Task";
}