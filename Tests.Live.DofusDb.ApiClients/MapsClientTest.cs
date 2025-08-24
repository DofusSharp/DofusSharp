using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class MapsClientTest
{
    [Fact]
    public async Task MapsClient_Should_GetMap()
    {
        IDofusDbTableClient<DofusDbMap> client = DofusDbClient.Beta().Maps();
        DofusDbMap value = await client.GetAsync(1);
        await Verify(value);
    }

    [Fact]
    public async Task MapsClient_Should_SearchMaps()
    {
        IDofusDbTableClient<DofusDbMap> client = DofusDbClient.Beta().Maps();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.Eq("subareaId", "10")] };
        DofusDbMap[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
