using EcoSensorApi.AirQuality.Properties;
using Gis.Net.Vector.Mapper;

namespace EcoSensorApi.AirQuality.Vector;

/// <inheritdoc />
public class AirQualityVectorMapper : GisProfileManyMapper<AirQualityVectorModel, AirQualityVectorDto, AirQualityVectorRequest, AirQualityPropertiesModel, AirQualityPropertiesDto>
{
    /// <inheritdoc />
    public AirQualityVectorMapper()
    {
        GisVectorModelToDtoMapper.ForMember(dest => dest.EntityVector, opt => opt.Ignore());
    }
}