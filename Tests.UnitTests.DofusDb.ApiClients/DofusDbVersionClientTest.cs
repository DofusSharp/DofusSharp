using System.Net;
using DofusSharp.DofusDb.ApiClients.Clients;
using FluentAssertions;
using JetBrains.Annotations;
using Moq;
using Moq.Contrib.HttpClient;

namespace Tests.UnitTests.DofusDb.ApiClients;

[TestSubject(typeof(DofusDbVersionClient))]
public class DofusDbVersionClientTest
{
    [Fact]
    public async Task GetVersion_Should_ReturnVersion()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com").ReturnsJsonResponse(HttpStatusCode.OK, "1.2.3.4");
        DofusDbVersionClient client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        Version version = await client.GetVersionAsync();

        version.Should().BeEquivalentTo(new Version(1, 2, 3, 4));
    }

    [Fact]
    public async Task GetVersion_Should_SetHttpParameters()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com").ReturnsJsonResponse(HttpStatusCode.OK, "1.2.3.4");
        DofusDbVersionClient client = new(new Uri("http://base.com"), new Uri("http://referrer.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.GetVersionAsync();

        httpHandlerMock.VerifyRequest(HttpMethod.Get, "http://base.com", req => req.Headers.Referrer == new Uri("http://referrer.com"));
    }
}
