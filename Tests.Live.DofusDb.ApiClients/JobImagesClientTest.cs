using DofusSharp.DofusDb.ApiClients;

namespace Tests.Live.DofusDb.ApiClients;

public class JobImagesClientTest
{
    [Fact(Skip = "The job images API doesn't seem to work.")]
    public async Task JobImagesClient_Should_GetItemImage()
    {
        IDofusDbImagesClient<long> client = DofusDbClient.Beta(Constants.Referrer).JobImages();
        await using Stream imageStream = await client.GetImageAsync(11);
        await Verify(imageStream, "jpg");
    }
}
