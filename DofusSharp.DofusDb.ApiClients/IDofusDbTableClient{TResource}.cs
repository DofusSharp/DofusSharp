using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;

namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     A client for interacting with table data from the DofusDB API.
/// </summary>
/// <typeparam name="TResource">The type of resource to fetch from the API.</typeparam>
public interface IDofusDbTableClient<TResource> where TResource: DofusDbEntity
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

    /// <summary>
    ///     Fetch the resource with the specified ID from the API.
    /// </summary>
    /// <param name="id">The unique identifier of the resource to fetch.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The resource with the specified ID.</returns>
    Task<TResource> GetAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Fetch the number of resources available in the API.
    /// </summary>
    /// <param name="predicates">The collection of predicates to filter the resources. If empty, all resources are counted.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The total count of resources.</returns>
    Task<int> CountAsync(IReadOnlyCollection<SearchPredicate> predicates, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Fetch a paginated list of resources from the API based on the provided search query.
    /// </summary>
    /// <param name="query">The search query containing pagination parameters.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The search result containing the resources matching the query.</returns>
    Task<SearchResult<TResource>> SearchAsync(SearchQuery query, CancellationToken cancellationToken = default);
}
