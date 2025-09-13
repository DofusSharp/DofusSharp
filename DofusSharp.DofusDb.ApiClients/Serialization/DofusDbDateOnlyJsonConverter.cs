using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models;

namespace DofusSharp.DofusDb.ApiClients.Serialization;

/// <summary>
/// </summary>
class DofusDbDateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        DofusDbModelsSourceGenerationContext context = new(options);

        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                double value = (double?)JsonSerializer.Deserialize(ref reader, typeof(double), context) ?? 0;
                DateTime dateTime = DateTime.UnixEpoch.AddMilliseconds(value);
                return DateOnly.FromDateTime(dateTime);
            default:
                return (DateOnly?)JsonSerializer.Deserialize(ref reader, typeof(DateOnly), context) ?? default;
        }
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, value, typeof(DateOnly), new DofusDbModelsSourceGenerationContext(options));
}
