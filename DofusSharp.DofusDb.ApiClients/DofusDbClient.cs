using System.Text.Json.Serialization.Metadata;
using DofusSharp.DofusDb.ApiClients.Clients;
using DofusSharp.DofusDb.ApiClients.Models;

namespace DofusSharp.DofusDb.ApiClients;

public static class DofusDbClient
{
    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the specified base URI.
    /// </summary>
    /// <param name="baseUri">The base URI of the API to query.</param>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Create(Uri baseUri, Uri? referrer = null) => Create(DofusDbModelsSourceGenerationContext.Default, baseUri, referrer);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the specified base URI.
    /// </summary>
    /// <param name="typeInfoResolver">The JSON type info resolver to use for serialization and deserialization.</param>
    /// <param name="baseUri">The base URI of the API to query.</param>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Create(IJsonTypeInfoResolver typeInfoResolver, Uri baseUri, Uri? referrer = null) =>
        new DofusDbClientsFactory(typeInfoResolver, baseUri, referrer);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the production API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Production(Uri? referrer = null) => Create(new Uri("https://api.dofusdb.fr/"), referrer);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the beta API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Beta(Uri? referrer = null) => Create(new Uri("https://api.beta.dofusdb.fr/"), referrer);
}
