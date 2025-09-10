#pragma warning disable IL2026
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Serialization;

/// <summary>
/// </summary>
class DofusDbDateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                double value = JsonSerializer.Deserialize<double>(ref reader, options);
                DateTime dateTime = DateTime.UnixEpoch.AddMilliseconds(value);
                return DateOnly.FromDateTime(dateTime);
            default:
                return JsonSerializer.Deserialize<DateOnly>(ref reader, options);
        }
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) => JsonSerializer.Serialize(writer, value, options);
}
