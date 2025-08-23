using System.Net;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Servers;
using FluentAssertions;
using JetBrains.Annotations;
using Moq;
using Moq.Contrib.HttpClient;
using Xunit;

namespace Tests.UnitTests.Dofocus.ApiClients;

[TestSubject(typeof(DofocusServersClient))]
public class DofocusServersClientTest
{
    [Fact]
    public async Task GetServers_Should_ReturnServers()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com")
            .ReturnsJsonResponse(
                HttpStatusCode.OK,
                new[]
                {
                    new DofocusServer { Name = "SERVER 1" },
                    new DofocusServer { Name = "SERVER 2" }
                }
            );
        DofocusServersClient client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        IReadOnlyCollection<DofocusServer> result = await client.GetServersAsync();

        result.Should()
            .BeEquivalentTo(
                [
                    new DofocusServer { Name = "SERVER 1" },
                    new DofocusServer { Name = "SERVER 2" }
                ]
            );
    }
}
