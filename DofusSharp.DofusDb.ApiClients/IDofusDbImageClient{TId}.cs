using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     A client for interacting with a DofusDB API that provides image resources.
/// </summary>
/// <typeparam name="TId">The type of the unique identifier for the images.</typeparam>
public interface IDofusDbImageClient<in TId> : IDofusDbClient
{
    /// <summary>
    ///     The format of the images that this client fetches from the DofusDB API.
    /// </summary>
    public ImageFormat ImageFormat { get; }

    /// <summary>
    ///     Fetches the image resource from the DofusDB API by its unique identifier.
    ///     The image format is determined by the <see cref="ImageFormat" /> property.
    /// </summary>
    /// <param name="id">The unique identifier of the resource to fetch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<Stream> GetImageAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get the URL of the image resource.
    ///     This URL is the one used by <see cref="GetImageAsync(TId, CancellationToken)" />.
    /// </summary>
    /// <param name="id">The unique identifier of the resource to fetch.</param>
    Uri GetImageQuery(TId id);
}
