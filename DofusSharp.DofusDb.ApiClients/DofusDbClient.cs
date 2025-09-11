using System.Text.Json.Serialization.Metadata;
using DofusSharp.DofusDb.ApiClients.Clients;
using DofusSharp.DofusDb.ApiClients.Models;

namespace DofusSharp.DofusDb.ApiClients;

public static class DofusDbClient
{
    /// <summary>
    ///     The base URI of the production DofusDB API.
    /// </summary>
    public static Uri ProductionUri { get; } = new("https://api.dofusdb.fr/");

    /// <summary>
    ///     The base URI of the beta DofusDB API.
    /// </summary>
    public static Uri BetaUri { get; } = new("https://api.beta.dofusdb.fr/");

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the specified base URI.
    /// </summary>
    /// <param name="baseUri">The base URI of the API to query.</param>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Create(Uri baseUri, Uri? referrer = null) => Create(baseUri, DofusDbModelsSourceGenerationContext.Default, referrer);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the specified base URI.
    /// </summary>
    /// <param name="baseUri">The base URI of the API to query.</param>
    /// <param name="typeInfoResolver">The JSON type info resolver to use for serialization and deserialization.</param>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Create(Uri baseUri, IJsonTypeInfoResolver typeInfoResolver, Uri? referrer = null) =>
        new DofusDbClientsFactory(baseUri, typeInfoResolver, referrer);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the production API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Production(Uri? referrer = null) => Create(ProductionUri, referrer);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the beta API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Beta(Uri? referrer = null) => Create(BetaUri, referrer);
}
