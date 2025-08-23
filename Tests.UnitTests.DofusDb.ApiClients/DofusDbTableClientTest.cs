using System.Net;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Clients;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;
using JetBrains.Annotations;
using Moq;
using Moq.Contrib.HttpClient;

namespace Tests.UnitTests.DofusDb.ApiClients;

[TestSubject(typeof(DofusDbTableClient<>))]
public class DofusDbTableClientTest
{
    [Fact]
    public async Task Count_Should_ReturnCount()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com?$limit=0")
            .ReturnsJsonResponse(HttpStatusCode.OK, new DofusDbSearchResult<EntityForTest> { Total = 123, Limit = 0, Skip = 0, Data = [] });
        DofusDbTableClient<EntityForTest> client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        int result = await client.CountAsync();

        result.Should().Be(123);
    }

    [Fact]
    public async Task Count_Should_SetHttpParameters()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com?$limit=0")
            .ReturnsJsonResponse(HttpStatusCode.OK, new DofusDbSearchResult<EntityForTest> { Total = 0, Limit = 0, Skip = 0, Data = [] });
        DofusDbTableClient<EntityForTest> client = new(new Uri("http://base.com"), new Uri("http://referrer.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.CountAsync();

        httpHandlerMock.VerifyRequest(HttpMethod.Get, "http://base.com?$limit=0", req => req.Headers.Referrer == new Uri("http://referrer.com"));
    }

    [Fact]
    public async Task Count_Should_SetQueryParams()
    {
        DofusDbSearchQuery expectedQuery = new()
        {
            Limit = 0,
            Predicates = [new DofusDbSearchPredicate.Eq("prop3", "value")]
        };
        string requestUrl = $"http://base.com?{expectedQuery.ToQueryString()}";

        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, requestUrl)
            .ReturnsJsonResponse(HttpStatusCode.OK, new DofusDbSearchResult<EntityForTest> { Total = 0, Limit = 0, Skip = 0, Data = [] });
        DofusDbTableClient<EntityForTest> client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.CountAsync(expectedQuery.Predicates);

        httpHandlerMock.VerifyRequest(HttpMethod.Get, requestUrl);
    }

    [Fact]
    public async Task Get_Should_ReturnEntity()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com/123").ReturnsJsonResponse(HttpStatusCode.OK, new EntityForTest { Prop1 = "Test", Prop2 = 42, Prop3 = true });
        DofusDbTableClient<EntityForTest> client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        EntityForTest result = await client.GetAsync(123);

        result.Should().BeEquivalentTo(new EntityForTest { Prop1 = "Test", Prop2 = 42, Prop3 = true });
    }

    [Fact]
    public async Task Get_Should_SetHttpParameters()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com/123").ReturnsJsonResponse(HttpStatusCode.OK, new EntityForTest());
        DofusDbTableClient<EntityForTest> client = new(new Uri("http://base.com"), new Uri("http://referrer.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.GetAsync(123);

        httpHandlerMock.VerifyRequest(HttpMethod.Get, "http://base.com/123", req => req.Headers.Referrer == new Uri("http://referrer.com"));
    }

    [Fact]
    public async Task Search_Should_ReturnSearchResult()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com")
            .ReturnsJsonResponse(
                HttpStatusCode.OK,
                new DofusDbSearchResult<EntityForTest>
                {
                    Total = 123, Limit = 456, Skip = 789, Data =
                    [
                        new EntityForTest { Prop1 = "Test", Prop2 = 42, Prop3 = true },
                        new EntityForTest { Prop1 = "Another Test", Prop2 = 24, Prop3 = false }
                    ]
                }
            );
        DofusDbTableClient<EntityForTest> client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        DofusDbSearchResult<EntityForTest> result = await client.SearchAsync(new DofusDbSearchQuery());

        result.Should()
            .BeEquivalentTo(
                new DofusDbSearchResult<EntityForTest>
                {
                    Total = 123, Limit = 456, Skip = 789, Data =
                    [
                        new EntityForTest { Prop1 = "Test", Prop2 = 42, Prop3 = true },
                        new EntityForTest { Prop1 = "Another Test", Prop2 = 24, Prop3 = false }
                    ]
                }
            );
    }

    [Fact]
    public async Task Search_Should_SetHttpParameters()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com")
            .ReturnsJsonResponse(HttpStatusCode.OK, new DofusDbSearchResult<EntityForTest> { Total = 0, Limit = 0, Skip = 0, Data = [] });
        DofusDbTableClient<EntityForTest> client = new(new Uri("http://base.com"), new Uri("http://referrer.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.SearchAsync(new DofusDbSearchQuery());

        httpHandlerMock.VerifyRequest(HttpMethod.Get, "http://base.com", req => req.Headers.Referrer == new Uri("http://referrer.com"));
    }

    [Fact]
    public async Task Search_Should_SetQueryParams()
    {
        DofusDbSearchQuery query = new()
        {
            Limit = 123, Skip = 456, Sort = new Dictionary<string, DofusDbSearchQuerySortOrder> { { "prop1", DofusDbSearchQuerySortOrder.Ascending } }, Select = ["prop2"],
            Predicates = [new DofusDbSearchPredicate.Eq("prop3", "value")]
        };
        string requestUrl = $"http://base.com?{query.ToQueryString()}";

        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, requestUrl)
            .ReturnsJsonResponse(HttpStatusCode.OK, new DofusDbSearchResult<EntityForTest> { Total = 0, Limit = 0, Skip = 0, Data = [] });
        DofusDbTableClient<EntityForTest> client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await client.SearchAsync(query);

        httpHandlerMock.VerifyRequest(HttpMethod.Get, requestUrl);
    }

    class EntityForTest : DofusDbEntity
    {
        public string Prop1 { get; set; } = "";
        public int Prop2 { get; set; }
        public bool Prop3 { get; set; }
    }
}
