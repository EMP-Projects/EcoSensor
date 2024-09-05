using EcoSensorApi.MeasurementPoints;
using Gis.Net.Core.Tasks.Notification;

namespace EcoSensorApi.Tasks.Osm;

/// <summary>
/// Represents an abstract base class for OSM tasks that handle notifications.
/// </summary>
/// <typeparam name="TService">The type of the measurement points service.</typeparam>
public abstract class OsmTasks<TService> : INotificationHandler where TService : IMeasurementPointsService
{
    /// <summary>
    /// The service provider used to create service scopes.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// The logger used for logging information.
    /// </summary>
    protected readonly ILogger<OsmTasks<TService>> Logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="OsmTasks{TService}"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="logger">The logger.</param>
    protected OsmTasks(
        IServiceProvider serviceProvider, 
        ILogger<OsmTasks<TService>> logger)
    {
        _serviceProvider = serviceProvider;
        Logger = logger;
    }

    /// <summary>
    /// Creates a new service scope and gets the required service.
    /// </summary>
    /// <returns>The required service of type <typeparamref name="TService"/>.</returns>
    protected TService Service()
    {
        var scope = _serviceProvider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<TService>();
    }

    /// <summary>
    /// Gets or sets the period for the task.
    /// </summary>
    public virtual TimeSpan? Period { get; set; } = TimeSpan.FromDays(1);

    /// <summary>
    /// Gets or sets the due time for the task.
    /// </summary>
    public virtual TimeSpan? DueTime { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Gets or sets the delay on error for the task.
    /// </summary>
    public virtual TimeSpan? DelayOnError { get; set; } = TimeSpan.FromHours(1);

    /// <summary>
    /// Handles notifications asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public abstract Task HandleNotificationsAsync();

    /// <summary>
    /// Gets the name of the task.
    /// </summary>
    public virtual string Name => $"{nameof(OsmTasks<TService>)} Task";
}