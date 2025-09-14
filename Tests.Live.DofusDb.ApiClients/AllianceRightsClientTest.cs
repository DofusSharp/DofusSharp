using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Social;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class AllianceRightsClientTest
{
    [Fact]
    public async Task AllianceRightsClient_Should_GetAllianceRight()
    {
        IDofusDbTableClient<DofusDbAllianceRight> client = DofusDbClient.Beta(Constants.Referrer).AllianceRights();
        DofusDbAllianceRight value = await client.GetAsync(11);
        await Verify(value);
    }

    [Fact]
    public async Task AllianceRightsClient_Should_SearchAllianceRights()
    {
        IDofusDbTableClient<DofusDbAllianceRight> client = DofusDbClient.Beta(Constants.Referrer).AllianceRights();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbAllianceRight[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
