using System.Net.Http.Json;

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
        using HttpClient httpClient = DofusDbClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress, Referrer);
        using HttpResponseMessage response = await httpClient.GetAsync(string.Empty, cancellationToken);
        response.EnsureSuccessStatusCode();

        string? result = await response.Content.ReadFromJsonAsync<string>(cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the version.");
        }

        return Version.Parse(result);
    }
}
