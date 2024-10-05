using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EcoSensorApi.MeasurementPoints;

/// <summary>
/// Converts <see cref="double"/> values to and from JSON using a specific format.
/// </summary>
public class DoubleConverter : JsonConverter<double>
{
    /// <summary>
    /// The format used for JSON serialization and deserialization of double values.
    /// </summary>
    private const string DoubleFormat = "0.00";

    /// <summary>
    /// Reads and converts the JSON to a <see cref="double"/> value.
    /// </summary>
    /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <returns>The converted <see cref="double"/> value.</returns>
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return double.Parse(reader.GetString(), CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Writes a <see cref="double"/> value as JSON.
    /// </summary>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write to.</param>
    /// <param name="value">The <see cref="double"/> value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DoubleFormat, CultureInfo.InvariantCulture));
    }
}