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
// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
                double value = JsonSerializer.Deserialize<double>(ref reader, options);
#pragma warning restore IL2026
                DateTime dateTime = DateTime.UnixEpoch.AddMilliseconds(value);
                return DateOnly.FromDateTime(dateTime);
            default:
// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
                return JsonSerializer.Deserialize<DateOnly>(ref reader, options);
#pragma warning restore IL2026
        }
    }

// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) => JsonSerializer.Serialize(writer, value, options);
#pragma warning restore IL2026
}
