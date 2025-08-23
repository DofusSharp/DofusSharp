using System.Net;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Common;
using DofusSharp.Dofocus.ApiClients.Models.Items;
using FluentAssertions;
using JetBrains.Annotations;
using Moq;
using Moq.Contrib.HttpClient;
using Xunit;

namespace Tests.UnitTests.Dofocus.ApiClients;

[TestSubject(typeof(DofocusItemsClient))]
public class DofocusItemsClientTest
{
    [Fact]
    public async Task GetItems_Should_ReturnItems()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com")
            .ReturnsJsonResponse(
                HttpStatusCode.OK,
                new[]
                {
                    new DofocusItemMinimal
                    {
                        Id = 12,
                        Name = new DofocusMultiLangString { Fr = "NAME FR 1", En = "NAME EN 1", Es = "NAME ES 1" },
                        Level = 34,
                        SuperType = new DofocusSuperTypeMinimal { Id = 56 },
                        ImageUrl = "http://image1.com",
                        Characteristics = [new DofocusItemCharacteristicsMinimal { Id = 78 }, new DofocusItemCharacteristicsMinimal { Id = 90 }]
                    },
                    new DofocusItemMinimal
                    {
                        Id = 14,
                        Name = new DofocusMultiLangString { Fr = "NAME FR 1", En = "NAME EN 1", Es = "NAME ES 1" },
                        Level = 72,
                        SuperType = new DofocusSuperTypeMinimal { Id = 58 },
                        ImageUrl = "http://image1.com",
                        Characteristics = [new DofocusItemCharacteristicsMinimal { Id = 36 }, new DofocusItemCharacteristicsMinimal { Id = 90 }]
                    }
                }
            );
        DofocusItemsClient client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        IReadOnlyCollection<DofocusItemMinimal> result = await client.GetItemsAsync();

        result.Should()
            .BeEquivalentTo(
                [
                    new DofocusItemMinimal
                    {
                        Id = 12,
                        Name = new DofocusMultiLangString { Fr = "NAME FR 1", En = "NAME EN 1", Es = "NAME ES 1" },
                        Level = 34,
                        SuperType = new DofocusSuperTypeMinimal { Id = 56 },
                        ImageUrl = "http://image1.com",
                        Characteristics = [new DofocusItemCharacteristicsMinimal { Id = 78 }, new DofocusItemCharacteristicsMinimal { Id = 90 }]
                    },
                    new DofocusItemMinimal
                    {
                        Id = 14,
                        Name = new DofocusMultiLangString { Fr = "NAME FR 1", En = "NAME EN 1", Es = "NAME ES 1" },
                        Level = 72,
                        SuperType = new DofocusSuperTypeMinimal { Id = 58 },
                        ImageUrl = "http://image1.com",
                        Characteristics = [new DofocusItemCharacteristicsMinimal { Id = 36 }, new DofocusItemCharacteristicsMinimal { Id = 90 }]
                    }
                ]
            );
    }
}
