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
    public override TimeSpan? Period { get; set; } = TimeSpan.FromMinutes(20);

    /// <summary>
    /// Gets or sets the due time for the task.
    /// </summary>
    public override TimeSpan? DueTime { get; set; } = TimeSpan.FromSeconds(20);

    /// <inheritdoc />
    public override async Task HandleNotificationsAsync()
    {
        await Service().UploadFeatureCollection();
    }
}