using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EcoSensorApi.AirQuality.Properties;

public class AirQualityPropertiesQuery : QueryBase, IAirQualityPropertiesQuery
{
    [FromQuery(Name="pollution")]
    public EPollution? Pollution { get; set; }
    
    [FromQuery(Name="source")]
    public EAirQualitySource? Source { get; set; }
    
    [FromQuery(Name="latitude")]
    public double? Latitude { get; set; }
    
    [FromQuery(Name="longitude")]
    public double? Longitude { get; set; }

    [FromQuery(Name="gisId")]
    public long? GisId { get; set; }
    
    [FromQuery(Name="begin")]
    public DateTime? Begin { get; set; }
    
    [FromQuery(Name="end")]
    public DateTime? End { get; set; }
}