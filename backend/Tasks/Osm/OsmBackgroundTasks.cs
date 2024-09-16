using Gis.Net.Core.Tasks.Notification;

namespace EcoSensorApi.Tasks.Osm;

/// <summary>
/// Represents a background service for handling OSM-related tasks.
/// </summary>
public class OsmBackgroundTasks : BackgroundService
{
    /// <summary>
    /// The notification service used to handle notifications.
    /// </summary>
    private readonly NotificationService _notificationService;

    /// <summary>
    /// The tasks related to measurement points.
    /// </summary>
    private readonly MeasurementPointsTasks _measurementPointsTasks;

    /// <summary>
    /// The tasks related to seeding features.
    /// </summary>
    private readonly SeedFeaturesTasks _seedFeaturesTasks;

    /// <summary>
    /// The tasks related to air quality.
    /// </summary>
    private readonly AirQualityTasks _airQualityTasks;
    
    /// <summary>
    /// The tasks related to deleting old data.
    /// </summary>
    private readonly DeleteOldDataTasks _deleteOldDataTasks;
    
    /// <summary>
    /// The tasks related to handling GeoJSON data and uploading it to S3.
    /// </summary>
    private readonly CreateGeoJson _createGeoJson;

    /// <summary>
    /// Initializes a new instance of the <see cref="OsmBackgroundTasks"/> class.
    /// </summary>
    /// <param name="measurementPointsTasks">The measurement points tasks.</param>
    /// <param name="notificationService">The notification service.</param>
    /// <param name="seedFeaturesTasks">The seed features tasks.</param>
    /// <param name="airQualityTasks">The air quality tasks.</param>
    /// <param name="deleteOldDataTasks">The tasks related to deleting old data.</param>
    /// <param name="createGeoJson">The tasks related to handling GeoJSON data and uploading it to S3.</param>
    public OsmBackgroundTasks(
        MeasurementPointsTasks measurementPointsTasks, 
        NotificationService notificationService, 
        SeedFeaturesTasks seedFeaturesTasks, 
        AirQualityTasks airQualityTasks, 
        DeleteOldDataTasks deleteOldDataTasks, 
        CreateGeoJson createGeoJson)
    {
        _measurementPointsTasks = measurementPointsTasks;
        _notificationService = notificationService;
        _seedFeaturesTasks = seedFeaturesTasks;
        _airQualityTasks = airQualityTasks;
        _deleteOldDataTasks = deleteOldDataTasks;
        _createGeoJson = createGeoJson;
    }

    /// <summary>
    /// Executes the background service asynchronously.
    /// </summary>
    /// <param name="stoppingToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // _notificationService.AddNotificationHandler(_measurementPointsTasks);
        // _notificationService.AddNotificationHandler(_seedFeaturesTasks);
        // _notificationService.AddNotificationHandler(_airQualityTasks);
        // _notificationService.AddNotificationHandler(_deleteOldDataTasks);
        _notificationService.AddNotificationHandler(_createGeoJson);
        return Task.CompletedTask;
    }
}