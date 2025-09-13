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
        T1 item1 = (T1?)JsonSerializer.Deserialize(ref reader, options.GetTypeInfo(typeof(T1))) ?? throw new JsonException("Could not deserialize 1st element of tuple.");
        reader.Read();
        T2 item2 = (T2?)JsonSerializer.Deserialize(ref reader, options.GetTypeInfo(typeof(T2))) ?? throw new JsonException("Could not deserialize 2nd element of tuple.");
        reader.Read();

        if (reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException("Expected end of array.");
        }

        return new ValueTuple<T1, T2>(item1, item2);
    }

    public override void Write(Utf8JsonWriter writer, ValueTuple<T1, T2> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        JsonSerializer.Serialize(writer, value.Item1, options.GetTypeInfo(typeof(T1)));
        JsonSerializer.Serialize(writer, value.Item2, options.GetTypeInfo(typeof(T2)));
        writer.WriteEndArray();
    }
}
