namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     A client for interacting with the `version` API.
/// </summary>
public interface IDofusDbVersionClient : IDofusDbClient
{
    /// <summary>
    ///     Gets the current version of the DofusDb API.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<Version> GetVersionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get the URL to fetch the current version.
    ///     This URL is the one used by <see cref="GetVersionAsync(CancellationToken)" />.
    /// </summary>
    Uri GetVersionRequestUri();
}
