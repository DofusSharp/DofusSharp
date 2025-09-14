using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class AchievementCategoriesClientTest
{
    [Fact]
    public async Task AchievementCategoriesClient_Should_GetAchievementCategorie()
    {
        IDofusDbTableClient<DofusDbAchievementCategory> client = DofusDbClient.Beta(Constants.Referrer).AchievementCategories();
        DofusDbAchievementCategory value = await client.GetAsync(11);
        await Verify(value);
    }

    [Fact]
    public async Task AchievementCategoriesClient_Should_SearchAchievementCategories()
    {
        IDofusDbTableClient<DofusDbAchievementCategory> client = DofusDbClient.Beta(Constants.Referrer).AchievementCategories();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbAchievementCategory[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
