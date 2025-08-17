using System.Runtime.CompilerServices;
using System.Text.Json;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;

namespace DofusSharp.DofusDb.ApiClients;

public static class DofusDbTableClientExtensions
{
    /// <summary>
    ///     Fetch the number of resources available in the API.
    /// </summary>
    /// <param name="client">The client instance to use for the requests.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The total count of resources.</returns>
    public static Task<int> CountAsync<TResource>(this IDofusDbTableClient<TResource> client, CancellationToken cancellationToken = default) where TResource: DofusDbEntity =>
        client.CountAsync([], cancellationToken);

    /// <summary>
    ///     Fetch the number of resources available in the API.
    /// </summary>
    /// <param name="client">The client instance to use for the requests.</param>
    /// <param name="predicate">The predicate to filter the resources.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The total count of resources.</returns>
    public static Task<int> CountAsync<TResource>(this IDofusDbTableClient<TResource> client, SearchPredicate predicate, CancellationToken cancellationToken = default)
        where TResource: DofusDbEntity =>
        client.CountAsync([predicate], cancellationToken);

    /// <summary>
    ///     Fetch a paginated list of resources from the API based on the provided search query.
    /// </summary>
    /// <param name="client">The client instance to use for the requests.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The search result containing the resources matching the query.</returns>
    public static Task<SearchResult<TResource>> SearchAsync<TResource>(this IDofusDbTableClient<TResource> client, CancellationToken cancellationToken = default)
        where TResource: DofusDbEntity =>
        client.SearchAsync(new SearchQuery(), cancellationToken);

    /// <summary>
    ///     Fetch ressources matching the search query from the API.
    ///     Performs multiple queries if necessary to fetch the requested number of results.
    /// </summary>
    /// <param name="client">The client instance to use for the requests.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <typeparam name="TResource">The type of resource to fetch from the API.</typeparam>
    /// <returns>The search result containing all resources matching the query.</returns>
    public static IAsyncEnumerable<TResource> MultiQuerySearchAsync<TResource>(this IDofusDbTableClient<TResource> client, CancellationToken cancellationToken = default)
        where TResource: DofusDbEntity =>
        MultiQuerySearchAsync(client, new SearchQuery(), cancellationToken);

    /// <summary>
    ///     Fetch ressources matching the search query from the API.
    ///     Performs multiple queries if necessary to fetch the requested number of results.
    /// </summary>
    /// <param name="client">The client instance to use for the requests.</param>
    /// <param name="query">The search query containing pagination parameters.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <typeparam name="TResource">The type of resource to fetch from the API.</typeparam>
    /// <returns>The search result containing all resources matching the query.</returns>
    public static async IAsyncEnumerable<TResource> MultiQuerySearchAsync<TResource>(
        this IDofusDbTableClient<TResource> client,
        SearchQuery query,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    ) where TResource: DofusDbEntity
    {
        int requested = query.Limit ?? int.MaxValue;
        int offset = query.Skip ?? 0;

        SearchQuery firstQuery = new() { Limit = requested, Skip = offset, Sort = query.Sort, Select = query.Select, Predicates = query.Predicates };
        SearchResult<TResource> firstResults = await SearchImplAsync(client, firstQuery, cancellationToken);

        foreach (TResource result in firstResults.Data)
        {
            yield return result;
        }

        offset += firstResults.Data.Count;
        requested -= firstResults.Data.Count;
        int total = firstResults.Total;

        if (offset >= total)
        {
            yield break;
        }

        while (requested > 0 && offset < total)
        {
            SearchQuery currentQuery = new() { Limit = requested, Skip = offset, Sort = query.Sort, Select = query.Select, Predicates = query.Predicates };

            SearchResult<TResource> results = await SearchImplAsync(client, currentQuery, cancellationToken);

            foreach (TResource result in results.Data)
            {
                yield return result;
            }

            offset += results.Data.Count;
            requested -= results.Data.Count;
        }
    }

    static async Task<SearchResult<TResource>> SearchImplAsync<TResource>(IDofusDbTableClient<TResource> client, SearchQuery currentQuery, CancellationToken cancellationToken)
        where TResource: DofusDbEntity
    {
        try
        {
            return await client.SearchAsync(currentQuery, cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception($"Error while executing query {JsonSerializer.Serialize(currentQuery)}", e);
        }
    }
}
