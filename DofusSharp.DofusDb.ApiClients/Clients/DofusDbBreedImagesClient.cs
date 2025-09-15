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
        Uri url = GetSymbolRequestUri(id);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
        return await httpClient.GetImageStreamAsync(url, cancellationToken);
    }

    public Uri GetSymbolRequestUri(long id) => new(BaseAddress, $"breeds/symbol_{id}.png");

    public async Task<Stream> GetLogoAsync(long id, CancellationToken cancellationToken = default)
    {
        Uri url = GetLogoRequestUri(id);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
        return await httpClient.GetImageStreamAsync(url, cancellationToken);
    }

    public Uri GetLogoRequestUri(long id) => new(BaseAddress, $"breeds/logo_transparent_{id}.png");

    public async Task<Stream> GetHeadAsync(long id, CancellationToken cancellationToken = default)
    {
        Uri url = GetHeadRequestUri(id);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
        return await httpClient.GetImageStreamAsync(url, cancellationToken);
    }

    public Uri GetHeadRequestUri(long id) => new(BaseAddress, $"heads/SmallHead_{id}.png");
}
