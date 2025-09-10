using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.Common;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;
using DofusSharp.DofusDb.ApiClients.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Clients;

class DofusDbTableClient<TResource> : IDofusDbTableClient<TResource> where TResource: DofusDbResource
{
    readonly JsonSerializerOptions? _options;
    readonly DofusDbSearchRequestQueryParamsBuilder _queryParamsBuilder = new();

    public DofusDbTableClient(Uri baseAddress, Uri? referrer = null)
    {
        Referrer = referrer;
        BaseAddress = baseAddress;
        _options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            TypeInfoResolver = DofusDbModelsSourceGenerationContext.Default,
            AllowOutOfOrderMetadataProperties = true,
            Converters =
            {
                new JsonStringEnumConverter(),
                new DofusDbValueOrFalseJsonConverterFactory(),
                new DofusDbValueTupleJsonConverterFactory(),
                new DofusDbDateOnlyJsonConverter()
            }
        };
    }

    public Uri BaseAddress { get; }
    public Uri? Referrer { get; }
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<TResource> GetAsync(long id, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress, Referrer);
        using HttpResponseMessage response = await httpClient.GetAsync($"{id}", cancellationToken);
        response.EnsureSuccessStatusCode();

#pragma warning disable IL2026
        TResource? result = await response.Content.ReadFromJsonAsync<TResource>(_options, cancellationToken);
#pragma warning restore IL2026
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the response content.");
        }

        return result;
    }

    public async Task<int> CountAsync(IReadOnlyCollection<DofusDbSearchPredicate> predicates, CancellationToken cancellationToken = default)
    {
        DofusDbSearchQuery query = new() { Limit = 0, Predicates = predicates };
        string queryParams = _queryParamsBuilder.BuildQueryParams(query);

        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress, Referrer);
        using HttpResponseMessage response = await httpClient.GetAsync($"?{queryParams}", cancellationToken);
        response.EnsureSuccessStatusCode();

#pragma warning disable IL2026
        DofusDbSearchResult<TResource>? result = await response.Content.ReadFromJsonAsync<DofusDbSearchResult<TResource>>(_options, cancellationToken);
#pragma warning restore IL2026
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the search result.");
        }

        return result.Total;
    }

    public async Task<DofusDbSearchResult<TResource>> SearchAsync(DofusDbSearchQuery query, CancellationToken cancellationToken = default)
    {
        string queryParams = _queryParamsBuilder.BuildQueryParams(query);
        string requestUri = string.IsNullOrWhiteSpace(queryParams) ? string.Empty : $"?{queryParams}";

        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress, Referrer);
        using HttpResponseMessage response = await httpClient.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();

#pragma warning disable IL2026
        DofusDbSearchResult<TResource>? result = await response.Content.ReadFromJsonAsync<DofusDbSearchResult<TResource>>(_options, cancellationToken);
#pragma warning restore IL2026
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the search result.");
        }

        return result;
    }
}
