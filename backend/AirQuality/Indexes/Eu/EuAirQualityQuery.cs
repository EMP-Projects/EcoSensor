using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.AirQuality.Indexes.Eu;

public class EuAirQualityQuery : QueryBase, IAirQualityLevelQuery
{
    [FromQuery(Name="pollution")]
    public EPollution Pollution { get; set; }
    
    [FromQuery(Name="level")]
    public EEuAirQualityLevel Level { get; set; }
    
    [FromQuery(Name="value")]
    public double Value { get; set; }
}