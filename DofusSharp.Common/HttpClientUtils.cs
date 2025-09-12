namespace DofusSharp.Common;

public static class HttpClientUtils
{
    public static HttpClient CreateHttpClient(IHttpClientFactory? clientFactory, Uri? referrer = null)
    {
        HttpClient? httpClient = null;
        try
        {
            httpClient = clientFactory?.CreateClient("DofusSharp") ?? new HttpClient();
            if (referrer != null)
            {
                httpClient.DefaultRequestHeaders.Referrer = referrer;
            }
            return httpClient;
        }
        catch
        {
            httpClient?.Dispose();
            throw;
        }
    }
}
