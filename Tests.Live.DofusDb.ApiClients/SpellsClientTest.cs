using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class SpellsClientTest
{
    [Fact]
    public async Task SpellsClient_Should_GetSpell()
    {
        IDofusDbTableClient<DofusDbSpell> client = DofusDbClient.Beta(Constants.Referrer).Spells();
        DofusDbSpell value = await client.GetAsync(202);
        await Verify(value);
    }

    [Fact]
    public async Task SpellsClient_Should_SearchSpells()
    {
        IDofusDbTableClient<DofusDbSpell> client = DofusDbClient.Beta(Constants.Referrer).Spells();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new() { Limit = 1000 };
        DofusDbSpell[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();

        results.Length.Should().Be(1000);
    }
}
