using DofusSharp.DofusDb.ApiClients;

namespace Tests.Live.DofusDb.ApiClients;

public class SpellImagesClientTest
{
    [Fact]
    public async Task SpellImagesClient_Should_GetItemImage()
    {
        IDofusDbImagesClient<long> client = DofusDbClient.Beta(Constants.Referrer).SpellImages();
        await using Stream imageStream = await client.GetImageAsync(13204);
        await Verify(imageStream, "png");
    }
}
