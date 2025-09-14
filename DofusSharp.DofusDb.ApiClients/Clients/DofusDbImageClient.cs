using DofusSharp.Common;
using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Clients;

/// <inheritdoc />
class DofusDbImagesClient<TId>(Uri baseAddress, ImageFormat imageFormat, Uri? referrer, string? prefix) : IDofusDbImagesClient<TId>
{
    public DofusDbImagesClient(Uri baseAddress, ImageFormat imageFormat, Uri? referrer = null) : this(baseAddress, imageFormat, referrer, null)
    {
    }

    public Uri BaseAddress { get; } = baseAddress;
    public Uri? Referrer { get; } = referrer;
    public ImageFormat ImageFormat { get; } = imageFormat;
    public string? Prefix { get; } = prefix;
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<Stream> GetImageAsync(TId id, CancellationToken cancellationToken = default)
    {
        Uri url = GetImageQuery(id);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
        return await httpClient.GetImageStreamAsync(url, cancellationToken);
    }

    public Uri GetImageQuery(TId id)
    {
        string extension = ImageFormat.ToExtension();
        return new Uri(BaseAddress, $"{Prefix}{id}.{extension}");
    }
}
