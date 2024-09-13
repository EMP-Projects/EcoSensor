using EcoSensorApi.MeasurementPoints;

namespace EcoSensorApi.Tasks.Osm;

/// <inheritdoc />
public class GeoJsonToS3Tasks : OsmTasks<MeasurementPointsService>
{
    /// <inheritdoc />
    public GeoJsonToS3Tasks(IServiceProvider serviceProvider, ILogger<OsmTasks<MeasurementPointsService>> logger) : base(serviceProvider, logger)
    {
    }

    /// <inheritdoc />
    public override string Name => $"{nameof(GeoJsonToS3Tasks)} Task";
    
    /// <inheritdoc />
    public override TimeSpan? Period { get; set; } = TimeSpan.FromMinutes(15);

    /// <inheritdoc />
    public override TimeSpan? DueTime { get; set; } = TimeSpan.FromSeconds(15);

    /// <inheritdoc />
    public override async Task HandleNotificationsAsync()
    {
        await Service().SerializeAndUploadToS3Async();
    }
}