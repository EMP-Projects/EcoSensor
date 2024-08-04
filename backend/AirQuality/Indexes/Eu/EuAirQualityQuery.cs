using Microsoft.AspNetCore.Mvc;
using TeamSviluppo.DTO;
namespace EcoSensorApi.AirQuality.Indexes.Eu;

public class EuAirQualityQuery : QueryByParamsBase, IAirQualityLevelQuery
{
    [FromQuery(Name="pollution")]
    public EPollution Pollution { get; set; }
    
    [FromQuery(Name="level")]
    public EEuAirQualityLevel Level { get; set; }
    
    [FromQuery(Name="value")]
    public double Value { get; set; }
}