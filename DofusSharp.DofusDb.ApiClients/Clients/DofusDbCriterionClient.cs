using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using DofusSharp.Common;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Clients;

public class DofusDbCriterionClient(Uri baseAddress, Uri? referrer, JsonSerializerOptions options) : IDofusDbCriterionClient
{
    public Uri BaseAddress { get; } = baseAddress;
    public Uri? Referrer { get; } = referrer;
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public async Task<DofusDbCriterion?> ParseCriterionAsync(string criterion, DofusDbLanguage? language = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(criterion))
        {
            return null;
        }

        Uri url = ParseCriterionQuery(criterion, language);
        using HttpClient httpClient = HttpClientUtils.CreateHttpClient(HttpClientFactory, null, Referrer);
        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        DofusDbCriterion? result = await response.Content.ReadFromJsonAsync(options.GetTypeInfo<DofusDbCriterion>(), cancellationToken);
        if (result == null)
        {
            throw new InvalidOperationException("Could not deserialize the criterion.");
        }

        return result;
    }

    public Uri ParseCriterionQuery(string criterion, DofusDbLanguage? language = null)
    {
        string queryString = language switch
        {
            DofusDbLanguage.Fr => "?lang=fr",
            DofusDbLanguage.En => "?lang=en",
            _ => ""
        };
        return new Uri(BaseAddress, HttpUtility.UrlEncode(criterion) + queryString);
    }
}
