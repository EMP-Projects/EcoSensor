namespace EcoSensorApi.AirQuality;

/// <summary>
/// Provides methods to retrieve pollution information and descriptions.
/// </summary>
public static class Pollution
{
    /// <summary>
    /// Gets the name of the pollution based on the provided key.
    /// </summary>
    /// <param name="key">The pollution key.</param>
    /// <returns>The name of the pollution.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the key is not a valid pollution type.</exception>
    public static string GetPollution(EPollution key)
    {
        var k = Enum.GetName(key);
        if (k is null)
            throw new ArgumentOutOfRangeException(nameof(key), key, "Invalid pollutant");
        return k;
    }

    /// <summary>
    /// Gets the description of the pollution based on the provided key.
    /// </summary>
    /// <param name="key">The pollution key.</param>
    /// <returns>The description of the pollution.</returns>
    public static string GetPollutionDescription(EPollution key)
    {
        return key switch
        {
            EPollution.SulphurDioxide => "Diossido di zolfo",
            EPollution.Ozone => "Ozono",
            EPollution.Pm25 => "Particelle sull'aria (PM2.5)",
            EPollution.Pm10 => "Particelle sull'aria (PM10)",
            EPollution.CarbonMonoxide => "Monossido di carbonio",
            EPollution.NitrogenDioxide => "Diossido di azoto",
            _ => string.Empty
        };
    }
    
    /// <summary>
    /// Gets the source of the pollution based on the provided key.
    /// </summary>
    /// <param name="key">The air quality source key.</param>
    /// <returns>The source of the pollution.</returns>
    public static string GetPollutionSource(EAirQualitySource key)
    {
        return key switch
        {
            EAirQualitySource.OpenMeteo => "OpenMeteo Api",
            EAirQualitySource.Iot => "Dispositivo Iot",
            _ => string.Empty
        };
    }

    /// <summary>
    /// Gets all pollution names.
    /// </summary>
    /// <returns>An enumerable of pollution names.</returns>
    public static IEnumerable<string> GetPollutions() => Enum.GetNames<EPollution>();
    
    /// <summary>
    /// Gets all pollution values.
    /// </summary>
    /// <returns>An enumerable of pollution values.</returns>
    public static IEnumerable<EPollution> GetValues() => Enum.GetValues<EPollution>();
    
}