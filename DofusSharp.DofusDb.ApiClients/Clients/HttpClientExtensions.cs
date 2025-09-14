using DofusSharp.Common;

namespace DofusSharp.DofusDb.ApiClients.Clients;

static class HttpClientExtensions
{
    public static async Task<Stream> GetImageStreamAsync(this HttpClient httpClient, Uri url, CancellationToken cancellationToken)
    {
        // NOTE: DO NOT dispose the response here, it will be disposed later when the resulting stream is disposed.
        HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await HttpResponseMessageStream.Create(response);
    }
}
