using EcoSensorApi.MeasurementPoints;
namespace EcoSensorApi.Tasks.Osm;

/// <summary>
/// Represents tasks related to measurement points in the OSM context.
/// </summary>
public class MeasurementPointsTasks : OsmTasks<MeasurementPointsService>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MeasurementPointsTasks"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="logger">The logger.</param>
    public MeasurementPointsTasks(
        IServiceProvider serviceProvider, 
        ILogger<MeasurementPointsTasks> logger) : base(serviceProvider, logger)
    {
    }

    /// <summary>
    /// Gets the name of the task.
    /// </summary>
    public override string Name => $"{nameof(MeasurementPointsTasks)} Task";

    /// <summary>
    /// Handles notifications asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async Task HandleNotificationsAsync()
    {
        var result = await Service().MeasurementPoints();
        Logger.LogInformation($"Sono stati trovati {result} nuovi punti di misurazione");
    }

    /// <summary>
    /// Gets or sets the due time for the task.
    /// </summary>
    public override TimeSpan? DueTime { get; set; } = TimeSpan.FromMinutes(2);
}