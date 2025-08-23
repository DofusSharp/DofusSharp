using System.Net.Http.Json;
using System.Text.Json;
using DofusSharp.Common;
using DofusSharp.Dofocus.ApiClients.Models.Items;

namespace DofusSharp.Dofocus.ApiClients;

public static class DofocusClient
{
    public static DofocusItemsClient Items() => new(new Uri("https://dofocus.fr"));
}

public class DofocusItemsClient(Uri baseAddress)
{
    readonly JsonSerializerOptions _options = new(JsonSerializerDefaults.Web);

    public Uri BaseAddress { get; } = baseAddress;
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<IReadOnlyCollection<DofocusItemMinimal>> GetItemsAsync(CancellationToken cancellationToken = default)
    {
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, BaseAddress);
        using HttpResponseMessage response = await httpClient.GetAsync("api/items", cancellationToken);
        response.EnsureSuccessStatusCode();

        IReadOnlyCollection<DofocusItemMinimal>? result = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<DofocusItemMinimal>>(_options, cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the results.");
        }

        return result;
    }
}
