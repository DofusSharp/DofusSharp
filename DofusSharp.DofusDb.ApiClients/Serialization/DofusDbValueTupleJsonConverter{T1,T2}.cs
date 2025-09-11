using System.Text.Json;
using System.Text.Json.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Serialization;

class DofusDbValueTupleJsonConverter<T1, T2> : JsonConverter<ValueTuple<T1, T2>>
{
    public override ValueTuple<T1, T2> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array.");
        }

        reader.Read();
// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
        T1 item1 = JsonSerializer.Deserialize<T1>(ref reader, options) ?? throw new JsonException("Could not deserialize 1st element of tuple.");
#pragma warning restore IL2026

        reader.Read();
// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
        T2 item2 = JsonSerializer.Deserialize<T2>(ref reader, options) ?? throw new JsonException("Could not deserialize 2nd element of tuple.");
#pragma warning restore IL2026

        reader.Read(); // Read the end of the array
        if (reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException("Expected end of array.");
        }

        return new ValueTuple<T1, T2>(item1, item2);
    }

    public override void Write(Utf8JsonWriter writer, ValueTuple<T1, T2> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
        JsonSerializer.Serialize(writer, value.Item1, options);
#pragma warning restore IL2026

// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
        JsonSerializer.Serialize(writer, value.Item2, options);
#pragma warning restore IL2026

        writer.WriteEndArray();
    }
}
