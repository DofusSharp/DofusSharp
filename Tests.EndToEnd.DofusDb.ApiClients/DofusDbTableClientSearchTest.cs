using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;
using JetBrains.Annotations;

namespace Tests.EndToEnd.DofusDb.ApiClients;

[TestSubject(typeof(DofusDbClient))]
public class DofusDbTableClientSearchTest
{
    [Fact]
    public async Task ShouldCountWithPredicate()
    {
        IDofusDbTableClient<ItemSet> client = DofusDbClient.Beta(Constants.Referrer).ItemSets();
        int level20SetsCount = await client.CountAsync(new SearchPredicate.Eq("level", "20"));
        List<ItemSet> allSets = await client.MultiQuerySearchAsync().ToListAsync();
        level20SetsCount.Should().Be(allSets.Count(s => s.Level == 20));
    }

    [Fact]
    public async Task ShouldLimitSearchResults()
    {
        IDofusDbTableClient<Item> client = DofusDbClient.Beta(Constants.Referrer).Items();
        SearchResult<Item> items = await client.SearchAsync(new SearchQuery { Limit = 12 });
        items.Data.Count.Should().Be(12);
    }

    [Fact]
    public async Task ShouldSkipSearchResults()
    {
        IDofusDbTableClient<Item> client = DofusDbClient.Beta(Constants.Referrer).Items();
        SearchResult<Item> firstAndSecondItems = await client.SearchAsync(new SearchQuery { Limit = 2 });

        SearchResult<Item> secondItem = await client.SearchAsync(new SearchQuery { Limit = 1, Skip = 1 });

        secondItem.Data.Should().BeEquivalentTo([firstAndSecondItems.Data[1]]);
    }

    [Fact]
    public async Task ShouldSortSearchResults()
    {
        IDofusDbTableClient<Item> client = DofusDbClient.Beta(Constants.Referrer).Items();

        SearchResult<Item> sortedSearchResults = await client.SearchAsync(
            new SearchQuery { Limit = 50, Sort = new Dictionary<string, SearchQuerySortOrder> { { nameof(Item.RealWeight), SearchQuerySortOrder.Descending } } }
        );

        // expect the results to be sorted by name.fr in descending order
        IOrderedEnumerable<Item> sortedData = sortedSearchResults.Data.OrderByDescending(d => d.RealWeight);
        sortedSearchResults.Data.Should().BeEquivalentTo(sortedData, opt => opt.WithStrictOrdering());
    }

    [Fact]
    public async Task ShouldSelectSearchResults()
    {
        IDofusDbTableClient<Item> client = DofusDbClient.Beta(Constants.Referrer).Items();

        SearchResult<Item> results = await client.SearchAsync(new SearchQuery { Limit = 1, Select = [nameof(Item.RealWeight)] });

        results.Data[0]
            .Should()
            .BeEquivalentTo(
                new Item
                {
                    // the selected field is not null
                    RealWeight = 1,
                    // Img is not null for whatever reason, looks like a bug in the API
                    Img = "https://api.beta.dofusdb.fr/img/items/undefined.png",
                    // all the other fields are null
                    Id = null,
                    TypeId = null,
                    Type = null,
                    Name = null,
                    Slug = null,
                    Description = null,
                    ImportantNotice = null,
                    Level = null,
                    Price = null,
                    Criteria = null,
                    CreatedAt = null,
                    UpdatedAt = null,
                    ItemSetId = null,
                    ItemSet = null,
                    HideEffects = null,
                    Effects = null,
                    EvolutiveEffectIds = null,
                    FavoriteSubAreas = null,
                    FavoriteSubAreasBonus = null,
                    IconId = null,
                    AppearanceId = null,
                    IsColorable = null,
                    HasLivingObjectSkinJntMood = null,
                    HasRecipe = null,
                    RecipeSlots = null,
                    RecipeIds = null,
                    RecipesThatUse = null,
                    CraftXpRatio = null,
                    CraftVisible = null,
                    CraftConditional = null,
                    CraftFeasible = null,
                    SecretRecipe = null,
                    BonusIsSecret = null,
                    Usable = null,
                    Targetable = null,
                    CriteriaTarget = null,
                    NonUsableOnAnother = null,
                    NeedUseConfirm = null,
                    UseAnimationId = null,
                    ResourcesBySubarea = null,
                    DropMonsterIds = null,
                    DropTemporisMonsterIds = null,
                    DropSubAreaIds = null,
                    RecyclingNuggets = null,
                    FavoriteRecyclingSubareas = null,
                    QuestsThatUse = null,
                    QuestsThatReward = null,
                    StartLegendaryTreasureHunt = null,
                    LegendaryTreasureHuntThatReward = null,
                    Flags = null,
                    Cursed = null,
                    Exchangeable = null,
                    Etheral = null,
                    Enhanceable = null,
                    ObjectIsDisplayOnWeb = null,
                    IsDestructible = null,
                    IsSaleable = null,
                    IsLegendary = null,
                    Visibility = null,
                    ChangeVersion = null,
                    TooltipExpirationDate = null
                }
            );
    }

    [Fact]
    public async Task ShouldFilterSearchResults()
    {
        IDofusDbTableClient<Item> client = DofusDbClient.Beta(Constants.Referrer).Items();

        SearchResult<Item> sortedSearchResults = await client.SearchAsync(
            new SearchQuery
            {
                Predicates = [new SearchPredicate.GreaterThan("level", "27"), new SearchPredicate.LessThan("level", "29")]
            }
        );

        // expect the results to be sorted by name.fr in descending order
        sortedSearchResults.Data.Should().AllSatisfy(d => d.Level.Should().Be(28));
    }
}
