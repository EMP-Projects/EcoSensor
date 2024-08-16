using EcoSensorApi.MeasurementPoints;
using Gis.Net.Core.Tasks.Notification;

namespace EcoSensorApi.Tasks.Osm;

public abstract class OsmTasks<TService> : INotificationHandler where TService : IMeasurementPointsService
{

    private readonly IServiceProvider _serviceProvider;
    protected readonly ILogger<OsmTasks<TService>> Logger;
    
    protected OsmTasks(
        IServiceProvider serviceProvider, 
        ILogger<OsmTasks<TService>> logger)
    {
        _serviceProvider = serviceProvider;
        Logger = logger;
    }

    protected TService Service()
    {
        var scope = _serviceProvider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<TService>();
    }

    public virtual TimeSpan? Period { get; set; } = TimeSpan.FromDays(1);
    public virtual TimeSpan? DueTime { get; set; } = TimeSpan.FromSeconds(1);
    public virtual TimeSpan? DelayOnError { get; set; } = TimeSpan.FromHours(1);
    public abstract Task HandleNotificationsAsync();
    public virtual string Name => $"{nameof(OsmTasks<TService>)} Task";
}