using System.Net.Http.Json;
using System.Text.Json;
using DofusSharp.Common;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;
using DofusSharp.DofusDb.ApiClients.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Clients;

class DofusDbTableClient<TResource>(Uri baseAddress, Uri? referrer, JsonSerializerOptions options) : IDofusDbTableClient<TResource> where TResource: DofusDbResource
{
    readonly DofusDbSearchRequestQueryParamsBuilder _queryParamsBuilder = new();

    public Uri BaseAddress { get; } = baseAddress;
    public Uri? Referrer { get; } = referrer;
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<TResource> GetAsync(long id, CancellationToken cancellationToken = default)
    {
        Uri url = GetQuery(id);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        if (await response.Content.ReadFromJsonAsync(options.GetTypeInfo<TResource>(), cancellationToken) is not { } result)
        {
            throw new InvalidOperationException("Could not deserialize the response content.");
        }

        return result;
    }

    public Uri GetQuery(long id) => new(BaseAddress, $"{id}");

    public async Task<int> CountAsync(IReadOnlyCollection<DofusDbSearchPredicate> predicates, CancellationToken cancellationToken = default)
    {
        Uri url = CountQuery(predicates);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        if (await response.Content.ReadFromJsonAsync(options.GetTypeInfo<DofusDbSearchResult<TResource>>(), cancellationToken) is not { } result)
        {
            throw new InvalidOperationException("Could not deserialize the search result.");
        }

        return result.Total;
    }

    public Uri CountQuery(IReadOnlyCollection<DofusDbSearchPredicate> predicates)
    {
        DofusDbSearchQuery query = new() { Limit = 0, Predicates = predicates };
        string queryParams = _queryParamsBuilder.BuildQueryParams(query);
        return new Uri(BaseAddress, $"?{queryParams}");
    }

    public async Task<DofusDbSearchResult<TResource>> SearchAsync(DofusDbSearchQuery query, CancellationToken cancellationToken = default)
    {
        Uri url = SearchQuery(query);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        if (await response.Content.ReadFromJsonAsync(options.GetTypeInfo<DofusDbSearchResult<TResource>>(), cancellationToken) is not { } result)
        {
            throw new InvalidOperationException("Could not deserialize the search result.");
        }

        return result;
    }

    public Uri SearchQuery(DofusDbSearchQuery query)
    {
        string queryParams = _queryParamsBuilder.BuildQueryParams(query);
        string requestUri = string.IsNullOrWhiteSpace(queryParams) ? string.Empty : $"?{queryParams}";
        return new Uri(BaseAddress, requestUri);
    }
}
