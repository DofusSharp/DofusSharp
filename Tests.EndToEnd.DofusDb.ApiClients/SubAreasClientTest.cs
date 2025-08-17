using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class SubAreasClientTest
{
    [Fact]
    public async Task SubAreasClient_Should_GetSubArea()
    {
        IDofusDbTableClient<SubArea> client = DofusDbClient.Beta().SubAreas();
        SubArea value = await client.GetAsync(1);
        await Verify(value);
    }

    [Fact]
    public async Task SubAreasClient_Should_SearchSubAreas()
    {
        IDofusDbTableClient<SubArea> client = DofusDbClient.Beta().SubAreas();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        SearchQuery query = new();
        SubArea[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
