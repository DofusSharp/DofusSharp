using DofusSharp.DofusDb.ApiClients.Http;
using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients;

class DofusDbImageClient : IDofusDbImageClient
{
    public DofusDbImageClient(Uri baseAddress, ImageFormat imageFormat, Uri? referrer = null)
    {
        BaseAddress = baseAddress;
        ImageFormat = imageFormat;
        Referrer = referrer;
    }

    public Uri BaseAddress { get; }
    public Uri? Referrer { get; }
    public ImageFormat ImageFormat { get; }
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<Stream> GetImageAsync(int id, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = CreateHttpClient();

        string extension = ImageFormat.ToExtension();

        // NOTE: DO NOT dispose the response here, it will be disposed later when the resulting stream is disposed.
        HttpResponseMessage response = await httpClient.GetAsync($"{id}.{extension}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await HttpResponseMessageStream.Create(response);
    }

    HttpClient CreateHttpClient()
    {
        HttpClient? httpClient = null;
        try
        {
            httpClient = HttpClientFactory?.CreateClient("DofusSharp") ?? new HttpClient();
            httpClient.BaseAddress = BaseAddress;
            httpClient.DefaultRequestHeaders.Referrer = Referrer;
            return httpClient;
        }
        catch
        {
            httpClient?.Dispose();
            throw;
        }
    }
}
