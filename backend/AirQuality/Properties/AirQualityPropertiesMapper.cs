using Gis.Net.Core.Mapper;

namespace EcoSensorApi.AirQuality.Properties;

public class AirQualityPropertiesMapper : AbstractMapperProfile<AirQualityPropertiesModel, AirQualityPropertiesDto, AirQualityPropertiesRequest>
{
    public AirQualityPropertiesMapper()
    {
        ModelToDtoMapper.ForMember(dest => dest.SourceText, opt => opt.Ignore());
        ModelToDtoMapper.ForMember(dest => dest.PollutionText, opt => opt.Ignore());
    }
}