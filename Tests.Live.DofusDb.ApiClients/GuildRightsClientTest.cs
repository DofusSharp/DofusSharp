using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Social;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class GuildRightsClientTest
{
    [Fact]
    public async Task GuildRightsClient_Should_GetGuildRight()
    {
        IDofusDbTableClient<DofusDbGuildRight> client = DofusDbClient.Beta(Constants.Referrer).GuildRights();
        DofusDbGuildRight value = await client.GetAsync(10);
        await Verify(value);
    }

    [Fact]
    public async Task GuildRightsClient_Should_SearchGuildRights()
    {
        IDofusDbTableClient<DofusDbGuildRight> client = DofusDbClient.Beta(Constants.Referrer).GuildRights();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbGuildRight[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
