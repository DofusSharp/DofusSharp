using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using DofusSharp.Common;
using DofusSharp.Dofocus.ApiClients.Models.Runes;
using DofusSharp.Dofocus.ApiClients.Requests;
using DofusSharp.Dofocus.ApiClients.Responses;

namespace DofusSharp.Dofocus.ApiClients;

public class DofocusRunesClient(Uri baseAddress)
{
    readonly JsonSerializerOptions _options = new(JsonSerializerDefaults.Web);

    public Uri BaseAddress { get; } = baseAddress;
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<IReadOnlyCollection<DofocusRune>> GetRunesAsync(CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress);
        httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));

        using HttpResponseMessage response = await httpClient.GetAsync("", cancellationToken);
        response.EnsureSuccessStatusCode();

        IReadOnlyCollection<DofocusRune>? result = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<DofocusRune>>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the results.");
        }

        return result;
    }

    public async Task<PutRunePriceResponse> PutRunePriceAsync(long runeId, PutRunePriceRequest request, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress);
        httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));

        using HttpResponseMessage response = await httpClient.PutAsync($"{runeId}", JsonContent.Create(request, options: _options), cancellationToken);
        response.EnsureSuccessStatusCode();

        PutRunePriceResponse? result = await response.Content.ReadFromJsonAsync<PutRunePriceResponse>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the results.");
        }

        return result;
    }
}
