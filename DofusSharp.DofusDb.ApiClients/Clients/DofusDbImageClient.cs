using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Clients;

class DofusDbImageClient<TId> : IDofusDbImageClient<TId>
{
    public DofusDbImageClient(Uri baseAddress, ImageFormat imageFormat, Uri? referrer = null) : this(baseAddress, imageFormat, null, referrer)
    {
    }

    public DofusDbImageClient(Uri baseAddress, ImageFormat imageFormat, string? prefix, Uri? referrer)
    {
        BaseAddress = baseAddress;
        ImageFormat = imageFormat;
        Prefix = prefix;
        Referrer = referrer;
    }

    public Uri BaseAddress { get; }
    public Uri? Referrer { get; }
    public ImageFormat ImageFormat { get; }
    public string? Prefix { get; }
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<Stream> GetImageAsync(TId id, CancellationToken cancellationToken = default)
    {
        string extension = ImageFormat.ToExtension();
        using HttpClient httpClient = DofusDbClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress, Referrer);

        // NOTE: DO NOT dispose the response here, it will be disposed later when the resulting stream is disposed.
        HttpResponseMessage response = await httpClient.GetAsync($"{Prefix}{id}.{extension}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await HttpResponseMessageStream.Create(response);
    }
}
