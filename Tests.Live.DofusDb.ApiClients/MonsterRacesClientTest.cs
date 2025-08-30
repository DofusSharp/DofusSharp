using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class MonsterRacesClientTest
{
    [Fact]
    public async Task MonsterRacesClient_Should_GetMonsterRace()
    {
        IDofusDbTableClient<DofusDbMonsterRace> client = DofusDbClient.Beta().MonsterRaces();
        DofusDbMonsterRace value = await client.GetAsync(31);
        await Verify(value);
    }

    [Fact]
    public async Task MonsterRacesClient_Should_SearchMonsterRaces()
    {
        IDofusDbTableClient<DofusDbMonsterRace> client = DofusDbClient.Beta().MonsterRaces();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbMonsterRace[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
