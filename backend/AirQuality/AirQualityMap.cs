using System.Text.Json.Serialization;

namespace EcoSensorApi.AirQuality;

/// <inheritdoc cref="EcoSensorApi.AirQuality.IAirQualityMap" />
public class AirQualityMap : DataMap, IAirQualityMap
{
    /// <inheritdoc />
    public AirQualityMap(string entityKey, ETypeMonitoringData typeMonitoringData, EPollution pollution) : base(entityKey, typeMonitoringData)
    {
        Prefix = "air_quality";
        Pollution = (int)pollution;
        PollutionDescription = AirQuality.Pollution.GetPollution(pollution);
        Data = $"{entityKey.ToLower()}_{AirQuality.Pollution.GetPollution(pollution).ToLower()}.json";
    }

    /// <inheritdoc />
    [JsonPropertyName("pollution")]
    public int Pollution { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("pollutionDescription")]
    public string PollutionDescription { get; set; }
}