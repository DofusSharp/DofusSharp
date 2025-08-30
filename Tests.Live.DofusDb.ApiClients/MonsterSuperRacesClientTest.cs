using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class MonsterSuperRacesClientTest
{
    [Fact]
    public async Task MonsterSuperRacesClient_Should_GetMonsterSuperRace()
    {
        IDofusDbTableClient<DofusDbMonsterSuperRace> client = DofusDbClient.Beta().MonsterSuperRaces();
        DofusDbMonsterSuperRace value = await client.GetAsync(2);
        await Verify(value);
    }

    [Fact]
    public async Task MonsterSuperRacesClient_Should_SearchMonsterSuperRaces()
    {
        IDofusDbTableClient<DofusDbMonsterSuperRace> client = DofusDbClient.Beta().MonsterSuperRaces();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbMonsterSuperRace[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
