using System.Text.Json;
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
    /// <param name="options"> The JSON serializer options to use when deserializing responses from the API.</param>
    public static IDofusDbClientsFactory Create(Uri baseUri, Uri? referrer, JsonSerializerOptions options) => new DofusDbClientsFactory(baseUri, referrer, options);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the specified base URI.
    ///     The factory will use the production models when deserializing responses from the API.
    /// </summary>
    /// <param name="baseUri">The base URI of the API to query.</param>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory CreateProductionFactory(Uri baseUri, Uri? referrer) => Create(baseUri, referrer, DofusDbModelsSourceGenerationContext.ProdOptions);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the specified base URI.
    ///     The factory will use the beta models when deserializing responses from the API.
    /// </summary>
    /// <param name="baseUri">The base URI of the API to query.</param>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory CreateBetaFactory(Uri baseUri, Uri? referrer) => Create(baseUri, referrer, DofusDbModelsSourceGenerationContext.BetaOptions);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the production API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Production(Uri? referrer = null) => CreateProductionFactory(ProductionUri, referrer);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the beta API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Beta(Uri? referrer = null) => CreateBetaFactory(BetaUri, referrer);
}
