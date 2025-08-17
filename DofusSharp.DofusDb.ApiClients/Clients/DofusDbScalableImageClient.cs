using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Clients;

class DofusDbScalableImageClient : IDofusDbScalableImageClient
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

    public Task<Stream> GetImageAsync(int id, CancellationToken cancellationToken = default) => GetImageAsync(id, ImageScale.Full, cancellationToken);

    public async Task<Stream> GetImageAsync(int id, ImageScale scale, CancellationToken cancellationToken = default)
    {
        string scaleString = scale switch
        {
            ImageScale.Full => "1",
            ImageScale.ThreeQuarters => "0.75",
            ImageScale.Half => "0.5",
            ImageScale.Quarter => "0.25",
            _ => throw new ArgumentOutOfRangeException(nameof(scale), scale, null)
        };
        string extension = ImageFormat.ToExtension();
        using HttpClient httpClient = DofusDbClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress, Referrer);

        // NOTE: DO NOT dispose the response here, it will be disposed later when the resulting stream is disposed.
        HttpResponseMessage response = await httpClient.GetAsync($"{scaleString}/{id}.{extension}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await HttpResponseMessageStream.Create(response);
    }
}
