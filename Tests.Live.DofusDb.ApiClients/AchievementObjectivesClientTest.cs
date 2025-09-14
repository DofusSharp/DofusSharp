using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class AchievementObjectivesClientTest
{
    [Fact]
    public async Task AchievementObjectivesClient_Should_GetAchievementObjective()
    {
        IDofusDbTableClient<DofusDbAchievementObjective> client = DofusDbClient.Beta(Constants.Referrer).AchievementObjectives();
        DofusDbAchievementObjective value = await client.GetAsync(74);
        await Verify(value);
    }

    [Fact]
    public async Task AchievementObjectivesClient_Should_SearchAchievementObjectives()
    {
        IDofusDbTableClient<DofusDbAchievementObjective> client = DofusDbClient.Beta(Constants.Referrer).AchievementObjectives();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new() { Limit = 1000 };
        DofusDbAchievementObjective[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();

        results.Length.Should().Be(1000);
    }
}
