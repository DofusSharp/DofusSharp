using System.Runtime.CompilerServices;
using System.Text.Json;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;

namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     A client for interacting with table data from the DofusDB API.
/// </summary>
/// <typeparam name="TResource">The type of resource to fetch from the API.</typeparam>
public interface IDofusDbTableClient<TResource> : IDofusDbClient where TResource: DofusDbResource
{
    /// <summary>
    ///     Fetch the resource with the specified ID from the API.
    /// </summary>
    /// <param name="id">The unique identifier of the resource to fetch.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The resource with the specified ID.</returns>
    Task<TResource> GetAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get the URL of the resource with the specified ID.
    ///     This URL is the one used by <see cref="GetAsync(long, CancellationToken)" />.
    /// </summary>
    /// <param name="id">The unique identifier of the resource to fetch.</param>
    Uri GetQuery(long id);

    /// <summary>
    ///     Fetch the number of resources available in the API.
    /// </summary>
    /// <param name="predicates">The collection of predicates to filter the resources. If empty, all resources are counted.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The total count of resources.</returns>
    Task<int> CountAsync(IReadOnlyCollection<DofusDbSearchPredicate> predicates, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get the URL to fetch the number of resources available.
    ///     This URL is the one used by <see cref="CountAsync(IReadOnlyCollection{DofusDbSearchPredicate}, CancellationToken)" />.
    /// </summary>
    /// <param name="predicates">The collection of predicates to filter the resources. If empty, all resources are counted.</param>
    Uri CountQuery(IReadOnlyCollection<DofusDbSearchPredicate> predicates);

    /// <summary>
    ///     Fetch a paginated list of resources from the API based on the provided search query.
    /// </summary>
    /// <param name="query">The search query containing pagination parameters.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The search result containing the resources matching the query.</returns>
    Task<DofusDbSearchResult<TResource>> SearchAsync(DofusDbSearchQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get the URL to fetch a paginated list of resources.
    ///     This URL is the one used by <see cref="SearchAsync(DofusDbSearchQuery, CancellationToken)" />.
    /// </summary>
    /// <param name="query">The search query containing pagination parameters.</param>
    Uri SearchQuery(DofusDbSearchQuery query);
}

public static class DofusDbTableClientExtensions
{
    /// <summary>
    ///     Fetch the number of resources available in the API.
    /// </summary>
    /// <param name="client">The client instance to use for the requests.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The total count of resources.</returns>
    public static Task<int> CountAsync<TResource>(this IDofusDbTableClient<TResource> client, CancellationToken cancellationToken = default) where TResource: DofusDbResource =>
        client.CountAsync([], cancellationToken);

    /// <summary>
    ///     Fetch the number of resources available in the API.
    /// </summary>
    /// <param name="client">The client instance to use for the requests.</param>
    /// <param name="predicate">The predicate to filter the resources.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The total count of resources.</returns>
    public static Task<int> CountAsync<TResource>(this IDofusDbTableClient<TResource> client, DofusDbSearchPredicate predicate, CancellationToken cancellationToken = default)
        where TResource: DofusDbResource =>
        client.CountAsync([predicate], cancellationToken);

    /// <summary>
    ///     Fetch a paginated list of resources from the API based on the provided search query.
    /// </summary>
    /// <param name="client">The client instance to use for the requests.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The search result containing the resources matching the query.</returns>
    public static Task<DofusDbSearchResult<TResource>> SearchAsync<TResource>(this IDofusDbTableClient<TResource> client, CancellationToken cancellationToken = default)
        where TResource: DofusDbResource =>
        client.SearchAsync(new DofusDbSearchQuery(), cancellationToken);

    /// <summary>
    ///     Fetch ressources matching the search query from the API.
    ///     Performs multiple queries if necessary to fetch the requested number of results.
    /// </summary>
    /// <param name="client">The client instance to use for the requests.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <typeparam name="TResource">The type of resource to fetch from the API.</typeparam>
    /// <returns>The search result containing all resources matching the query.</returns>
    public static IAsyncEnumerable<TResource> MultiQuerySearchAsync<TResource>(this IDofusDbTableClient<TResource> client, CancellationToken cancellationToken = default)
        where TResource: DofusDbResource =>
        MultiQuerySearchAsync(client, new DofusDbSearchQuery(), cancellationToken);

    /// <summary>
    ///     Fetch ressources matching the search query from the API.
    ///     Performs multiple queries if necessary to fetch the requested number of results.
    /// </summary>
    /// <param name="client">The client instance to use for the requests.</param>
    /// <param name="query">The search query containing pagination parameters.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <typeparam name="TResource">The type of resource to fetch from the API.</typeparam>
    /// <returns>The search result containing all resources matching the query.</returns>
    public static IAsyncEnumerable<TResource> MultiQuerySearchAsync<TResource>(
        this IDofusDbTableClient<TResource> client,
        DofusDbSearchQuery query,
        CancellationToken cancellationToken = default
    ) where TResource: DofusDbResource =>
        MultiQuerySearchAsync(client, query, null, cancellationToken);

    /// <summary>
    ///     Fetch ressources matching the search query from the API.
    ///     Performs multiple queries if necessary to fetch the requested number of results.
    /// </summary>
    /// <param name="client">The client instance to use for the requests.</param>
    /// <param name="query">The search query containing pagination parameters.</param>
    /// <param name="progress">The progress reporter.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <typeparam name="TResource">The type of resource to fetch from the API.</typeparam>
    /// <returns>The search result containing all resources matching the query.</returns>
    public static async IAsyncEnumerable<TResource> MultiQuerySearchAsync<TResource>(
        this IDofusDbTableClient<TResource> client,
        DofusDbSearchQuery query,
        IProgress<MultiSearchQueryProgress>? progress,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    ) where TResource: DofusDbResource
    {
        int toFetch = query.Limit ?? int.MaxValue;
        int offset = query.Skip ?? 0;

        DofusDbSearchQuery firstQuery = new() { Limit = toFetch, Skip = offset, Sort = query.Sort, Select = query.Select, Predicates = query.Predicates };
        progress?.Report(new MultiSearchCurrentCount(0, toFetch));
        progress?.Report(new MultiSearchNextQuery(firstQuery));

        DofusDbSearchResult<TResource> firstResults = await SearchImplAsync(client, firstQuery, cancellationToken);

        int loaded = firstResults.Data.Count;
        int requested = Math.Min(query.Limit ?? int.MaxValue, firstResults.Total - (query.Skip ?? 0));

        progress?.Report(new MultiSearchCurrentCount(loaded, requested));

        foreach (TResource result in firstResults.Data)
        {
            yield return result;
        }

        offset += firstResults.Data.Count;
        toFetch = requested - firstResults.Data.Count;
        int total = firstResults.Total;

        if (offset >= total)
        {
            yield break;
        }

        while (toFetch > 0 && offset < total)
        {
            DofusDbSearchQuery currentQuery = new() { Limit = toFetch, Skip = offset, Sort = query.Sort, Select = query.Select, Predicates = query.Predicates };
            progress?.Report(new MultiSearchNextQuery(currentQuery));

            DofusDbSearchResult<TResource> results = await SearchImplAsync(client, currentQuery, cancellationToken);

            loaded += results.Data.Count;
            progress?.Report(new MultiSearchCurrentCount(loaded, requested));

            foreach (TResource result in results.Data)
            {
                yield return result;
            }

            offset += results.Data.Count;
            toFetch -= results.Data.Count;
        }
    }

    static async Task<DofusDbSearchResult<TResource>> SearchImplAsync<TResource>(
        IDofusDbTableClient<TResource> client,
        DofusDbSearchQuery currentQuery,
        CancellationToken cancellationToken
    ) where TResource: DofusDbResource
    {
        try
        {
            return await client.SearchAsync(currentQuery, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new Exception($"Error while executing query {JsonSerializer.Serialize(currentQuery, DofusDbModelsSourceGenerationContext.Default.DofusDbSearchQuery)}", e);
        }
    }

    /// <summary>
    ///     Base class for progress of the multi search operation.
    /// </summary>
    public abstract record MultiSearchQueryProgress;

    /// <summary>
    ///     The search operation will execute a new query.
    /// </summary>
    /// <param name="NextQuery">The next query that will be executed. Null can either mean that the next query has not been computed yet, or that there is no more query to execute.</param>
    public record MultiSearchNextQuery(DofusDbSearchQuery NextQuery) : MultiSearchQueryProgress;

    /// <summary>
    ///     The search operation has fetched additional results.
    /// </summary>
    /// <param name="AlreadyFetched">The number of items that have already been fetched from the server.</param>
    /// <param name="TotalToFetch">The total number of items that will be fetched from the server.</param>
    public record MultiSearchCurrentCount(int AlreadyFetched, int TotalToFetch) : MultiSearchQueryProgress;
}
