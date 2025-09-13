using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Mounts;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class MountsClientTest
{
    [Fact]
    public async Task MountsClient_Should_GetMount()
    {
        IDofusDbTableClient<DofusDbMount> client = DofusDbClient.Beta(Constants.Referrer).Mounts();
        DofusDbMount value = await client.GetAsync(11);
        await Verify(value);
    }

    [Fact]
    public async Task MountsClient_Should_SearchMounts()
    {
        IDofusDbTableClient<DofusDbMount> client = DofusDbClient.Beta(Constants.Referrer).Mounts();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbMount[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
