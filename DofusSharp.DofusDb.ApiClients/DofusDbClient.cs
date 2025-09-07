using DofusSharp.DofusDb.ApiClients.Clients;

namespace DofusSharp.DofusDb.ApiClients;

public static class DofusDbClient
{
    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the production API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Production(Uri? referrer = null) => new DofusDbClientsFactory(new Uri("https://api.dofusdb.fr/"), referrer);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the beta API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbClientsFactory Beta(Uri? referrer = null) => new DofusDbClientsFactory(new Uri("https://api.beta.dofusdb.fr/"), referrer);
}
