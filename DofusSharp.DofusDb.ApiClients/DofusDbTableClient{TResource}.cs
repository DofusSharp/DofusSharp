using System.Net.Http.Json;
using System.Text.Json;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;
using DofusSharp.DofusDb.ApiClients.Serialization;

namespace DofusSharp.DofusDb.ApiClients;

/// <inheritdoc />
class DofusDbTableClient<TResource> : IDofusDbTableClient<TResource> where TResource: DofusDbEntity
{
    readonly JsonSerializerOptions? _options;
    readonly SearchRequestQueryParamsBuilder _queryParamsBuilder = new();

    public DofusDbTableClient(Uri baseAddress, Uri? referrer = null)
    {
        Referrer = referrer;
        BaseAddress = baseAddress;
        _options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            AllowOutOfOrderMetadataProperties = true,
            Converters =
            {
                new ValueOrFalseJsonConverterFactory(),
                new ValueTupleJsonConverterFactory()
            }
        };
    }

    public Uri BaseAddress { get; }
    public Uri? Referrer { get; }
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<TResource> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = CreateHttpClient();

        using HttpResponseMessage response = await httpClient.GetAsync($"{id}", cancellationToken);
        response.EnsureSuccessStatusCode();

        TResource? result = await response.Content.ReadFromJsonAsync<TResource>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the response content.");
        }

        return result;
    }

    public async Task<int> CountAsync(IReadOnlyCollection<SearchPredicate> predicates, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = CreateHttpClient();

        SearchQuery query = new() { Limit = 0, Predicates = predicates };
        string queryParams = _queryParamsBuilder.BuildQueryParams(query);
        using HttpResponseMessage response = await httpClient.GetAsync($"?{queryParams}", cancellationToken);
        response.EnsureSuccessStatusCode();

        SearchResult<TResource>? result = await response.Content.ReadFromJsonAsync<SearchResult<TResource>>(cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the search result.");
        }

        return result.Total;
    }

    public async Task<SearchResult<TResource>> SearchAsync(SearchQuery query, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = CreateHttpClient();

        string queryParams = _queryParamsBuilder.BuildQueryParams(query);
        string requestUri = string.IsNullOrWhiteSpace(queryParams) ? string.Empty : $"?{queryParams}";

        using HttpResponseMessage response = await httpClient.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();

        SearchResult<TResource>? result = await response.Content.ReadFromJsonAsync<SearchResult<TResource>>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the search result.");
        }

        return result;
    }

    HttpClient CreateHttpClient()
    {
        HttpClient? httpClient = null;
        try
        {
            httpClient = HttpClientFactory?.CreateClient("DofusSharp") ?? new HttpClient();
            httpClient.BaseAddress = BaseAddress;
            httpClient.DefaultRequestHeaders.Referrer = Referrer;
            return httpClient;
        }
        catch
        {
            httpClient?.Dispose();
            throw;
        }
    }
}
