using System.Net;
using DofusSharp.DofusDb.ApiClients.Clients;
using DofusSharp.DofusDb.ApiClients.Models.Common;
using FluentAssertions;
using JetBrains.Annotations;
using Moq;
using Moq.Contrib.HttpClient;
using Tests.UnitTests.DofusDb.ApiClients.Extensions;

namespace Tests.UnitTests.DofusDb.ApiClients;

[TestSubject(typeof(DofusDbImagesClient<>))]
public class DofusDbScalableImagesClientTest
{
    [Theory]
    [InlineData(ImageFormat.Jpeg, "jpg")]
    [InlineData(ImageFormat.Png, "png")]
    public async Task GetImage_Should_ReturnImage_WithFormat(ImageFormat imageFormat, string extension)
    {
        byte[] imageBytes = [4, 5, 6];
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, $"http://base.com/1/123.{extension}").ReturnsResponse(HttpStatusCode.OK, imageBytes);
        DofusDbScalableImagesClient<int> client = new(new Uri("http://base.com"), imageFormat)
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await using Stream imageStream = await client.GetImageAsync(123);

        imageStream.ReadToByteArray().Should().BeEquivalentTo(imageBytes);
    }


    [Theory]
    [InlineData(DofusDbImageScale.Full, "1")]
    public async Task GetImage_Should_ReturnImage_WithScale(DofusDbImageScale imageScale, string scaleString)
    {
        byte[] imageBytes = [4, 5, 6];
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, $"http://base.com/{scaleString}/123.jpg").ReturnsResponse(HttpStatusCode.OK, imageBytes);
        DofusDbScalableImagesClient<int> client = new(new Uri("http://base.com"), ImageFormat.Jpeg)
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await using Stream imageStream = await client.GetImageAsync(123, imageScale);

        imageStream.ReadToByteArray().Should().BeEquivalentTo(imageBytes);
    }

    [Fact]
    public async Task GetImage_Should_SetHttpParameters()
    {
        Mock<HttpMessageHandler> httpHandlerMock = new(MockBehavior.Strict);
        httpHandlerMock.SetupRequest(HttpMethod.Get, "http://base.com/1/123.jpg").ReturnsResponse(HttpStatusCode.OK, []);
        DofusDbScalableImagesClient<int> client = new(new Uri("http://base.com"), ImageFormat.Jpeg, new Uri("http://referrer.com"))
        {
            HttpClientFactory = httpHandlerMock.CreateClientFactory()
        };

        await using Stream _ = await client.GetImageAsync(123);

        httpHandlerMock.VerifyRequest(HttpMethod.Get, "http://base.com/1/123.jpg", req => req.Headers.Referrer == new Uri("http://referrer.com"));
    }
}
