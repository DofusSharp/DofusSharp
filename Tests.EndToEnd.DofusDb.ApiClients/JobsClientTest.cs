using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class JobsClientTest
{
    [Fact]
    public async Task JobsClient_Should_GetJob()
    {
        IDofusDbTableClient<DofusDbJob> client = DofusDbClient.Beta(Constants.Referrer).Jobs();
        DofusDbJob value = await client.GetAsync(11);
        await Verify(value);
    }

    [Fact]
    public async Task JobsClient_Should_SearchJobs()
    {
        IDofusDbTableClient<DofusDbJob> client = DofusDbClient.Beta(Constants.Referrer).Jobs();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbJob[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
