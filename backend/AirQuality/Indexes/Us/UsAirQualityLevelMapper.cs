using TeamSviluppo.Mapper;
namespace EcoSensorApi.AirQuality.Indexes.Us;

public class UsAirQualityLevelMapper : AbstractMapperProfile<UsAirQualityLevelDto, UsAirQualityLevel>
{
    public UsAirQualityLevelMapper()
    {
        ModelToDtoMapper
            .ForMember(dest => dest.LevelName, opt 
                => opt.MapFrom(src => AirQualityIndex.UsLevelName(src.Level)))
            .ForMember(dest => dest.Pollution, opt
                => opt.MapFrom(src => Pollution.GetPollution(src.Pollution)));
    }
}