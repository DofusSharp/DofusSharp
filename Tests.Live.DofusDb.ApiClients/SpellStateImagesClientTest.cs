using DofusSharp.DofusDb.ApiClients;

namespace Tests.Live.DofusDb.ApiClients;

public class SpellStateImagesClientTest
{
    [Fact]
    public async Task SpellStateImagesClient_Should_GetItemImage()
    {
        IDofusDbImageClient<string> client = DofusDbClient.Beta(Constants.Referrer).SpellStateImages();
        await using Stream imageStream = await client.GetImageAsync("stateRooted");
        await Verify(imageStream, "png");
    }
}
