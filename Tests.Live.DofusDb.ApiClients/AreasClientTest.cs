using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class AreasClientTest
{
    [Fact]
    public async Task AreasClient_Should_GetArea()
    {
        IDofusDbTableClient<DofusDbArea> client = DofusDbClient.Beta().Areas();
        DofusDbArea value = await client.GetAsync(1);
        await Verify(value);
    }

    [Fact]
    public async Task AreasClient_Should_SearchAreas()
    {
        IDofusDbTableClient<DofusDbArea> client = DofusDbClient.Beta().Areas();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbArea[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
