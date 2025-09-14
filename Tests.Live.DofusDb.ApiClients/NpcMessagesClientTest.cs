using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Npcs;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class NpcMessagesClientTest
{
    [Fact]
    public async Task NpcMessagesClient_Should_GetNpcMessage()
    {
        IDofusDbTableClient<DofusDbNpcMessage> client = DofusDbClient.Beta(Constants.Referrer).NpcMessages();
        DofusDbNpcMessage value = await client.GetAsync(36);
        await Verify(value);
    }

    [Fact]
    public async Task NpcMessagesClient_Should_SearchNpcMessages()
    {
        IDofusDbTableClient<DofusDbNpcMessage> client = DofusDbClient.Beta(Constants.Referrer).NpcMessages();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new() { Limit = 1000 };
        DofusDbNpcMessage[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();

        results.Length.Should().Be(1000);
    }
}
