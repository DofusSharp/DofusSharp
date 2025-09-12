using System.Net.Http.Json;
using DofusSharp.Common;
using DofusSharp.DofusDb.ApiClients.Models;

namespace DofusSharp.DofusDb.ApiClients.Clients;

/// <inheritdoc />
class DofusDbVersionClient : IDofusDbVersionClient
{
    public DofusDbVersionClient(Uri baseAddress, Uri? referrer = null)
    {
        Referrer = referrer;
        BaseAddress = baseAddress;
    }

    public Uri BaseAddress { get; }
    public Uri? Referrer { get; }
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<Version> GetVersionAsync(CancellationToken cancellationToken = default)
    {
        Uri url = GetVersionQuery();
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, Referrer);
        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        string? result = await response.Content.ReadFromJsonAsync<string>(DofusDbModelsSourceGenerationContext.Default.String, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the version.");
        }

        return Version.Parse(result);
    }

    public Uri GetVersionQuery() => BaseAddress;
}
