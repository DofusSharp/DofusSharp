using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using DofusSharp.Common;
using DofusSharp.Dofocus.ApiClients.Models.Items;
using DofusSharp.Dofocus.ApiClients.Requests;
using DofusSharp.Dofocus.ApiClients.Responses;

namespace DofusSharp.Dofocus.ApiClients;

class DofocusItemsClient(Uri baseAddress) : IDofocusItemsClient
{
    readonly JsonSerializerOptions _options = new(JsonSerializerDefaults.Web);

    public Uri BaseAddress { get; } = baseAddress;
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<IReadOnlyCollection<DofocusItemMinimal>> GetItemsAsync(CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress);
        httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));

        using HttpResponseMessage response = await httpClient.GetAsync("", cancellationToken);
        response.EnsureSuccessStatusCode();

        IReadOnlyCollection<DofocusItemMinimal>? result = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<DofocusItemMinimal>>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the results.");
        }

        return result;
    }

    public async Task<DofocusItem> GetItemAsync(long itemId, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress);
        httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));

        using HttpResponseMessage response = await httpClient.GetAsync($"{itemId}", cancellationToken);
        response.EnsureSuccessStatusCode();

        DofocusItem? result = await response.Content.ReadFromJsonAsync<DofocusItem>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the results.");
        }

        return result;
    }

    public async Task<PutItemPriceResponse> PutItemPriceAsync(long itemId, PutItemPriceRequest request, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress);
        httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));

        using HttpResponseMessage response = await httpClient.PutAsync($"{itemId}/prices", JsonContent.Create(request, options: _options), cancellationToken);
        response.EnsureSuccessStatusCode();

        PutItemPriceResponse? result = await response.Content.ReadFromJsonAsync<PutItemPriceResponse>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the results.");
        }

        return result;
    }

    public async Task<PutItemCoefficientResponse> PutItemCoefficientAsync(long itemId, PutItemCoefficientRequest request, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress);
        httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));

        using HttpResponseMessage response = await httpClient.PutAsync($"{itemId}/coefficients", JsonContent.Create(request, options: _options), cancellationToken);
        response.EnsureSuccessStatusCode();

        PutItemCoefficientResponse? result = await response.Content.ReadFromJsonAsync<PutItemCoefficientResponse>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the results.");
        }

        return result;
    }
}
