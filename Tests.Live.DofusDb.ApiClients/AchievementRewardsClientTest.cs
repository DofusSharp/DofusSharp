using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class AchievementRewardsClientTest
{
    [Fact]
    public async Task AchievementRewardsClient_Should_GetAchievementReward()
    {
        IDofusDbTableClient<DofusDbAchievementReward> client = DofusDbClient.Beta(Constants.Referrer).AchievementRewards();
        DofusDbAchievementReward value = await client.GetAsync(12);
        await Verify(value);
    }

    [Fact]
    public async Task AchievementRewardsClient_Should_SearchAchievementRewards()
    {
        IDofusDbTableClient<DofusDbAchievementReward> client = DofusDbClient.Beta(Constants.Referrer).AchievementRewards();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.GreaterThan("experienceRatio", "0")] };
        DofusDbAchievementReward[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
