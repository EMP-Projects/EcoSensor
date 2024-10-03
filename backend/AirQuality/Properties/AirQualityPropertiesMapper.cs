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
        ModelToDtoMapper
            .ForMember(dest => dest.SourceText, opt => opt.MapFrom(src => Pollution.GetPollutionSource(src.Source)))
            .ForMember(dest => dest.PollutionText,
                opt => opt.MapFrom(src => Pollution.GetPollutionDescription(src.Pollution)))
            .ForMember(dest => dest.Gis, opt => opt.Ignore())
            .ForMember(dest => dest.Lat, opt => opt.Ignore())
            .ForMember(dest => dest.Lng, opt => opt.Ignore())
            .ForMember(dest => dest.EntityKey, opt => opt.Ignore())
            .ForMember(dest => dest.TimeStamp, opt => opt.Ignore())
            .ForMember(dest => dest.EuropeanAqiText, opt => opt.Ignore())
            .ForMember(dest => dest.UsAqiText, opt => opt.Ignore());
    }
}