using System.Net;
using System.Net.Http.Json;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Common;
using DofusSharp.Dofocus.ApiClients.Models.Runes;
using DofusSharp.Dofocus.ApiClients.Requests;
using DofusSharp.Dofocus.ApiClients.Responses;
using FluentAssertions;
using JetBrains.Annotations;
using Moq;
using Moq.Contrib.HttpClient;
using Xunit;

namespace Tests.UnitTests.Dofocus.ApiClients;

[TestSubject(typeof(DofocusRunesClient))]
public class DofocusRunesClientTest
{
    [Fact]
    public async Task GetRunes_Should_ReturnRunes()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com")
            .ReturnsJsonResponse(
                HttpStatusCode.OK,
                new[]
                {
                    new DofocusRune
                    {
                        Id = 12,
                        Name = new DofocusMultiLangString { Fr = "NAME FR 1", En = "NAME EN 1", Es = "NAME ES 1" },
                        CharacteristicId = 34,
                        CharacteristicName = new DofocusMultiLangString { Fr = "CHAR NAME FR 1", En = "CHAR NAME EN 1", Es = "CHAR NAME ES 1" },
                        Value = 56,
                        Weight = 78,
                        LatestPrices = [new DofocusRunePriceRecord { ServerName = "SERVER 1", Price = 90, DateUpdated = new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.Zero) }]
                    },
                    new DofocusRune
                    {
                        Id = 14,
                        Name = new DofocusMultiLangString { Fr = "NAME FR 1", En = "NAME EN 1", Es = "NAME ES 1" },
                        CharacteristicId = 72,
                        CharacteristicName = new DofocusMultiLangString { Fr = "CHAR NAME FR 1", En = "CHAR NAME EN 1", Es = "CHAR NAME ES 1" },
                        Value = 58,
                        Weight = 36,
                        LatestPrices = [new DofocusRunePriceRecord { ServerName = "SERVER 1", Price = 90, DateUpdated = new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.Zero) }]
                    }
                }
            );
        DofocusRunesClient client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        IReadOnlyCollection<DofocusRune> result = await client.GetRunesAsync();

        result.Should()
            .BeEquivalentTo(
                [
                    new DofocusRune
                    {
                        Id = 12,
                        Name = new DofocusMultiLangString { Fr = "NAME FR 1", En = "NAME EN 1", Es = "NAME ES 1" },
                        CharacteristicId = 34,
                        CharacteristicName = new DofocusMultiLangString { Fr = "CHAR NAME FR 1", En = "CHAR NAME EN 1", Es = "CHAR NAME ES 1" },
                        Value = 56,
                        Weight = 78,
                        LatestPrices = [new DofocusRunePriceRecord { ServerName = "SERVER 1", Price = 90, DateUpdated = new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.Zero) }]
                    },
                    new DofocusRune
                    {
                        Id = 14,
                        Name = new DofocusMultiLangString { Fr = "NAME FR 1", En = "NAME EN 1", Es = "NAME ES 1" },
                        CharacteristicId = 72,
                        CharacteristicName = new DofocusMultiLangString { Fr = "CHAR NAME FR 1", En = "CHAR NAME EN 1", Es = "CHAR NAME ES 1" },
                        Value = 58,
                        Weight = 36,
                        LatestPrices = [new DofocusRunePriceRecord { ServerName = "SERVER 1", Price = 90, DateUpdated = new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.Zero) }]
                    }
                ]
            );
    }

    [Fact]
    public async Task PutRunePrice_Should_ReturnResponse()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Put, "http://base.com/123456")
            .ReturnsJsonResponse(
                HttpStatusCode.OK,
                new PutRunePriceResponse
                {
                    Message = "MESSAGE",
                    NewRunePrice = new NewRunePrice { RuneId = 123, ServerName = "RESPONSE SERVER", Price = 456, DateUpdated = new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.Zero) }
                }
            );
        DofocusRunesClient client = new(new Uri("http://base.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        PutRunePriceResponse result = await client.PutRunePriceAsync(123456, new PutRunePriceRequest { ServerName = "REQUEST SERVER", Price = 987 });

        httpHandlerMock.VerifyRequest(
            HttpMethod.Put,
            "http://base.com/123456",
            message =>
            {
                PutRunePriceRequest? request = message.Content!.ReadFromJsonAsync<PutRunePriceRequest>().Result;
                request.Should().BeEquivalentTo(new PutRunePriceRequest { ServerName = "REQUEST SERVER", Price = 987 });
                return true;
            }
        );

        result.Should()
            .BeEquivalentTo(
                new PutRunePriceResponse
                {
                    Message = "MESSAGE",
                    NewRunePrice = new NewRunePrice { RuneId = 123, ServerName = "RESPONSE SERVER", Price = 456, DateUpdated = new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.Zero) }
                }
            );
    }
}
