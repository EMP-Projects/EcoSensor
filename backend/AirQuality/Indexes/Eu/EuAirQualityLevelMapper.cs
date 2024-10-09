using Gis.Net.Core.Mapper;

namespace EcoSensorApi.AirQuality.Indexes.Eu;

/// <inheritdoc />
public class EuAirQualityLevelMapper : AbstractMapperProfile<EuAirQualityLevel, EuAirQualityLevelDto, EuAirQualityLevelRequest>
{
    /// <inheritdoc />
    public EuAirQualityLevelMapper()
    {
        ModelToDtoMapper
            .ForMember(dest => dest.LevelName, opt 
                => opt.MapFrom(src => AirQualityIndex.EuLevelName(src.Level)))
            .ForMember(dest => dest.PollutionText, opt
                => opt.MapFrom(src => Pollution.GetPollution(src.Pollution)));
    }
}