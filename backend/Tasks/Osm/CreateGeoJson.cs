using EcoSensorApi.MeasurementPoints;

namespace EcoSensorApi.Tasks.Osm;

/// <inheritdoc />
public class CreateGeoJson : OsmTasks<MeasurementPointsService>
{
    /// <inheritdoc />
    public CreateGeoJson(IServiceProvider serviceProvider, ILogger<OsmTasks<MeasurementPointsService>> logger) : base(serviceProvider, logger)
    {
    }

    /// <inheritdoc />
    public override string Name => $"{nameof(CreateGeoJson)} Task";
    
    /// <inheritdoc />
    public override TimeSpan? Period { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Gets or sets the due time for the task.
    /// </summary>
    public override TimeSpan? DueTime { get; set; } = TimeSpan.FromSeconds(4);

    /// <inheritdoc />
    public override async Task HandleNotificationsAsync()
    {
        // Upload the feature collection to the server
        await Service().UploadFeatureCollection();
    }
}