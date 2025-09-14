using DofusSharp.Common;

namespace DofusSharp.DofusDb.ApiClients.Clients;

/// <inheritdoc />
class DofusDbBreedImagesClient(Uri baseAddress, Uri? referrer) : IDofusDbBreedImagesClient
{
    public Uri BaseAddress { get; } = baseAddress;
    public Uri? Referrer { get; } = referrer;
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<Stream> GetSymbolAsync(long id, CancellationToken cancellationToken = default)
    {
        Uri url = GetSymbolQuery(id);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);

        // NOTE: DO NOT dispose the response here, it will be disposed later when the resulting stream is disposed.
        HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await HttpResponseMessageStream.Create(response);
    }

    public Uri GetSymbolQuery(long id) => new(BaseAddress, $"breeds/symbol_{id}.png");

    public async Task<Stream> GetLogoAsync(long id, CancellationToken cancellationToken = default)
    {
        Uri url = GetLogoQuery(id);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);

        // NOTE: DO NOT dispose the response here, it will be disposed later when the resulting stream is disposed.
        HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await HttpResponseMessageStream.Create(response);
    }

    public Uri GetLogoQuery(long id) => new(BaseAddress, $"breeds/logo_transparent_{id}.png");

    public async Task<Stream> GetHeadAsync(long id, CancellationToken cancellationToken = default)
    {
        Uri url = GetHeadQuery(id);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);

        // NOTE: DO NOT dispose the response here, it will be disposed later when the resulting stream is disposed.
        HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await HttpResponseMessageStream.Create(response);
    }

    public Uri GetHeadQuery(long id) => new(BaseAddress, $"heads/SmallHead_{id}.png");
}
