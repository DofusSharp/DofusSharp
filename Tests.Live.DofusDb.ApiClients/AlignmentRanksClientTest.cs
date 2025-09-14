using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Alignments;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class AlignmentRanksClientTest
{
    [Fact]
    public async Task AlignmentRanksClient_Should_GetAlignmentRank()
    {
        IDofusDbTableClient<DofusDbAlignmentRank> client = DofusDbClient.Beta(Constants.Referrer).AlignmentRanks();
        DofusDbAlignmentRank value = await client.GetAsync(11);
        await Verify(value);
    }

    [Fact]
    public async Task AlignmentRanksClient_Should_SearchAlignmentRanks()
    {
        IDofusDbTableClient<DofusDbAlignmentRank> client = DofusDbClient.Beta(Constants.Referrer).AlignmentRanks();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbAlignmentRank[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
