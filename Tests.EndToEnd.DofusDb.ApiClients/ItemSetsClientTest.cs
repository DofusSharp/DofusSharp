using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class ItemSetsClientTest
{
    [Fact]
    public async Task ItemSetsClient_Should_GetItemSet()
    {
        IDofusDbTableClient<ItemSet> client = DofusDbClients.Beta().ItemSets();
        ItemSet value = await client.GetAsync(1);
        await Verify(value);
    }

    [Fact]
    public async Task ItemSetsClient_Should_SearchItemSets()
    {
        IDofusDbTableClient<ItemSet> client = DofusDbClients.Beta().ItemSets();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        SearchQuery query = new();
        ItemSet[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
