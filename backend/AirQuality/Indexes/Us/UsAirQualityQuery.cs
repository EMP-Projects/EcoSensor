using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.AirQuality.Indexes.Us;

public class UsAirQualityQuery : QueryBase, IAirQualityLevelQuery
{
    [FromQuery(Name="pollution")]
    public EPollution Pollution { get; set; }
    
    [FromQuery(Name="level")]
    public EUsAirQualityLevel Level { get; set; }
    
    [FromQuery(Name="value")]
    public double Value { get; set; }
}