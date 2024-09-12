using EcoSensorApi.MeasurementPoints;

namespace EcoSensorApi.Tasks.Osm;

/// <inheritdoc />
public class DeleteOldDataTasks : OsmTasks<MeasurementPointsService>
{
    /// <inheritdoc />
    public DeleteOldDataTasks(IServiceProvider serviceProvider, ILogger<DeleteOldDataTasks> logger) : base(serviceProvider, logger)
    {
    }

    /// <inheritdoc />
    public override string Name => $"{nameof(DeleteOldDataTasks)} Task";

    /// <inheritdoc />
    public override TimeSpan? DueTime { get; set; } = TimeSpan.FromMinutes(5);

    /// <inheritdoc />
    public override async Task HandleNotificationsAsync()
    {
        var result = await Service().DeleteOldRecords();
        Logger.LogInformation($"{result} old data has been deleted");
    }
}