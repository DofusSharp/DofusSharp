namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     A client for interacting with the DofusDb API.
/// </summary>
public interface IDofusDbClient
{
    /// <summary>
    ///     The base URL of the API to query.
    /// </summary>
    Uri BaseAddress { get; }

    /// <summary>
    ///     The referer header to include in requests to the API.
    /// </summary>
    /// <remarks>The DofusDB maintainers kindly ask to add the URI of the application using their APIs in the Referer header.</remarks>
    Uri? Referrer { get; }

    /// <summary>
    ///     The factory for creating HTTP clients used by this API client.
    /// </summary>
    IHttpClientFactory? HttpClientFactory { get; set; }
}
