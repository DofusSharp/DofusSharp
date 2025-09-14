using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace DofusSharp.DofusDb.ApiClients.Serialization;

public static class JsonExtensions
{
    /// <summary>
    ///     Get the typed <see cref="JsonTypeInfo{T}" /> for the specified type.
    /// </summary>
    /// <remarks>
    ///     Backporting of API approved for .NET 11: https://github.com/dotnet/runtime/issues/118468
    /// </remarks>
    public static JsonTypeInfo<T> GetTypeInfo<T>(this JsonSerializerOptions options) => (JsonTypeInfo<T>)options.GetTypeInfo(typeof(T));

    public static JsonTypeInfo<T> GetTypeInfo<T>(this JsonSerializerContext context) => (JsonTypeInfo<T>)context.Options.GetTypeInfo(typeof(T));
}
