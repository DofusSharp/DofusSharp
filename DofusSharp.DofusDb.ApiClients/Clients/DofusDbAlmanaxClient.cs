using System.Net.Http.Json;
using System.Text.Json;
using DofusSharp.Common;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;
using DofusSharp.DofusDb.ApiClients.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Clients;

/// <inheritdoc />
class DofusDbAlmanaxClient(Uri baseAddress, Uri? referrer, JsonSerializerOptions options) : IDofusDbAlmanaxCalendarClient
{
    public Uri BaseAddress { get; } = baseAddress;
    public Uri? Referrer { get; } = referrer;
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<DofusDbAlmanaxCalendar> GetAlmanaxAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        Uri url = GetAlmanaxQuery(date);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync(options.GetTypeInfo<DofusDbAlmanaxCalendar>(), cancellationToken)
               ?? throw new InvalidOperationException("Could not deserialize the almanax calendar.");
    }

    public Uri GetAlmanaxQuery(DateOnly date) => new(BaseAddress, "?date=" + date.ToString("MM/dd/yyyy"));
}
