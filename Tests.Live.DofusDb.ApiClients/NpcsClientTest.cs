using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Npcs;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class NpcsClientTest
{
    [Fact]
    public async Task NpcsClient_Should_GetNpc()
    {
        IDofusDbTableClient<DofusDbNpc> client = DofusDbClient.Beta(Constants.Referrer).Npcs();
        DofusDbNpc value = await client.GetAsync(37);
        await Verify(value);
    }

    [Fact]
    public async Task NpcsClient_Should_SearchNpcs()
    {
        IDofusDbTableClient<DofusDbNpc> client = DofusDbClient.Beta(Constants.Referrer).Npcs();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbNpc[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
