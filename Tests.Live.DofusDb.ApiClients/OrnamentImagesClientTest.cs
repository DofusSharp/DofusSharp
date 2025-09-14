using DofusSharp.DofusDb.ApiClients;

namespace Tests.Live.DofusDb.ApiClients;

public class OrnamentImagesClientTest
{
    [Fact]
    public async Task OrnamentImagesClient_Should_GetItemImage()
    {
        IDofusDbImagesClient<long> client = DofusDbClient.Beta(Constants.Referrer).OrnamentImages();
        await using Stream imageStream = await client.GetImageAsync(1);
        await Verify(imageStream, "png");
    }
}
