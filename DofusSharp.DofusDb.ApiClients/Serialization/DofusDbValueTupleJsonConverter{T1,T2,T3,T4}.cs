#pragma warning disable IL2026
using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models;

namespace DofusSharp.DofusDb.ApiClients.Serialization;

class DofusDbValueTupleJsonConverter<T1, T2, T3, T4> : JsonConverter<ValueTuple<T1, T2, T3, T4>>
{
    public override ValueTuple<T1, T2, T3, T4> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array.");
        }

        DofusDbModelsSourceGenerationContext context = new(options);

        reader.Read();
        T1 item1 = (T1?)JsonSerializer.Deserialize(ref reader, typeof(T1), context) ?? throw new JsonException("Could not deserialize 1st element of tuple.");
        reader.Read();
        T2 item2 = (T2?)JsonSerializer.Deserialize(ref reader, typeof(T2), context) ?? throw new JsonException("Could not deserialize 2nd element of tuple.");
        reader.Read();
        T3 item3 = (T3?)JsonSerializer.Deserialize(ref reader, typeof(T3), context) ?? throw new JsonException("Could not deserialize 3rd element of tuple.");
        reader.Read();
        T4 item4 = (T4?)JsonSerializer.Deserialize(ref reader, typeof(T4), context) ?? throw new JsonException("Could not deserialize 4th element of tuple.");
        reader.Read();

        if (reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException("Expected end of array.");
        }

        return new ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
    }

    public override void Write(Utf8JsonWriter writer, ValueTuple<T1, T2, T3, T4> value, JsonSerializerOptions options)
    {
        DofusDbModelsSourceGenerationContext context = new(options);

        writer.WriteStartArray();
        JsonSerializer.Serialize(writer, value.Item1, typeof(T1), context);
        JsonSerializer.Serialize(writer, value.Item2, typeof(T2), context);
        JsonSerializer.Serialize(writer, value.Item3, typeof(T3), context);
        JsonSerializer.Serialize(writer, value.Item4, typeof(T4), context);
        writer.WriteEndArray();
    }
}
