using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class ServersClientTest
{
    [Fact]
    public async Task ServersClient_Should_GetServer()
    {
        IDofusDbTableClient<DofusDbServer> client = DofusDbClient.Beta().Servers();
        DofusDbServer value = await client.GetAsync(50);
        await Verify(value);
    }

    [Fact]
    public async Task ServersClient_Should_SearchServers()
    {
        IDofusDbTableClient<DofusDbServer> client = DofusDbClient.Beta().Servers();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbServer[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
