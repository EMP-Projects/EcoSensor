using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EcoSensorApi.MeasurementPoints;

/// <summary>
/// Converts <see cref="DateTime"/> objects to and from JSON using a specific date format.
/// </summary>
public class DateTimeConverter : JsonConverter<DateTime>
{
    /// <summary>
    /// The date format used for JSON serialization and deserialization.
    /// </summary>
    private const string DateFormat = "yyyy-MM-ddTHH:mm:ssZ";

    /// <summary>
    /// Reads and converts the JSON to a <see cref="DateTime"/> object.
    /// </summary>
    /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <returns>The converted <see cref="DateTime"/> object.</returns>
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString(), DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
    }

    /// <summary>
    /// Writes a <see cref="DateTime"/> object as JSON.
    /// </summary>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write to.</param>
    /// <param name="value">The <see cref="DateTime"/> value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
    }
}