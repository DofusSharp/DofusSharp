using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Alterations;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class AlterationsClientTest
{
    [Fact]
    public async Task AlterationsClient_Should_GetAlteration()
    {
        IDofusDbTableClient<DofusDbAlteration> client = DofusDbClient.Beta(Constants.Referrer).Alterations();
        DofusDbAlteration value = await client.GetAsync(68);
        await Verify(value);
    }

    [Fact]
    public async Task AlterationsClient_Should_SearchAlterations()
    {
        IDofusDbTableClient<DofusDbAlteration> client = DofusDbClient.Beta(Constants.Referrer).Alterations();
        DofusDbSearchQuery query = new();
        DofusDbAlteration[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);
        results.Length.Should().Be(count);
    }
}
