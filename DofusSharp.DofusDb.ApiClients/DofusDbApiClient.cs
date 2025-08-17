namespace DofusSharp.DofusDb.ApiClients;

public static class DofusDbApiClient
{
    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the production API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static DofusDbApiClientsFactory Production(Uri? referrer = null) => new(new Uri("https://api.dofusdb.fr/"), referrer);

    /// <summary>
    ///     Create a factory for DofusDb API clients that connects to the beta API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static DofusDbApiClientsFactory Beta(Uri? referrer = null) => new(new Uri("https://api.beta.dofusdb.fr/"), referrer);
}
