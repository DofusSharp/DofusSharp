using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class SpellStatesClientTest
{
    [Fact]
    public async Task SpellStatesClient_Should_GetSpell()
    {
        IDofusDbTableClient<DofusDbSpellState> client = DofusDbClient.Beta(Constants.Referrer).SpellStates();
        DofusDbSpellState value = await client.GetAsync(16);
        await Verify(value);
    }

    [Fact]
    public async Task SpellStatesClient_Should_SearchSpellStates()
    {
        IDofusDbTableClient<DofusDbSpellState> client = DofusDbClient.Beta(Constants.Referrer).SpellStates();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbSpellState[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
