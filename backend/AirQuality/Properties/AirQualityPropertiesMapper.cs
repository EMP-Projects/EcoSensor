using TeamSviluppo.Mapper;
namespace EcoSensorApi.AirQuality.Properties;

public class AirQualityPropertiesMapper : AbstractMapperProfile<AirQualityPropertiesDto, AirQualityPropertiesModel>
{
    public AirQualityPropertiesMapper()
    {
        ModelToDtoMapper.ForMember(dest => dest.SourceText, opt => opt.Ignore());
        ModelToDtoMapper.ForMember(dest => dest.PollutionText, opt => opt.Ignore());
    }
}