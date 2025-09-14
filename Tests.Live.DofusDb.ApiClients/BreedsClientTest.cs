using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Breeds;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class BreedsClientTest
{
    [Fact]
    public async Task BreedsClient_Should_GetBreed()
    {
        IDofusDbTableClient<DofusDbBreed> client = DofusDbClient.Beta(Constants.Referrer).Breeds();
        DofusDbBreed value = await client.GetAsync(1);
        await Verify(value);
    }

    [Fact]
    public async Task BreedsClient_Should_SearchBreeds()
    {
        IDofusDbTableClient<DofusDbBreed> client = DofusDbClient.Beta(Constants.Referrer).Breeds();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbBreed[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
