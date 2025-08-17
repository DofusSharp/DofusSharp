using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class ItemsClientTest
{
    [Fact]
    public async Task ItemsClient_Should_GetItem_Items()
    {
        IDofusDbTableClient<Item> client = DofusDbClients.Beta().Items();
        Item value = await client.GetAsync(70);
        await Verify(value);
    }

    [Fact]
    public async Task ItemsClient_Should_GetItem_Weapon()
    {
        IDofusDbTableClient<Item> client = DofusDbClients.Beta().Items();
        Item value = await client.GetAsync(44);
        await Verify(value);
    }

    [Fact]
    public async Task ItemsClient_Should_SearchItems()
    {
        IDofusDbTableClient<Item> client = DofusDbClients.Beta().Items();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        SearchQuery query = new();
        Item[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
