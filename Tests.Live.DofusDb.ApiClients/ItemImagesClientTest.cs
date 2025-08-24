using DofusSharp.DofusDb.ApiClients;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class ItemImagesClientTest
{
    [Fact]
    public async Task ItemImagesClient_Should_GetItemImage()
    {
        IDofusDbImageClient<int> client = DofusDbClient.Beta(Constants.Referrer).ItemImages();
        await using Stream imageStream = await client.GetImageAsync(6007);
        await Verify(imageStream, "png");
    }
}
