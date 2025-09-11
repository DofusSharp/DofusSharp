#pragma warning disable IL2026
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Serialization;

class DofusDbValueTupleJsonConverter<T1, T2, T3, T4, T5, T6, T7> : JsonConverter<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>
{
    public override ValueTuple<T1, T2, T3, T4, T5, T6, T7> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array.");
        }

        reader.Read();
        T1 item1 =
// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
            JsonSerializer.Deserialize<T1>(ref reader, options) ?? throw new JsonException("Could not deserialize 1st element of tuple.");
#pragma warning restore IL2026
        reader.Read();
        T2 item2 =
// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
            JsonSerializer.Deserialize<T2>(ref reader, options) ?? throw new JsonException("Could not deserialize 2nd element of tuple.");
#pragma warning restore IL2026
        reader.Read();
        T3 item3 =
// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
            JsonSerializer.Deserialize<T3>(ref reader, options) ?? throw new JsonException("Could not deserialize 3rd element of tuple.");
#pragma warning restore IL2026
        reader.Read();
        T4 item4 =
// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
            JsonSerializer.Deserialize<T4>(ref reader, options) ?? throw new JsonException("Could not deserialize 4th element of tuple.");
#pragma warning restore IL2026
        reader.Read();
        T5 item5 =
// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
            JsonSerializer.Deserialize<T5>(ref reader, options) ?? throw new JsonException("Could not deserialize 5th element of tuple.");
#pragma warning restore IL2026
        reader.Read();
        T6 item6 =
// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
            JsonSerializer.Deserialize<T6>(ref reader, options) ?? throw new JsonException("Could not deserialize 6th element of tuple.");
#pragma warning restore IL2026
        reader.Read();
        T7 item7 =
// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
            JsonSerializer.Deserialize<T7>(ref reader, options) ?? throw new JsonException("Could not deserialize 7th element of tuple.");
#pragma warning restore IL2026

        reader.Read(); // Read the end of the array
        if (reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException("Expected end of array.");
        }

        return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
    }

    public override void Write(Utf8JsonWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7> value, JsonSerializerOptions options)
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

// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
        JsonSerializer.Serialize(writer, value.Item3, options);
#pragma warning restore IL2026

// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
        JsonSerializer.Serialize(writer, value.Item4, options);
#pragma warning restore IL2026

// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
        JsonSerializer.Serialize(writer, value.Item5, options);
#pragma warning restore IL2026

// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
        JsonSerializer.Serialize(writer, value.Item6, options);
#pragma warning restore IL2026

// There is no trim issue if the JsonSerializerOptions contains the proper TypeInfoResolver.
#pragma warning disable IL2026
        JsonSerializer.Serialize(writer, value.Item7, options);
#pragma warning restore IL2026

        writer.WriteEndArray();
    }
}
