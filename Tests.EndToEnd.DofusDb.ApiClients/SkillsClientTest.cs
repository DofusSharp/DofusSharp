using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class SkillsClientTest
{
    [Fact]
    public async Task SkillsClient_Should_GetSkill()
    {
        IDofusDbTableClient<Skill> client = DofusDbClient.Beta(Constants.Referrer).Skills();
        Skill value = await client.GetAsync(44);
        await Verify(value);
    }

    [Fact]
    public async Task SkillsClient_Should_SearchSkills()
    {
        IDofusDbTableClient<Skill> client = DofusDbClient.Beta(Constants.Referrer).Skills();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        SearchQuery query = new() { Predicates = [new SearchPredicate.GreaterThanOrEqual("level", "190")] };
        Skill[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
