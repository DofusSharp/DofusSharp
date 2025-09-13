using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Titles;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class TitlesClientTest
{
    [Fact]
    public async Task TitlesClient_Should_GetTitle()
    {
        IDofusDbTableClient<DofusDbTitle> client = DofusDbClient.Beta(Constants.Referrer).Titles();
        DofusDbTitle value = await client.GetAsync(11);
        await Verify(value);
    }

    [Fact]
    public async Task TitlesClient_Should_SearchTitles()
    {
        IDofusDbTableClient<DofusDbTitle> client = DofusDbClient.Beta(Constants.Referrer).Titles();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbTitle[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
