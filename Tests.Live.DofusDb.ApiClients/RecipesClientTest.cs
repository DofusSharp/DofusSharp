using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class RecipesClientTest
{
    [Fact]
    public async Task RecipesClient_Should_GetRecipe()
    {
        IDofusDbTableClient<DofusDbRecipe> client = DofusDbClient.Beta(Constants.Referrer).Recipes();
        DofusDbRecipe value = await client.GetAsync(44);
        await Verify(value);
    }

    [Fact]
    public async Task RecipesClient_Should_SearchRecipes()
    {
        IDofusDbTableClient<DofusDbRecipe> client = DofusDbClient.Beta(Constants.Referrer).Recipes();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.GreaterThanOrEqual("level", "190")] };
        DofusDbRecipe[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
