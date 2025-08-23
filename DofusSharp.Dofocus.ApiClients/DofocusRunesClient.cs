using System.Net.Http.Json;
using System.Text.Json;
using DofusSharp.Common;
using DofusSharp.Dofocus.ApiClients.Models.Runes;

namespace DofusSharp.Dofocus.ApiClients;

public class DofocusRunesClient(Uri baseAddress)
{
    readonly JsonSerializerOptions _options = new(JsonSerializerDefaults.Web);

    public Uri BaseAddress { get; } = baseAddress;
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<IReadOnlyCollection<DofocusRune>> GetRunesAsync(CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress);
        using HttpResponseMessage response = await httpClient.GetAsync("", cancellationToken);
        response.EnsureSuccessStatusCode();

        IReadOnlyCollection<DofocusRune>? result = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<DofocusRune>>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the results.");
        }

        return result;
    }
}
