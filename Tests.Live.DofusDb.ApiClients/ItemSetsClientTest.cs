using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class ItemSetsClientTest
{
    [Fact]
    public async Task ItemSetsClient_Should_GetItemSet()
    {
        IDofusDbTableClient<DofusDbItemSet> client = DofusDbClient.Beta(Constants.Referrer).ItemSets();
        DofusDbItemSet value = await client.GetAsync(1);
        await Verify(value);
    }

    [Fact]
    public async Task ItemSetsClient_Should_SearchItemSets()
    {
        IDofusDbTableClient<DofusDbItemSet> client = DofusDbClient.Beta(Constants.Referrer).ItemSets();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbItemSet[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
