using TeamSviluppo.Tasks.Notification;
namespace EcoSensorApi.Tasks.Osm;

public class OsmBackgroundTasks : BackgroundService
{
    private readonly NotificationService _notificationService;
    private readonly MeasurementPointsTasks _measurementPointsTasks;
    private readonly SeedFeaturesTasks _seedFeaturesTasks;
    private readonly AirQualityTasks _airQualityTasks;

    /// <inheritdoc />
    public OsmBackgroundTasks(
        MeasurementPointsTasks measurementPointsTasks, 
        NotificationService notificationService, 
        SeedFeaturesTasks seedFeaturesTasks, 
        AirQualityTasks airQualityTasks)
    {
        _measurementPointsTasks = measurementPointsTasks;
        _notificationService = notificationService;
        _seedFeaturesTasks = seedFeaturesTasks;
        _airQualityTasks = airQualityTasks;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _notificationService.AddNotificationHandler(_measurementPointsTasks);
        _notificationService.AddNotificationHandler(_seedFeaturesTasks);
        _notificationService.AddNotificationHandler(_airQualityTasks);
        return Task.CompletedTask;
    }
}