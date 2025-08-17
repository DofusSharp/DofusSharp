using System.Net;
using DofusSharp.DofusDb.ApiClients.Clients;
using DofusSharp.DofusDb.ApiClients.Models.Common;
using FluentAssertions;
using JetBrains.Annotations;
using Moq;
using Moq.Contrib.HttpClient;
using Tests.UnitTests.DofusDb.ApiClients.Extensions;

namespace Tests.UnitTests.DofusDb.ApiClients;

[TestSubject(typeof(DofusDbImageClient))]
public class DofusDbImageClientTest
{
    [Theory]
    [InlineData(ImageFormat.Jpeg, "jpg")]
    [InlineData(ImageFormat.Png, "png")]
    public async Task GetImage_Should_ReturnImage(ImageFormat imageFormat, string extension)
    {
        byte[] imageBytes = [4, 5, 6];
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, $"http://base.com/123.{extension}").ReturnsResponse(HttpStatusCode.OK, imageBytes);
        DofusDbImageClient client = new(new Uri("http://base.com"), imageFormat)
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await using Stream imageStream = await client.GetImageAsync(123);

        imageStream.ReadToByteArray().Should().BeEquivalentTo(imageBytes);
    }

    [Fact]
    public async Task GetImage_Should_SetHttpParameters()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com/123.jpg").ReturnsResponse(HttpStatusCode.OK, "1.2.3.4");
        DofusDbImageClient client = new(new Uri("http://base.com"), ImageFormat.Jpeg, new Uri("http://referrer.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await using Stream _ = await client.GetImageAsync(123);

        httpHandlerMock.VerifyRequest(HttpMethod.Get, "http://base.com/123.jpg", req => req.Headers.Referrer == new Uri("http://referrer.com"));
    }
}
