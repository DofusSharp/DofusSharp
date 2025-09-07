using DofusSharp.DofusDb.ApiClients.Queries;

namespace DofusSharp.DofusDb.ApiClients;

public static class DofusDbQuery
{
    /// <summary>
    ///     Create a query provider for the DofusDb API that connects to the production API.
    /// </summary>
    /// <param name="clientsFactory">The clients factory used by the query.</param>
    public static IDofusDbQueryProvider Create(IDofusDbClientsFactory clientsFactory) => new DofusDbQueryProvider(clientsFactory);

    /// <summary>
    ///     Create a query provider for the DofusDb API that connects to the production API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbQueryProvider Production(Uri? referrer = null) => Create(DofusDbClient.Production(referrer));

    /// <summary>
    ///     Create a query provider for the DofusDb API that connects to the beta API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static IDofusDbQueryProvider Beta(Uri? referrer = null) => Create(DofusDbClient.Beta(referrer));
}
