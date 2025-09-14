using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Ornaments;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class OrnamentsClientTest
{
    [Fact]
    public async Task OrnamentsClient_Should_GetOrnament()
    {
        IDofusDbTableClient<DofusDbOrnament> client = DofusDbClient.Beta(Constants.Referrer).Ornaments();
        DofusDbOrnament value = await client.GetAsync(13);
        await Verify(value);
    }

    [Fact]
    public async Task OrnamentsClient_Should_SearchOrnaments()
    {
        IDofusDbTableClient<DofusDbOrnament> client = DofusDbClient.Beta(Constants.Referrer).Ornaments();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbOrnament[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
