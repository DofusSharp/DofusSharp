using DofusSharp.DofusDb.ApiClients;

namespace Tests.Live.DofusDb.ApiClients;

public class MonsterImagesClientTest
{
    [Fact]
    public async Task MonsterImagesClient_Should_GetMonsterImage()
    {
        IDofusDbImagesClient<long> client = DofusDbClient.Beta(Constants.Referrer).MonsterImages();
        await using Stream imageStream = await client.GetImageAsync(1);
        await Verify(imageStream, "png");
    }
}
