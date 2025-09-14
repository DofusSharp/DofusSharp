using System.Net;
using System.Text.Json;
using DofusSharp.DofusDb.ApiClients.Clients;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;
using FluentAssertions;
using JetBrains.Annotations;
using Moq;
using Moq.Contrib.HttpClient;

namespace Tests.UnitTests.DofusDb.ApiClients;

[TestSubject(typeof(DofusDbAlmanaxClient))]
public class DofusDbAlmanaxClientTest
{
    readonly JsonSerializerOptions _options = DofusDbModelsSourceGenerationContext.ProdOptions;

    [Fact]
    public async Task GetAlmanax_Should_ReturnAlmanax()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com?date=05/06/0004").ReturnsJsonResponse(HttpStatusCode.OK, new { Id = 123 });
        DofusDbAlmanaxClient client = new(new Uri("http://base.com"), null, _options)
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        DofusDbAlmanaxCalendar almanax = await client.GetAlmanaxAsync(new DateOnly(4, 5, 6));

        almanax.Should().BeOfType<DofusDbAlmanaxCalendar>().And.BeEquivalentTo(new DofusDbAlmanaxCalendar { Id = 123 });
    }

    [Fact]
    public async Task GetAlmanax_Should_SetHttpParameters()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com?date=05/06/0004").ReturnsJsonResponse(HttpStatusCode.OK, new { Id = 123 });
        DofusDbAlmanaxClient client = new(new Uri("http://base.com"), new Uri("http://referrer.com"), _options)
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.GetAlmanaxAsync(new DateOnly(4, 5, 6));

        httpHandlerMock.VerifyRequest(HttpMethod.Get, "http://base.com?date=05/06/0004", req => req.Headers.Referrer == new Uri("http://referrer.com"));
    }
}
