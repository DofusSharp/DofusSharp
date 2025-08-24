using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class SpellLevelsClientTest
{
    [Fact]
    public async Task SpellLevelsClient_Should_GetSpell()
    {
        IDofusDbTableClient<DofusDbSpellLevel> client = DofusDbClient.Beta(Constants.Referrer).SpellLevels();
        DofusDbSpellLevel value = await client.GetAsync(1018);
        await Verify(value);
    }

    [Fact]
    public async Task SpellLevelsClient_Should_SearchSpellLevels()
    {
        IDofusDbTableClient<DofusDbSpellLevel> client = DofusDbClient.Beta(Constants.Referrer).SpellLevels();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new() { Limit = 1000 };
        DofusDbSpellLevel[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();

        results.Length.Should().Be(1000);
    }
}
