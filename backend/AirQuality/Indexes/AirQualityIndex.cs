using EcoSensorApi.AirQuality.Indexes.Eu;
using EcoSensorApi.AirQuality.Indexes.Us;
namespace EcoSensorApi.AirQuality.Indexes;

public static class AirQualityIndex
{
    public static List<EuAirQualityLevel> EuIndexes { get; set; } =
    [
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Good,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 0,
            Max = 10,
            Color = EuLevelColor(EEuAirQualityLevel.Good),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Fair,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 10,
            Max = 20,
            Color = EuLevelColor(EEuAirQualityLevel.Fair),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Moderate,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 20,
            Max = 25,
            Color = EuLevelColor(EEuAirQualityLevel.Moderate),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Poor,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 25,
            Max = 50,
            Color = EuLevelColor(EEuAirQualityLevel.Poor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.VeryPoor,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 50,
            Max = 75,
            Color = EuLevelColor(EEuAirQualityLevel.VeryPoor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.ExtremelyPoor,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 75,
            Max = 800,
            Color = EuLevelColor(EEuAirQualityLevel.ExtremelyPoor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Good,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm10,
            Min = 0,
            Max = 20,
            Color = EuLevelColor(EEuAirQualityLevel.Good),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Fair,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm10,
            Min = 20,
            Max = 40,
            Color = EuLevelColor(EEuAirQualityLevel.Fair),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Moderate,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm10,
            Min = 40,
            Max = 50,
            Color = EuLevelColor(EEuAirQualityLevel.Moderate),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Poor,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm10,
            Min = 50,
            Max = 100,
            Color = EuLevelColor(EEuAirQualityLevel.Poor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.VeryPoor,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 100,
            Max = 150,
            Color = EuLevelColor(EEuAirQualityLevel.VeryPoor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.ExtremelyPoor,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 150,
            Max = 1200,
            Color = EuLevelColor(EEuAirQualityLevel.ExtremelyPoor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Good,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 0,
            Max = 40,
            Color = EuLevelColor(EEuAirQualityLevel.Good),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Fair,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 40,
            Max = 90,
            Color = EuLevelColor(EEuAirQualityLevel.Fair),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Moderate,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 90,
            Max = 120,
            Color = EuLevelColor(EEuAirQualityLevel.Moderate),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Poor,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 120,
            Max = 230,
            Color = EuLevelColor(EEuAirQualityLevel.Poor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.VeryPoor,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 230,
            Max = 340,
            Color = EuLevelColor(EEuAirQualityLevel.VeryPoor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.ExtremelyPoor,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 340,
            Max = 1000,
            Color = EuLevelColor(EEuAirQualityLevel.ExtremelyPoor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Good,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.Ozone,
            Min = 0,
            Max = 50,
            Color = EuLevelColor(EEuAirQualityLevel.Good),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Fair,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.Ozone,
            Min = 50,
            Max = 100,
            Color = EuLevelColor(EEuAirQualityLevel.Fair),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Moderate,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.Ozone,
            Min = 100,
            Max = 130,
            Color = EuLevelColor(EEuAirQualityLevel.Moderate),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Poor,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.Ozone,
            Min = 130,
            Max = 240,
            Color = EuLevelColor(EEuAirQualityLevel.Poor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.VeryPoor,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.Ozone,
            Min = 240,
            Max = 380,
            Color = EuLevelColor(EEuAirQualityLevel.VeryPoor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.ExtremelyPoor,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.Ozone,
            Min = 380,
            Max = 800,
            Color = EuLevelColor(EEuAirQualityLevel.ExtremelyPoor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Good,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 0,
            Max = 100,
            Color = EuLevelColor(EEuAirQualityLevel.Good),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Fair,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 100,
            Max = 200,
            Color = EuLevelColor(EEuAirQualityLevel.Fair),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Moderate,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 200,
            Max = 350,
            Color = EuLevelColor(EEuAirQualityLevel.Moderate),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.Poor,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 350,
            Max = 500,
            Color = EuLevelColor(EEuAirQualityLevel.Poor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.VeryPoor,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 500,
            Max = 750,
            Color = EuLevelColor(EEuAirQualityLevel.VeryPoor),
            Unit = "μg/m3"
        },
        new EuAirQualityLevel
        {
            Level = EEuAirQualityLevel.ExtremelyPoor,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 750,
            Max = 1250,
            Color = EuLevelColor(EEuAirQualityLevel.ExtremelyPoor),
            Unit = "μg/m3"
        }
    ];
    
    public static List<UsAirQualityLevel> UsIndexes { get; set; } =
    [
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Good,
            Period = TimeSpan.FromHours(8),
            Pollution = EPollution.Ozone,
            Min = 0,
            Max = 55,
            Color = UsLevelColor(EUsAirQualityLevel.Good),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Moderate,
            Period = TimeSpan.FromHours(8),
            Pollution = EPollution.Ozone,
            Min = 55,
            Max = 70,
            Color = UsLevelColor(EUsAirQualityLevel.Moderate),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.UnhealthyForSensitiveGroups,
            Period = TimeSpan.FromHours(8),
            Pollution = EPollution.Ozone,
            Min = 70,
            Max = 85,
            Color = UsLevelColor(EUsAirQualityLevel.UnhealthyForSensitiveGroups),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Unhealthy,
            Period = TimeSpan.FromHours(8),
            Pollution = EPollution.Ozone,
            Min = 85,
            Max = 105,
            Color = UsLevelColor(EUsAirQualityLevel.Unhealthy),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.VeryUnhealthy,
            Period = TimeSpan.FromHours(8),
            Pollution = EPollution.Ozone,
            Min = 105,
            Max = 200,
            Color = UsLevelColor(EUsAirQualityLevel.VeryUnhealthy),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.UnhealthyForSensitiveGroups,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.Ozone,
            Min = 125,
            Max = 165,
            Color = UsLevelColor(EUsAirQualityLevel.UnhealthyForSensitiveGroups),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Unhealthy,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.Ozone,
            Min = 165,
            Max = 205,
            Color = UsLevelColor(EUsAirQualityLevel.Unhealthy),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.VeryUnhealthy,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.Ozone,
            Min = 205,
            Max = 405,
            Color = UsLevelColor(EUsAirQualityLevel.VeryUnhealthy),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Hazardous,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.Ozone,
            Min = 405,
            Max = 605,
            Color = UsLevelColor(EUsAirQualityLevel.Hazardous),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Good,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 0,
            Max = 12,
            Color = UsLevelColor(EUsAirQualityLevel.Good),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Moderate,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 12,
            Max = 35.5,
            Color = UsLevelColor(EUsAirQualityLevel.Moderate),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.UnhealthyForSensitiveGroups,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 35.5,
            Max = 55.5,
            Color = UsLevelColor(EUsAirQualityLevel.UnhealthyForSensitiveGroups),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Unhealthy,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 55.5,
            Max = 105.5,
            Color = UsLevelColor(EUsAirQualityLevel.Unhealthy),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.VeryUnhealthy,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 150.5,
            Max = 250.5,
            Color = UsLevelColor(EUsAirQualityLevel.VeryUnhealthy),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Hazardous,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm25,
            Min = 250.5,
            Max = 500.5,
            Color = UsLevelColor(EUsAirQualityLevel.Hazardous),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Good,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm10,
            Min = 0,
            Max = 55,
            Color = UsLevelColor(EUsAirQualityLevel.Good),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Moderate,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm10,
            Min = 55,
            Max = 155,
            Color = UsLevelColor(EUsAirQualityLevel.Moderate),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.UnhealthyForSensitiveGroups,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm10,
            Min = 155,
            Max = 255,
            Color = UsLevelColor(EUsAirQualityLevel.UnhealthyForSensitiveGroups),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Unhealthy,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm10,
            Min = 255,
            Max = 355,
            Color = UsLevelColor(EUsAirQualityLevel.Unhealthy),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.VeryUnhealthy,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm10,
            Min = 355,
            Max = 425,
            Color = UsLevelColor(EUsAirQualityLevel.VeryUnhealthy),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Hazardous,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.Pm10,
            Min = 425,
            Max = 605,
            Color = UsLevelColor(EUsAirQualityLevel.Hazardous),
            Unit = "μg/m3"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Good,
            Period = TimeSpan.FromHours(8),
            Pollution = EPollution.CarbonMonoxide,
            Min = 0,
            Max = 4.5,
            Color = UsLevelColor(EUsAirQualityLevel.Good),
            Unit = "ppm"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Moderate,
            Period = TimeSpan.FromHours(8),
            Pollution = EPollution.CarbonMonoxide,
            Min = 4.5,
            Max = 9.5,
            Color = UsLevelColor(EUsAirQualityLevel.Moderate),
            Unit = "ppm"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.UnhealthyForSensitiveGroups,
            Period = TimeSpan.FromHours(8),
            Pollution = EPollution.CarbonMonoxide,
            Min = 9.5,
            Max = 12.5,
            Color = UsLevelColor(EUsAirQualityLevel.UnhealthyForSensitiveGroups),
            Unit = "ppm"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Unhealthy,
            Period = TimeSpan.FromHours(8),
            Pollution = EPollution.CarbonMonoxide,
            Min = 12.5,
            Max = 15.5,
            Color = UsLevelColor(EUsAirQualityLevel.Unhealthy),
            Unit = "ppm"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.VeryUnhealthy,
            Period = TimeSpan.FromHours(8),
            Pollution = EPollution.CarbonMonoxide,
            Min = 15.5,
            Max = 30.5,
            Color = UsLevelColor(EUsAirQualityLevel.VeryUnhealthy),
            Unit = "ppm"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Hazardous,
            Period = TimeSpan.FromHours(8),
            Pollution = EPollution.CarbonMonoxide,
            Min = 30.5,
            Max = 50.5,
            Color = UsLevelColor(EUsAirQualityLevel.Hazardous),
            Unit = "ppm"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Good,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 0,
            Max = 35,
            Color = UsLevelColor(EUsAirQualityLevel.Good),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Moderate,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 35,
            Max = 75,
            Color = UsLevelColor(EUsAirQualityLevel.Moderate),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.UnhealthyForSensitiveGroups,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 75,
            Max = 185,
            Color = UsLevelColor(EUsAirQualityLevel.UnhealthyForSensitiveGroups),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Unhealthy,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 185,
            Max = 305,
            Color = UsLevelColor(EUsAirQualityLevel.Unhealthy),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.VeryUnhealthy,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 305,
            Max = 605,
            Color = UsLevelColor(EUsAirQualityLevel.VeryUnhealthy),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Hazardous,
            Period = TimeSpan.FromDays(1),
            Pollution = EPollution.SulphurDioxide,
            Min = 605,
            Max = 1005,
            Color = UsLevelColor(EUsAirQualityLevel.Hazardous),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Good,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 0,
            Max = 54,
            Color = UsLevelColor(EUsAirQualityLevel.Good),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Moderate,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 54,
            Max = 100,
            Color = UsLevelColor(EUsAirQualityLevel.Moderate),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.UnhealthyForSensitiveGroups,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 100,
            Max = 360,
            Color = UsLevelColor(EUsAirQualityLevel.UnhealthyForSensitiveGroups),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Unhealthy,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 360,
            Max = 650,
            Color = UsLevelColor(EUsAirQualityLevel.Unhealthy),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.VeryUnhealthy,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 650,
            Max = 1250,
            Color = UsLevelColor(EUsAirQualityLevel.VeryUnhealthy),
            Unit = "ppb"
        },
        new UsAirQualityLevel
        {
            Level = EUsAirQualityLevel.Hazardous,
            Period = TimeSpan.FromHours(1),
            Pollution = EPollution.NitrogenDioxide,
            Min = 1250,
            Max = 2050,
            Color = UsLevelColor(EUsAirQualityLevel.Hazardous),
            Unit = "ppb"
        }
    ];

    private static string EuLevelColor(EEuAirQualityLevel level)
    {
        return level switch {
            EEuAirQualityLevel.Good => "#47EEE0",
            EEuAirQualityLevel.Fair => "#44C39A",
            EEuAirQualityLevel.Moderate => "#ECE433",
            EEuAirQualityLevel.Poor => "#E8333C",
            EEuAirQualityLevel.VeryPoor => "#820026",
            EEuAirQualityLevel.ExtremelyPoor => "#680D6D",
            _ => "#000000"
        };
    }

    public static string EuLevelName(EEuAirQualityLevel level)
    {
        return level switch {
            EEuAirQualityLevel.Good => "Good",
            EEuAirQualityLevel.Fair => "Fair",
            EEuAirQualityLevel.Moderate => "Moderate",
            EEuAirQualityLevel.Poor => "Poor",
            EEuAirQualityLevel.VeryPoor => "Very poor",
            EEuAirQualityLevel.ExtremelyPoor => "Extremely poor",
            _ => "Unknown"
        };
    }

    public static string UsLevelName(EUsAirQualityLevel level)
    {
        return level switch
        {
            EUsAirQualityLevel.Good => "Good",
            EUsAirQualityLevel.Moderate => "Moderate",
            EUsAirQualityLevel.UnhealthyForSensitiveGroups => "Unhealthy for Sensitive Groups",
            EUsAirQualityLevel.Unhealthy => "Unhealthy",
            EUsAirQualityLevel.VeryUnhealthy => "Very Unhealthy",
            EUsAirQualityLevel.Hazardous => "Hazardous",
            _ => "Unknown"
        };
    }
    
    private static string UsLevelColor(EUsAirQualityLevel level)
    {
        return level switch {
            EUsAirQualityLevel.Good => "#47EEE0",
            EUsAirQualityLevel.Moderate => "#44C39A",
            EUsAirQualityLevel.UnhealthyForSensitiveGroups => "#ECE433",
            EUsAirQualityLevel.Unhealthy => "#E8333C",
            EUsAirQualityLevel.VeryUnhealthy => "#820026",
            EUsAirQualityLevel.Hazardous => "#680D6D",
            _ => "#000000"
        };
    }
}