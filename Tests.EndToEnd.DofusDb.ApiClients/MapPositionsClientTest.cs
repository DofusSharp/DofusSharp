using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class MapPositionsClientTest
{
    [Fact]
    public async Task MapPositionsClient_Should_GetMapPosition()
    {
        IDofusDbTableClient<DofusDbMapPosition> client = DofusDbClient.Beta().MapPositions();
        DofusDbMapPosition value = await client.GetAsync(3333);
        await Verify(value);
    }

    [Fact]
    public async Task MapPositionsClient_Should_SearchMapPositions()
    {
        IDofusDbTableClient<DofusDbMapPosition> client = DofusDbClient.Beta().MapPositions();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.Eq("subAreaId", "10")] };
        DofusDbMapPosition[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
