using DofusSharp.DofusDb.ApiClients.Models;

namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     A client for interacting with table data from the DofusDB API.
/// </summary>
public interface IDofusDbTableClient : IDofusDbClient
{
    /// <summary>
    ///     Fetch the resource with the specified ID from the API.
    /// </summary>
    /// <param name="id">The unique identifier of the resource to fetch.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The resource with the specified ID.</returns>
    Task<DofusDbResource> GetAsync(long id, CancellationToken cancellationToken = default);
}
