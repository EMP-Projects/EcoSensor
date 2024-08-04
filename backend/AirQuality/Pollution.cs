namespace EcoSensorApi.AirQuality;

public static class Pollution
{
    public static string GetPollution(EPollution key)
    {
        var k = Enum.GetName(key);
        if (k is null)
            throw new ArgumentOutOfRangeException(nameof(key), key, "Inquinante non valido");
        return k;
    }

    public static string GetPollutionDescription(EPollution key)
    {
        return key switch
        {
            EPollution.SulphurDioxide => "Diossido di zolfo",
            EPollution.Ammonia => "Ammoniaca",
            EPollution.Dust => "Polvere",
            EPollution.Ozone => "Ozono",
            EPollution.Pm25 => "Particelle sull'aria (PM2.5)",
            EPollution.Pm10 => "Particelle sull'aria (PM10)",
            EPollution.CarbonMonoxide => "Monossido di carbonio",
            EPollution.NitrogenDioxide => "Diossido di azoto",
            EPollution.AerosolOpticalDepth => "Aerosol Ottico Profondità",
            EPollution.UvIndex => "Indice UV",
            EPollution.UvIndexClearSky => "Indice UV Cielo sereno",
            EPollution.AlderPollen => "Età del polline",
            EPollution.BirchPollen => "Polline di betulla",
            EPollution.GrassPollen => "Polline d'erba",
            EPollution.MugwortPollen => "Polline di artemisia",
            EPollution.OlivePollen => "Polline d'oliva",
            EPollution.RagweedPollen => "Polline di ambrosia",
            _ => string.Empty
        };
    }
    
    public static string GetPollutionSource(EAirQualitySource key)
    {
        return key switch
        {
            EAirQualitySource.OpenMeteo => "OpenMeteo Api",
            EAirQualitySource.Iot => "Dispositivo Iot",
            _ => string.Empty
        };
    }

    public static IEnumerable<string> GetPollutions() => Enum.GetNames<EPollution>();
}