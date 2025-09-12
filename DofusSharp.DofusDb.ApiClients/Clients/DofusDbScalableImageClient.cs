using DofusSharp.Common;
using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Clients;

class DofusDbScalableImageClient<TId> : IDofusDbScalableImageClient<TId>
{
    public DofusDbScalableImageClient(Uri baseAddress, ImageFormat imageFormat, Uri? referrer = null)
    {
        BaseAddress = baseAddress;
        ImageFormat = imageFormat;
        Referrer = referrer;
    }

    public Uri BaseAddress { get; }
    public ImageFormat ImageFormat { get; }
    public Uri? Referrer { get; }
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public Task<Stream> GetImageAsync(TId id, CancellationToken cancellationToken = default) => GetImageAsync(id, DofusDbImageScale.Full, cancellationToken);

    public async Task<Stream> GetImageAsync(TId id, DofusDbImageScale scale, CancellationToken cancellationToken = default)
    {
        Uri url = GetImageQuery(id, scale);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, Referrer);

        // NOTE: DO NOT dispose the response here, it will be disposed later when the resulting stream is disposed.
        HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await HttpResponseMessageStream.Create(response);
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
