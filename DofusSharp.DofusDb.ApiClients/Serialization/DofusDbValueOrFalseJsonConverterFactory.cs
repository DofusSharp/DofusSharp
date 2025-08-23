using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Serialization;

class DofusDbValueOrFalseJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(DofusDbValueOrFalse<>);

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type underlyingType = typeToConvert.GetGenericArguments()[0];
        Type converterType = typeof(DofusDbValueOrFalseJsonConverter<>).MakeGenericType(underlyingType);
        return (JsonConverter?)Activator.CreateInstance(converterType);
    }
}
