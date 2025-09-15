using DofusSharp.DofusDb.ApiClients;

namespace Tests.Live.DofusDb.ApiClients;

public class ChallengeImagesClientTest
{
    [Fact]
    public async Task ChallengeImagesClient_Should_GetChallengeImage()
    {
        IDofusDbImagesClient<long> client = DofusDbClient.Beta(Constants.Referrer).ChallengeImages();
        await using Stream imageStream = await client.GetImageAsync(10);
        await Verify(imageStream, "png");
    }
}
