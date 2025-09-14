using System.Net.Http.Json;
using DofusSharp.Common;
using DofusSharp.DofusDb.ApiClients.Models;

namespace DofusSharp.DofusDb.ApiClients.Clients;

/// <inheritdoc />
class DofusDbVersionClient(Uri baseAddress, Uri? referrer = null) : IDofusDbVersionClient
{
    public Uri BaseAddress { get; } = baseAddress;
    public Uri? Referrer { get; } = referrer;
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<Version> GetVersionAsync(CancellationToken cancellationToken = default)
    {
        Uri url = GetVersionQuery();
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
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
