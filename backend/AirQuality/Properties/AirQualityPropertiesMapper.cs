using Gis.Net.Core.Mapper;

namespace EcoSensorApi.AirQuality.Properties;

/// <summary>
/// Maps air quality properties between the model, DTO, and request objects.
/// </summary>
public class AirQualityPropertiesMapper : AbstractMapperProfile<AirQualityPropertiesModel, AirQualityPropertiesDto, AirQualityPropertiesRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AirQualityPropertiesMapper"/> class.
    /// </summary>
    public AirQualityPropertiesMapper()
    {
        ModelToDtoMapper.ForMember(dest => dest.SourceText, opt => opt.Ignore());
        ModelToDtoMapper.ForMember(dest => dest.PollutionText, opt => opt.Ignore());
    }
}