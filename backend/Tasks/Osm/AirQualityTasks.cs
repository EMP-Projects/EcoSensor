using EcoSensorApi.MeasurementPoints;
namespace EcoSensorApi.Tasks.Osm;

/// <summary>
/// Represents tasks related to air quality in the OSM context.
/// </summary>
public class AirQualityTasks : OsmTasks<MeasurementPointsService>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AirQualityTasks"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="logger">The logger.</param>
    public AirQualityTasks(IServiceProvider serviceProvider, ILogger<OsmTasks<MeasurementPointsService>> logger) : base(serviceProvider, logger)
    {
    }
    
    /// <summary>
    /// Gets the name of the task.
    /// </summary>
    public override string Name => $"{nameof(AirQualityTasks)} Task";

    /// <summary>
    /// Gets or sets the period for the task.
    /// </summary>
    public override TimeSpan? Period { get; set; } = TimeSpan.FromHours(1);

    /// <summary>
    /// Gets or sets the due time for the task.
    /// </summary>
    public override TimeSpan? DueTime { get; set; } = TimeSpan.FromMinutes(3);

    /// <summary>
    /// Handles notifications asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public override async Task HandleNotificationsAsync()
    {
        var result = await Service().AirQuality();
        Logger.LogInformation($"{result} new air quality data has been recorded");
    }
}