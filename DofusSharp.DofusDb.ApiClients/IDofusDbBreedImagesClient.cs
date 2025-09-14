namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     A client for interacting with the DofusDB API that provides breed image resources.
/// </summary>
public interface IDofusDbBreedImagesClient : IDofusDbClient
{
    /// <summary>
    ///     Fetch the symbol image with the given id.
    ///     The result is a stream containing the image data in PNG format.
    /// </summary>
    /// <remarks>The symbol ID is the same as the breed ID.</remarks>
    /// <param name="id">The unique identifier of the symbol.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<Stream> GetSymbolAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get the URL of the symbol image with the given id.
    /// </summary>
    /// <remarks>The symbol ID is the same as the breed ID.</remarks>
    /// <param name="id">The unique identifier of the symbol.</param>
    Uri GetSymbolQuery(long id);

    /// <summary>
    ///     Fetch the logo image with the given id.
    ///     The result is a stream containing the image data in PNG format.
    /// </summary>
    /// <remarks>The logo ID is the same as the breed ID.</remarks>
    /// <param name="id">The unique identifier of the logo.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<Stream> GetLogoAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get the URL of the logo image with the given id.
    /// </summary>
    /// <remarks>The logo ID is the same as the breed ID.</remarks>
    /// <param name="id">The unique identifier of the logo.</param>
    Uri GetLogoQuery(long id);

    /// <summary>
    ///     Fetch the head image with the given id.
    /// </summary>
    /// <remarks>The male head ID is the breed ID multiplied by 10. The female head ID is the breed ID multiplied by 10 plus 1.</remarks>
    /// <param name="id">The unique identifier of the head.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<Stream> GetHeadAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get the URL of the head image with the given id.
    /// </summary>
    /// <remarks>The male head ID is the breed ID multiplied by 10. The female head ID is the breed ID multiplied by 10 plus 1.</remarks>
    /// <param name="id">The unique identifier of the head.</param>
    Uri GetHeadQuery(long id);
}
