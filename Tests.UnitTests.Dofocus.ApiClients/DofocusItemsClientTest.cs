using System.Net;
using System.Net.Http.Json;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Common;
using DofusSharp.Dofocus.ApiClients.Models.Items;
using DofusSharp.Dofocus.ApiClients.Requests;
using DofusSharp.Dofocus.ApiClients.Responses;
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

    [Fact]
    public async Task GetItem_Should_ReturnItem()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com/123456")
            .ReturnsJsonResponse(
                HttpStatusCode.OK,
                new DofocusItem
                {
                    Id = 12,
                    Name = new DofocusMultiLangString { Fr = "NAME FR 1", En = "NAME EN 1", Es = "NAME ES 1" },
                    Level = 34,
                    SuperType = new DofocusSuperType { Id = 56, Name = new DofocusMultiLangString { Fr = "SUPERTYPE FR 1", En = "SUPERTYPE EN 1", Es = "SUPERTYPE ES 1" } },
                    Type = new DofocusType { Id = 78, Name = new DofocusMultiLangString { Fr = "TYPE FR 1", En = "TYPE EN 1", Es = "TYPE ES 1" } },
                    ImageUrl = "http://image1.com",
                    Characteristics =
                    [
                        new DofocusItemCharacteristic
                        {
                            Id = 123,
                            Name = new DofocusMultiLangString { Fr = "CHAR FR 1", En = "CHAR EN 1", Es = "CHAR ES 1" },
                            From = 456,
                            To = 789
                        },
                        new DofocusItemCharacteristic
                        {
                            Id = 147,
                            Name = new DofocusMultiLangString { Fr = "CHAR FR 2", En = "CHAR EN 2", Es = "CHAR ES 2" },
                            From = 258,
                            To = 369
                        }
                    ],
                    Coefficients =
                    [
                        new DofocusCoefficientRecord { ServerName = "SERVER 1", Coefficient = 1234, LastUpdate = new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.Zero) },
                        new DofocusCoefficientRecord { ServerName = "SERVER 2", Coefficient = 5678, LastUpdate = new DateTimeOffset(2, 3, 4, 5, 6, 7, TimeSpan.Zero) }
                    ],
                    Prices =
                    [
                        new DofocusItemPriceRecord { ServerName = "SERVER 3", Price = 1472, LastUpdate = new DateTimeOffset(3, 4, 5, 6, 7, 8, TimeSpan.Zero) },
                        new DofocusItemPriceRecord { ServerName = "SERVER 4", Price = 5836, LastUpdate = new DateTimeOffset(4, 5, 6, 7, 8, 9, TimeSpan.Zero) }
                    ]
                }
            );
        DofocusItemsClient client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        DofocusItem result = await client.GetItemAsync(123456);

        result.Should()
            .BeEquivalentTo(
                new DofocusItem
                {
                    Id = 12,
                    Name = new DofocusMultiLangString { Fr = "NAME FR 1", En = "NAME EN 1", Es = "NAME ES 1" },
                    Level = 34,
                    SuperType = new DofocusSuperType { Id = 56, Name = new DofocusMultiLangString { Fr = "SUPERTYPE FR 1", En = "SUPERTYPE EN 1", Es = "SUPERTYPE ES 1" } },
                    Type = new DofocusType { Id = 78, Name = new DofocusMultiLangString { Fr = "TYPE FR 1", En = "TYPE EN 1", Es = "TYPE ES 1" } },
                    ImageUrl = "http://image1.com",
                    Characteristics =
                    [
                        new DofocusItemCharacteristic
                        {
                            Id = 123,
                            Name = new DofocusMultiLangString { Fr = "CHAR FR 1", En = "CHAR EN 1", Es = "CHAR ES 1" },
                            From = 456,
                            To = 789
                        },
                        new DofocusItemCharacteristic
                        {
                            Id = 147,
                            Name = new DofocusMultiLangString { Fr = "CHAR FR 2", En = "CHAR EN 2", Es = "CHAR ES 2" },
                            From = 258,
                            To = 369
                        }
                    ],
                    Coefficients =
                    [
                        new DofocusCoefficientRecord { ServerName = "SERVER 1", Coefficient = 1234, LastUpdate = new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.Zero) },
                        new DofocusCoefficientRecord { ServerName = "SERVER 2", Coefficient = 5678, LastUpdate = new DateTimeOffset(2, 3, 4, 5, 6, 7, TimeSpan.Zero) }
                    ],
                    Prices =
                    [
                        new DofocusItemPriceRecord { ServerName = "SERVER 3", Price = 1472, LastUpdate = new DateTimeOffset(3, 4, 5, 6, 7, 8, TimeSpan.Zero) },
                        new DofocusItemPriceRecord { ServerName = "SERVER 4", Price = 5836, LastUpdate = new DateTimeOffset(4, 5, 6, 7, 8, 9, TimeSpan.Zero) }
                    ]
                }
            );
    }

    [Fact]
    public async Task PutItemCoefficient_Should_ReturnResponse()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Put, "http://base.com/123456")
            .ReturnsJsonResponse(
                HttpStatusCode.OK,
                new PutItemCoefficientResponse
                {
                    Message = "MESSAGE",
                    Coefficient = new NewItemCoefficient
                        { ItemId = 123, ServerName = "RESPONSE SERVER", Coefficient = 456, DateUpdated = new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.Zero) }
                }
            );
        DofocusItemsClient client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        PutItemCoefficientResponse result = await client.PutItemCoefficientAsync(123456, new PutItemCoefficientRequest { ServerName = "REQUEST SERVER", Coefficient = 987 });

        httpHandlerMock.VerifyRequest(
            HttpMethod.Put,
            "http://base.com/123456",
            message =>
            {
                PutItemCoefficientRequest? request = message.Content!.ReadFromJsonAsync<PutItemCoefficientRequest>().Result;
                request.Should().BeEquivalentTo(new PutItemCoefficientRequest { ServerName = "REQUEST SERVER", Coefficient = 987 });
                return true;
            }
        );

        result.Should()
            .BeEquivalentTo(
                new PutItemCoefficientResponse
                {
                    Message = "MESSAGE",
                    Coefficient = new NewItemCoefficient
                        { ItemId = 123, ServerName = "RESPONSE SERVER", Coefficient = 456, DateUpdated = new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.Zero) }
                }
            );
    }
}
