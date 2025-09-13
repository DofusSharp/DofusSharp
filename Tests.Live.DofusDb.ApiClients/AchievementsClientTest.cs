using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class AchievementsClientTest
{
    [Fact]
    public async Task AchievementsClient_Should_GetAchievement()
    {
        IDofusDbTableClient<DofusDbAchievement> client = DofusDbClient.Beta(Constants.Referrer).Achievements();
        DofusDbAchievement value = await client.GetAsync(11);
        await Verify(value);
    }

    [Fact]
    public async Task AchievementsClient_Should_SearchAchievements()
    {
        IDofusDbTableClient<DofusDbAchievement> client = DofusDbClient.Beta(Constants.Referrer).Achievements();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        // TODO: entity 8575 has an issue that causes the server to throw an error 500, remove the predicate once it is fixed
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.NotEq("id", "8575")] };
        DofusDbAchievement[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
