using Gis.Net.Core.Mapper;

namespace EcoSensorApi.AirQuality.Indexes.Us;

/// <inheritdoc />
public class UsAirQualityLevelMapper : AbstractMapperProfile<UsAirQualityLevel, UsAirQualityLevelDto, UsAirQualityLevelRequest>
{
    /// <inheritdoc />
    public UsAirQualityLevelMapper()
    {
        ModelToDtoMapper
            .ForMember(dest => dest.LevelName, opt 
                => opt.MapFrom(src => AirQualityIndex.UsLevelName(src.Level)))
            .ForMember(dest => dest.Pollution, opt
                => opt.MapFrom(src => Pollution.GetPollution(src.Pollution)));
    }
}