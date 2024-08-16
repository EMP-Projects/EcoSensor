using EcoSensorApi.AirQuality.Vector;
using Gis.Net.Core.Services;

namespace EcoSensorApi.AirQuality.Properties;

public class AirQualityPropertiesService : ServiceCore<AirQualityPropertiesModel, 
    AirQualityPropertiesDto, 
    AirQualityPropertiesQuery, 
    AirQualityPropertiesRequest, 
    EcoSensorDbContext>
{

    /// <inheritdoc />
    public AirQualityPropertiesService(
        ILogger<AirQualityPropertiesService> logger, 
        AirQualityPropertiesRepository propertiesRepository) : 
        base(logger, propertiesRepository)
    {
    }
}