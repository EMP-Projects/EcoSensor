using Gis.Net.Core.Mapper;

namespace EcoSensorApi.AirQuality.Indexes.Eu;

public class EuAirQualityLevelMapper : AbstractMapperProfile<EuAirQualityLevel, EuAirQualityLevelDto, EuAirQualityLevelRequest>
{
    public EuAirQualityLevelMapper()
    {
        ModelToDtoMapper
            .ForMember(dest => dest.LevelName, opt 
                => opt.MapFrom(src => AirQualityIndex.EuLevelName(src.Level)))
            .ForMember(dest => dest.Pollution, opt
                => opt.MapFrom(src => Pollution.GetPollution(src.Pollution)));
    }
}