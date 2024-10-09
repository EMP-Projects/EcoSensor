using System.Text.Json.Serialization;

namespace EcoSensorApi.MeasurementPoints;

using System.Text.Json;

/// <summary>
/// Provides methods to configure JSON serialization options for measurements.
/// </summary>
public static class MeasurementsConverter 
{
    /// <summary>
    /// Gets the JSON serializer options with custom converters.
    /// </summary>
    /// <returns>A <see cref="JsonSerializerOptions"/> object configured with custom converters.</returns>
    public static JsonSerializerOptions GetOptions()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter(),
                new DoubleConverter(),
                new DateTimeConverter()
            }
        };

        return options;
    }
}