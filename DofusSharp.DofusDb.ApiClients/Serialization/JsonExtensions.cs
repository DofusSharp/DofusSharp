using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DofusSharp.DofusDb.ApiClients.Serialization;

static class JsonExtensions
{
    /// <summary>
    ///     Get the typed <see cref="JsonTypeInfo{T}" /> for the specified type.
    /// </summary>
    /// <remark>
    ///     Backporting of API approved for .NET 11: https://github.com/dotnet/runtime/issues/118468
    /// </remark>
    public static JsonTypeInfo<T> GetTypeInfo<T>(this JsonSerializerOptions options) => (JsonTypeInfo<T>)options.GetTypeInfo(typeof(T));
}
