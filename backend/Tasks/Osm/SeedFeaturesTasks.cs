using EcoSensorApi.MeasurementPoints;
namespace EcoSensorApi.Tasks.Osm;

/// <inheritdoc />
public class SeedFeaturesTasks : OsmTasks<MeasurementPointsService>
{

    /// <inheritdoc />
    public SeedFeaturesTasks(IServiceProvider serviceProvider, ILogger<OsmTasks<MeasurementPointsService>> logger) : base(serviceProvider, logger)
    {
    }

    /// <summary>
    /// Handles notifications asynchronously.
    /// </summary>
    /// <returns>An awaitable task.</returns>
    public override async Task HandleNotificationsAsync()
    {
        var result = await Service().SeedFeatures();
        var msg = $"{result} new geographic features have been found";
        Logger.LogInformation(msg);
    }

    /// <summary>
    /// Gets or sets the due time for the task.
    /// </summary>
    public override TimeSpan? DueTime { get; set; } = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Gets the name of the Osm task.
    /// </summary>
    public override string Name => $"{nameof(SeedFeaturesTasks)} Task";
}