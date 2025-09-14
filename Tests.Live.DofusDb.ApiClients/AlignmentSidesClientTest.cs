using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Alignments;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class AlignmentSidesClientTest
{
    [Fact]
    public async Task AlignmentSidesClient_Should_GetAlignmentSide()
    {
        IDofusDbTableClient<DofusDbAlignmentSide> client = DofusDbClient.Beta(Constants.Referrer).AlignmentSides();
        DofusDbAlignmentSide value = await client.GetAsync(2);
        await Verify(value);
    }

    [Fact]
    public async Task AlignmentSidesClient_Should_SearchAlignmentSides()
    {
        IDofusDbTableClient<DofusDbAlignmentSide> client = DofusDbClient.Beta(Constants.Referrer).AlignmentSides();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbAlignmentSide[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
