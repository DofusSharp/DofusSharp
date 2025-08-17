using DofusSharp.DofusDb.ApiClients;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class MapImagesClientTest
{
    [Fact]
    public async Task MapImagesClient_Should_GetMapImage()
    {
        IDofusDbImageClient client = DofusDbClient.Beta(Constants.Referrer).MapImages();
        await using Stream imageStream = await client.GetImageAsync(3333);
        await Verify(imageStream, "jpg");
    }
}
