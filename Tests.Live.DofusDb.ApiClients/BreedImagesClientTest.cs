using DofusSharp.DofusDb.ApiClients;

namespace Tests.Live.DofusDb.ApiClients;

public class BreedImagesClientTest
{
    [Fact]
    public async Task BreedImagesClient_Should_GetSymbol()
    {
        IDofusDbBreedImagesClient client = DofusDbClient.Beta(Constants.Referrer).BreedImages();
        await using Stream imageStream = await client.GetSymbolAsync(11);
        await Verify(imageStream, "png");
    }

    [Fact]
    public async Task BreedImagesClient_Should_GetLogo()
    {
        IDofusDbBreedImagesClient client = DofusDbClient.Beta(Constants.Referrer).BreedImages();
        await using Stream imageStream = await client.GetLogoAsync(11);
        await Verify(imageStream, "png");
    }

    [Fact]
    public async Task BreedImagesClient_Should_GetHead()
    {
        IDofusDbBreedImagesClient client = DofusDbClient.Beta(Constants.Referrer).BreedImages();
        await using Stream imageStream = await client.GetHeadAsync(110);
        await Verify(imageStream, "png");
    }
}
