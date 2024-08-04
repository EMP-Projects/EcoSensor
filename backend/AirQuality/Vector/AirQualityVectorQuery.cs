using Microsoft.AspNetCore.Mvc;
using TeamSviluppo.Gis.NetCoreFw.Dto;
namespace EcoSensorApi.AirQuality.Vector;

public class AirQualityVectorQuery : GisCoreQueryByParams
{
    [FromQuery(Name = "sourceData")]
    public ESourceData SourceData { get; set; }
}