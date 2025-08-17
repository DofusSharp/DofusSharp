using System.Net;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;
using Moq;
using Moq.Contrib.HttpClient;

namespace Tests.UnitTests.DofusDb.ApiClients;

public class DofusDbApiClientTest
{
    [Fact]
    public async Task Count_Should_SetHttpParameters()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com?$limit=0")
            .ReturnsJsonResponse(HttpStatusCode.OK, new SearchResult<EntityForTest> { Total = 0, Limit = 0, Skip = 0, Data = [] });
        DofusDbApiClient<EntityForTest> client = new(new Uri("http://base.com"), new Uri("http://referrer.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.CountAsync();

        httpHandlerMock.VerifyRequest(HttpMethod.Get, "http://base.com?$limit=0", req => req.Headers.Referrer == new Uri("http://referrer.com"));
    }

    [Fact]
    public async Task Get_Should_SetHttpParameters()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com/123").ReturnsJsonResponse(HttpStatusCode.OK, new EntityForTest());
        DofusDbApiClient<EntityForTest> client = new(new Uri("http://base.com"), new Uri("http://referrer.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.GetAsync(123);

        httpHandlerMock.VerifyRequest(HttpMethod.Get, "http://base.com/123", req => req.Headers.Referrer == new Uri("http://referrer.com"));
    }

    [Fact]
    public async Task Search_Should_SetHttpParameters()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com")
            .ReturnsJsonResponse(HttpStatusCode.OK, new SearchResult<EntityForTest> { Total = 0, Limit = 0, Skip = 0, Data = [] });
        DofusDbApiClient<EntityForTest> client = new(new Uri("http://base.com"), new Uri("http://referrer.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.SearchAsync(new SearchQuery());

        httpHandlerMock.VerifyRequest(HttpMethod.Get, "http://base.com", req => req.Headers.Referrer == new Uri("http://referrer.com"));
    }

    class EntityForTest : DofusDbEntity;
}
