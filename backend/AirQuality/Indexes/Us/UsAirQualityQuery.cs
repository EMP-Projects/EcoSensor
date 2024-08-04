using Microsoft.AspNetCore.Mvc;
using TeamSviluppo.DTO;
namespace EcoSensorApi.AirQuality.Indexes.Us;

public class UsAirQualityQuery : QueryByParamsBase, IAirQualityLevelQuery
{
    [FromQuery(Name="pollution")]
    public EPollution Pollution { get; set; }
    
    [FromQuery(Name="level")]
    public EUsAirQualityLevel Level { get; set; }
    
    [FromQuery(Name="value")]
    public double Value { get; set; }
}