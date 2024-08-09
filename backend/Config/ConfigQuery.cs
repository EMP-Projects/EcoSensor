using Microsoft.AspNetCore.Mvc;
using TeamSviluppo.DTO;

namespace EcoSensorApi.Config;

public class ConfigQuery : QueryByParamsBase
{
    [FromQuery(Name = "typeSource")]
    public ETypeSourceLayer? TypeSource { get; set; }
    
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
    
    [FromQuery(Name = "regionField")]
    public string? RegionField { get; set; }
    
    [FromQuery(Name = "regionCode")]
    public int? RegionCode { get; set; }
    
    [FromQuery(Name = "cityField")]
    public string? CityField { get; set; }
    
    [FromQuery(Name = "cityCode")]
    public int? CityCode { get; set; }
}