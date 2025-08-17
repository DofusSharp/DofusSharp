using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class SuperAreasClientTest
{
    [Fact]
    public async Task SuperAreasClient_Should_GetSuperArea()
    {
        IDofusDbTableClient<SuperArea> client = DofusDbClient.Beta().SuperAreas();
        SuperArea value = await client.GetAsync(0);
        await Verify(value);
    }

    [Fact]
    public async Task SuperAreasClient_Should_SearchSuperAreas()
    {
        IDofusDbTableClient<SuperArea> client = DofusDbClient.Beta().SuperAreas();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        SearchQuery query = new();
        SuperArea[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
