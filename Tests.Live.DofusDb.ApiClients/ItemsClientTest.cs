using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class ItemsClientTest
{
    [Fact]
    public async Task ItemsClient_Should_GetItem_Items()
    {
        IDofusDbTableClient<DofusDbItem> client = DofusDbClient.Beta(Constants.Referrer).Items();
        DofusDbItem value = await client.GetAsync(70);
        await Verify(value);
    }

    [Fact]
    public async Task ItemsClient_Should_GetItem_Weapon()
    {
        IDofusDbTableClient<DofusDbItem> client = DofusDbClient.Beta(Constants.Referrer).Items();
        DofusDbItem value = await client.GetAsync(44);
        value.Should().BeOfType<DofusDbWeapon>();
        await Verify(value);
    }

    [Fact]
    public async Task ItemsClient_Should_SearchItems()
    {
        IDofusDbTableClient<DofusDbItem> client = DofusDbClient.Beta(Constants.Referrer).Items();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.GreaterThanOrEqual("level", "190")] };
        DofusDbItem[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
