using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class SpellVariantsClientTest
{
    [Fact]
    public async Task SpellVariantsClient_Should_GetSpell()
    {
        IDofusDbTableClient<DofusDbSpellVariant> client = DofusDbClient.Beta(Constants.Referrer).SpellVariants();
        DofusDbSpellVariant value = await client.GetAsync(16);
        await Verify(value);
    }

    [Fact]
    public async Task SpellVariantsClient_Should_SearchSpellVariants()
    {
        IDofusDbTableClient<DofusDbSpellVariant> client = DofusDbClient.Beta(Constants.Referrer).SpellVariants();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbSpellVariant[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
