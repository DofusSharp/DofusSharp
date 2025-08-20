using DofusSharp.DofusDb.ApiClients;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class SpellImagesClientTest
{
    [Fact]
    public async Task SpellImagesClient_Should_GetItemImage()
    {
        IDofusDbImageClient<int> client = DofusDbClient.Beta(Constants.Referrer).SpellImages();
        await using Stream imageStream = await client.GetImageAsync(13204);
        await Verify(imageStream, "png");
    }
}
