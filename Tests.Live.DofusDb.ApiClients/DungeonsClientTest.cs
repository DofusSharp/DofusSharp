using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class DungeonsClientTest
{
    [Fact]
    public async Task DungeonsClient_Should_GetDungeon()
    {
        IDofusDbTableClient<DofusDbDungeon> client = DofusDbClient.Beta().Dungeons();
        DofusDbDungeon value = await client.GetAsync(1);
        await Verify(value);
    }

    [Fact]
    public async Task DungeonsClient_Should_SearchDungeons()
    {
        IDofusDbTableClient<DofusDbDungeon> client = DofusDbClient.Beta().Dungeons();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbDungeon[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
