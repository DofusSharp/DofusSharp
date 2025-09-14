using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Mounts;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class MountBehaviorsClientTest
{
    [Fact]
    public async Task MountBehaviorsClient_Should_GetMountBehavior()
    {
        IDofusDbTableClient<DofusDbMountBehavior> client = DofusDbClient.Beta(Constants.Referrer).MountBehaviors();
        DofusDbMountBehavior value = await client.GetAsync(5);
        await Verify(value);
    }

    [Fact]
    public async Task MountBehaviorsClient_Should_SearchMountBehaviors()
    {
        IDofusDbTableClient<DofusDbMountBehavior> client = DofusDbClient.Beta(Constants.Referrer).MountBehaviors();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbMountBehavior[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
