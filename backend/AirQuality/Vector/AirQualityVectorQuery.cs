using Gis.Net.Vector.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.AirQuality.Vector;

public class AirQualityVectorQuery : GisVectorQuery
{
    [FromQuery(Name = "sourceData")]
    public ESourceData SourceData { get; set; }
}