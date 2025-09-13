using System.Net.Http.Json;
using System.Web;
using DofusSharp.Common;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;
using DofusSharp.DofusDb.ApiClients.Models.Servers;

namespace DofusSharp.DofusDb.ApiClients.Clients;

public class DofusDbCriterionClient : IDofusDbCriterionClient
{
    readonly DofusDbModelsSourceGenerationContext _context;

    public DofusDbCriterionClient(Uri baseAddress, Uri? referrer = null)
    {
        Referrer = referrer;
        BaseAddress = baseAddress;
        _context = DofusDbModelsSourceGenerationContext.Instance;
    }

    public Uri BaseAddress { get; }
    public Uri? Referrer { get; }
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

        DofusDbCriterion? result = await response.Content.ReadFromJsonAsync(_context.DofusDbCriterion, cancellationToken);
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
