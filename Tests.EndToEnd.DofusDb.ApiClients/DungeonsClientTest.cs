using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class DungeonsClientTest
{
    [Fact]
    public async Task DungeonsClient_Should_GetDungeon()
    {
        IDofusDbTableClient<Dungeon> client = DofusDbClient.Beta().Dungeons();
        Dungeon value = await client.GetAsync(1);
        await Verify(value);
    }

    [Fact]
    public async Task DungeonsClient_Should_SearchDungeons()
    {
        IDofusDbTableClient<Dungeon> client = DofusDbClient.Beta().Dungeons();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        SearchQuery query = new();
        Dungeon[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
