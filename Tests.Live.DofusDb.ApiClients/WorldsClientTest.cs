using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class WorldsClientTest
{
    [Fact]
    public async Task WorldsClient_Should_GetWorld()
    {
        IDofusDbTableClient<DofusDbWorld> client = DofusDbClient.Beta().Worlds();
        DofusDbWorld value = await client.GetAsync(1);
        await Verify(value);
    }

    [Fact]
    public async Task WorldsClient_Should_SearchWorlds()
    {
        IDofusDbTableClient<DofusDbWorld> client = DofusDbClient.Beta().Worlds();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbWorld[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
