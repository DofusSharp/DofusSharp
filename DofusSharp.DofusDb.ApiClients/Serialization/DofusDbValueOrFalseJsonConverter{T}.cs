using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Serialization;

class DofusDbValueOrFalseJsonConverter<T> : JsonConverter<DofusDbValueOrFalse<T>>
{
    public override DofusDbValueOrFalse<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.False)
        {
            return new DofusDbValueOrFalse<T> { Value = default };
        }

        T? value = JsonSerializer.Deserialize<T>(ref reader, options);
        return new DofusDbValueOrFalse<T> { Value = value };
    }

    public override void Write(Utf8JsonWriter writer, DofusDbValueOrFalse<T> value, JsonSerializerOptions options)
    {
        if (value.IsFalse)
        {
            writer.WriteBooleanValue(false);
        }
        else
        {
            JsonSerializer.Serialize(writer, value.Value, options);
        }
    }
}
