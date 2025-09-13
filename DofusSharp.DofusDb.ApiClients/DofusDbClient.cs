using DofusSharp.DofusDb.ApiClients.Clients;

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
    public static IDofusDbClientsFactory Create(Uri baseUri, Uri? referrer = null) => new DofusDbClientsFactory(baseUri, referrer);

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
