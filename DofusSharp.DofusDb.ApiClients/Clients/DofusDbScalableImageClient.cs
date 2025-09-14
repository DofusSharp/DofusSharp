using DofusSharp.Common;
using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Clients;

/// <inheritdoc />
class DofusDbScalableImagesClient<TId>(Uri baseAddress, ImageFormat imageFormat, Uri? referrer = null) : IDofusDbScalableImagesClient<TId>
{
    public Uri BaseAddress { get; } = baseAddress;
    public ImageFormat ImageFormat { get; } = imageFormat;
    public Uri? Referrer { get; } = referrer;
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public Task<Stream> GetImageAsync(TId id, CancellationToken cancellationToken = default) => GetImageAsync(id, DofusDbImageScale.Full, cancellationToken);

    public async Task<Stream> GetImageAsync(TId id, DofusDbImageScale scale, CancellationToken cancellationToken = default)
    {
        Uri url = GetImageQuery(id, scale);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
        return await httpClient.GetImageStreamAsync(url, cancellationToken);
    }

    public Uri GetImageQuery(TId id) => GetImageQuery(id, DofusDbImageScale.Full);

    public Uri GetImageQuery(TId id, DofusDbImageScale scale)
    {
        string scaleString = scale switch
        {
            DofusDbImageScale.Full => "1",
            DofusDbImageScale.ThreeQuarters => "0.75",
            DofusDbImageScale.Half => "0.5",
            DofusDbImageScale.Quarter => "0.25",
            _ => throw new ArgumentOutOfRangeException(nameof(scale), scale, null)
        };
        string extension = ImageFormat.ToExtension();
        return new Uri(BaseAddress, $"{scaleString}/{id}.{extension}");
    }
}
