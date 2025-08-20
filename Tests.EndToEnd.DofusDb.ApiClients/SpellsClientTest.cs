using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class SpellsClientTest
{
    [Fact]
    public async Task SpellsClient_Should_GetSpell()
    {
        IDofusDbTableClient<Spell> client = DofusDbClient.Beta(Constants.Referrer).Spells();
        Spell value = await client.GetAsync(202);
        await Verify(value);
    }

    [Fact]
    public async Task SpellsClient_Should_SearchSpells()
    {
        IDofusDbTableClient<Spell> client = DofusDbClient.Beta(Constants.Referrer).Spells();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        SearchQuery query = new() { Limit = 1000 };
        Spell[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();

        results.Length.Should().Be(1000);
    }
}
