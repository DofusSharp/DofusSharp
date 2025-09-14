using System.Net.Http.Json;
using System.Text.Json;
using DofusSharp.Common;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;
using DofusSharp.DofusDb.ApiClients.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Clients;

/// <inheritdoc />
class DofusDbAlmanaxClient : IDofusDbAlmanaxCalendarClient
{
    readonly JsonSerializerOptions _options;

    public DofusDbAlmanaxClient(Uri baseAddress, Uri? referrer, JsonSerializerOptions options)
    {
        _options = options;
        Referrer = referrer;
        BaseAddress = baseAddress;
    }

    public Uri BaseAddress { get; }
    public Uri? Referrer { get; }
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<DofusDbAlmanaxCalendar> GetAlmanaxAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        Uri url = GetAlmanaxQuery(date);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync(_options.GetTypeInfo<DofusDbAlmanaxCalendar>(), cancellationToken)
               ?? throw new InvalidOperationException("Could not deserialize the almanax calendar.");
    }

    public Uri GetAlmanaxQuery(DateOnly date) => new(BaseAddress, "?date=" + date.ToString("MM/dd/yyyy"));
}
