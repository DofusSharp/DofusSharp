using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class SpellLevelsClientTest
{
    [Fact]
    public async Task SpellLevelsClient_Should_GetSpell()
    {
        IDofusDbTableClient<SpellLevel> client = DofusDbClient.Beta(Constants.Referrer).SpellLevels();
        SpellLevel value = await client.GetAsync(1018);
        await Verify(value);
    }

    [Fact]
    public async Task SpellLevelsClient_Should_SearchSpellLevels()
    {
        IDofusDbTableClient<SpellLevel> client = DofusDbClient.Beta(Constants.Referrer).SpellLevels();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        SearchQuery query = new() { Limit = 1000 };
        SpellLevel[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();

        results.Length.Should().Be(1000);
    }
}
