using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class MonstersClientTest
{
    [Fact]
    public async Task MonstersClient_Should_GetMonster()
    {
        IDofusDbTableClient<DofusDbMonster> client = DofusDbClient.Beta().Monsters();
        DofusDbMonster value = await client.GetAsync(31);
        await Verify(value);
    }

    [Fact]
    public async Task MonstersClient_Should_SearchMonsters()
    {
        IDofusDbTableClient<DofusDbMonster> client = DofusDbClient.Beta().Monsters();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.In("race", "9", "10", "11", "49", "69", "135", "174", "182", "256")] };
        DofusDbMonster[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
