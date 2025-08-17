namespace DofusSharp.DofusDb.ApiClients;

public static class DofusDbQuery
{
    /// <summary>
    ///     Create a query provider for the DofusDb API that connects to the production API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static DofusDbQueryProvider Production(Uri? referrer = null) => new(DofusDbClients.Production(referrer));

    /// <summary>
    ///     Create a query provider for the DofusDb API that connects to the beta API.
    /// </summary>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    public static DofusDbQueryProvider Beta(Uri? referrer = null) => new(DofusDbClients.Beta(referrer));
}
