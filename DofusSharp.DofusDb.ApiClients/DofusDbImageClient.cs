using DofusSharp.DofusDb.ApiClients.Http;

namespace DofusSharp.DofusDb.ApiClients;

class DofusDbImageClient : IDofusDbImageClient
{
    public DofusDbImageClient(Uri baseAddress, Uri? referrer = null)
    {
        BaseAddress = baseAddress;
        Referrer = referrer;
    }

    public Uri BaseAddress { get; }
    public Uri? Referrer { get; }
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<Stream> GetImageAsync(int id, CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = CreateHttpClient();

        // NOTE: DO NOT dispose the response here, it will be disposed later when the resulting stream is disposed.
        HttpResponseMessage response = await httpClient.GetAsync($"{id}", cancellationToken);
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
