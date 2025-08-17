namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     A client for interacting with a DofusDB API that provides image resources.
/// </summary>
public interface IDofusDbImageClient : IDofusDbClient
{
    /// <summary>
    ///     Fetches the image resource from the DofusDB API by its unique identifier.
    ///     The image is returned as a stream containing a JPEG image.
    /// </summary>
    /// <param name="id">The unique identifier of the resource to fetch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<Stream> GetImageAsync(int id, CancellationToken cancellationToken = default);
}
