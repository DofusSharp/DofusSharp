using System.Net;
using System.Text.Json;
using DofusSharp.DofusDb.ApiClients.Clients;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using FluentAssertions;
using JetBrains.Annotations;
using Moq;
using Moq.Contrib.HttpClient;

namespace Tests.UnitTests.DofusDb.ApiClients;

[TestSubject(typeof(DofusDbCriterionClient))]
public class DofusDbCriterionClientTest
{
    readonly JsonSerializerOptions _options = DofusDbModelsSourceGenerationContext.ProdOptions;

    [Fact]
    public async Task GetCriterion_Should_ReturnCriterion()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com/CRITERION").ReturnsJsonResponse(HttpStatusCode.OK, (string[])["TEXT"]);
        DofusDbCriterionClient client = new(new Uri("http://base.com"), null, _options)
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        DofusDbCriterion? criterion = await client.ParseCriterionAsync("CRITERION");

        criterion.Should().BeOfType<DofusDbCriterionText>().And.BeEquivalentTo(new DofusDbCriterionText("TEXT"));
    }

    [Theory]
    [InlineData(DofusDbLanguage.En, "en")]
    [InlineData(DofusDbLanguage.Fr, "fr")]
    public async Task GetCriterion_Should_SetLanguage(DofusDbLanguage language, string languageStr)
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, $"http://base.com/CRITERION?lang={languageStr}").ReturnsJsonResponse(HttpStatusCode.OK, (string[])["TEXT"]);
        DofusDbCriterionClient client = new(new Uri("http://base.com"), null, _options)
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.ParseCriterionAsync("CRITERION", language);

        httpHandlerMock.VerifyRequest(HttpMethod.Get, $"http://base.com/CRITERION?lang={languageStr}");
    }

    [Fact]
    public async Task GetCriterion_Should_SetHttpParameters()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com/CRITERION").ReturnsJsonResponse(HttpStatusCode.OK, (string[])["TEXT"]);
        DofusDbCriterionClient client = new(new Uri("http://base.com"), new Uri("http://referrer.com"), _options)
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.ParseCriterionAsync("CRITERION");

        httpHandlerMock.VerifyRequest(HttpMethod.Get, "http://base.com/CRITERION", req => req.Headers.Referrer == new Uri("http://referrer.com"));
    }
}
