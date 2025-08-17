using System.Net.Http.Json;
using System.Text.Json;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;
using DofusSharp.DofusDb.ApiClients.Serialization;

namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     A client for interacting with the DofusDB API.
/// </summary>
/// <typeparam name="TResource">The type of resource to fetch from the API.</typeparam>
class DofusDbApiClient<TResource> : IDofusDbApiClient<TResource> where TResource: DofusDbEntity
{
    readonly JsonSerializerOptions? _options;
    readonly SearchRequestQueryParamsBuilder _queryParamsBuilder = new();

    /// <summary>
    ///     A client for interacting with the DofusDB API.
    /// </summary>
    /// <param name="baseAddress">The base URL of the API to query.</param>
    /// <param name="referrer">The referer header to include in requests to the API.</param>
    /// <typeparam name="TResource">The type of resource to fetch from the API.</typeparam>
    public DofusDbApiClient(Uri baseAddress, Uri? referrer = null)
    {
        Referrer = referrer;
        BaseAddress = baseAddress;
        _options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            Converters =
            {
                new ValueOrFalseJsonConverterFactory(),
                new ValueTupleJsonConverterFactory()
            }
        };
    }

    /// <summary>
    ///     The base URL of the API to query.
    /// </summary>
    public Uri BaseAddress { get; }

    /// <summary>
    ///     The referer header to include in requests to the API.
    /// </summary>
    /// <remarks>The DofusDB maintainers kindly ask to add the URI of the application using their APIs in the Referer header.</remarks>
    public Uri? Referrer { get; }

    /// <summary>
    ///     Fetch the resource with the specified ID from the API.
    /// </summary>
    /// <param name="id">The unique identifier of the resource to fetch.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The resource with the specified ID.</returns>
    public async Task<TResource> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = new();
        httpClient.BaseAddress = BaseAddress;
        httpClient.DefaultRequestHeaders.Referrer = Referrer;

        HttpResponseMessage response = await httpClient.GetAsync($"{id}", cancellationToken);
        response.EnsureSuccessStatusCode();

        TResource? result = await response.Content.ReadFromJsonAsync<TResource>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the response content.");
        }

        return result;
    }

    /// <summary>
    ///     Fetch the number of resources available in the API.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The total count of resources.</returns>
    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = new();
        httpClient.BaseAddress = BaseAddress;
        httpClient.DefaultRequestHeaders.Referrer = Referrer;

        HttpResponseMessage response = await httpClient.GetAsync("?$limit=0", cancellationToken);
        response.EnsureSuccessStatusCode();

        SearchResult<TResource>? result = await response.Content.ReadFromJsonAsync<SearchResult<TResource>>(cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the search result.");
        }

        return result.Total;
    }

    /// <summary>
    ///     Fetch a paginated list of resources from the API based on the provided search query.
    /// </summary>
    /// <param name="query">The search query containing pagination parameters.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The search result containing the resources matching the query.</returns>
    public async Task<SearchResult<TResource>> SearchAsync(SearchQuery query, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = new();
        httpClient.BaseAddress = BaseAddress;
        httpClient.DefaultRequestHeaders.Referrer = Referrer;

        string queryParams = _queryParamsBuilder.BuildQueryParams(query);
        string requestUri = string.IsNullOrWhiteSpace(queryParams) ? string.Empty : $"?{queryParams}";

        HttpResponseMessage response = await httpClient.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();

        SearchResult<TResource>? result = await response.Content.ReadFromJsonAsync<SearchResult<TResource>>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the search result.");
        }

        return result;
    }
}
