using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;
using JetBrains.Annotations;
using Moq;

namespace Tests.UnitTests.DofusDb.ApiClients;

[TestSubject(typeof(DofusDbQuery<>))]
public class DofusDbQueryTest
{
    readonly Mock<IDofusDbTableClient<Item>> _clientMock;
    readonly DofusDbQuery<Item> _builder;

    public DofusDbQueryTest()
    {
        _clientMock = new Mock<IDofusDbTableClient<Item>>();
        _clientMock.Setup(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new SearchResult<Item>
                {
                    Total = 0, Limit = 0, Skip = 0, Data = []
                }
            );

        _builder = new DofusDbQuery<Item>(_clientMock.Object);
    }

    [Fact]
    public async Task Execute_ShouldSetLimitParameter()
    {
        await _builder.Take(123).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Limit.Should().Be(123);
    }

    [Fact]
    public async Task Execute_ShouldSetSkipParameter()
    {
        await _builder.Skip(123).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Skip.Should().Be(123);
    }

    [Fact]
    public async Task Execute_ShouldSetSortParameter_Ascending()
    {
        await _builder.SortByAscending(i => i.AppearanceId).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Sort.Should().BeEquivalentTo(new Dictionary<string, SearchQuerySortOrder> { { "appearanceId", SearchQuerySortOrder.Ascending } });
    }

    [Fact]
    public async Task Execute_ShouldSetSortParameter_Descending()
    {
        await _builder.SortByDescending(i => i.AppearanceId).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Sort.Should().BeEquivalentTo(new Dictionary<string, SearchQuerySortOrder> { { "appearanceId", SearchQuerySortOrder.Descending } });
    }

    [Fact]
    public async Task Execute_ShouldSetSortParameter_NestedProperty()
    {
        await _builder.SortByDescending(i => i.Name.Fr).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Sort.Should().BeEquivalentTo(new Dictionary<string, SearchQuerySortOrder> { { "name.fr", SearchQuerySortOrder.Descending } });
    }

    [Fact]
    public async Task Execute_ShouldSetSortParameter_ForMultipleFields()
    {
        await _builder.SortByAscending(i => i.Name).SortByDescending(i => i.AppearanceId).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Sort.Should()
            .BeEquivalentTo(new Dictionary<string, SearchQuerySortOrder> { { "name", SearchQuerySortOrder.Ascending }, { "appearanceId", SearchQuerySortOrder.Descending } });
    }

    [Fact]
    public async Task Execute_ShouldSetSelectParameter()
    {
        await _builder.Select(i => i.AppearanceId).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Select.Should().BeEquivalentTo("appearanceId");
    }

    [Fact]
    public async Task Execute_ShouldSetSelectParameter_ForNestedFields()
    {
        await _builder.Select(i => i.Name.Fr).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Select.Should().BeEquivalentTo("name.fr");
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Eq()
    {
        await _builder.Where(i => i.AppearanceId == 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.Eq("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Neq()
    {
        await _builder.Where(i => i.AppearanceId != 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.NotEq("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_In()
    {
        List<long?> collection = [1, 2];
        await _builder.Where(i => collection.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.In("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Nin()
    {
        List<long?> collection = [1, 2];
        await _builder.Where(i => !collection.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.NotIn("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Gt()
    {
        await _builder.Where(i => i.AppearanceId > 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.GreaterThan("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Gte()
    {
        await _builder.Where(i => i.AppearanceId >= 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.GreaterThanOrEqual("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Lt()
    {
        await _builder.Where(i => i.AppearanceId < 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.LessThan("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Lte()
    {
        await _builder.Where(i => i.AppearanceId <= 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.LessThanOrEquals("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_And()
    {
        await _builder.Where(i => i.AppearanceId == 1 && i.AppearanceId != 2).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.And(new SearchPredicate.Eq("appearanceId", "1"), new SearchPredicate.NotEq("appearanceId", "2"))]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Or()
    {
        await _builder.Where(i => i.AppearanceId == 1 || i.AppearanceId != 2).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.Or(new SearchPredicate.Eq("appearanceId", "1"), new SearchPredicate.NotEq("appearanceId", "2"))]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Eq()
    {
        // ReSharper disable once NegativeEqualityExpression
        await _builder.Where(i => !(i.AppearanceId == 1)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.NotEq("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Neq()
    {
        // ReSharper disable once NegativeEqualityExpression
        await _builder.Where(i => !(i.AppearanceId != 1)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.Eq("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_In()
    {
        List<long?> collection = [1, 2];
        await _builder.Where(i => !collection.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.NotIn("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Nin()
    {
        List<long?> collection = [1, 2];
        // ReSharper disable once DoubleNegationOperator
        await _builder.Where(i => !!collection.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.In("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Gt()
    {
        await _builder.Where(i => !(i.AppearanceId > 1)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.LessThanOrEquals("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Gte()
    {
        await _builder.Where(i => i.AppearanceId >= 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.LessThan("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Lt()
    {
        await _builder.Where(i => i.AppearanceId < 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.GreaterThanOrEqual("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Lte()
    {
        await _builder.Where(i => i.AppearanceId <= 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.GreaterThan("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_And()
    {
        await _builder.Where(i => !(i.AppearanceId == 1 && i.AppearanceId != 2)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.Or(new SearchPredicate.NotEq("appearanceId", "1"), new SearchPredicate.Eq("appearanceId", "2"))]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Or()
    {
        await _builder.Where(i => !(i.AppearanceId == 1 || i.AppearanceId != 2)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.And(new SearchPredicate.NotEq("appearanceId", "1"), new SearchPredicate.Eq("appearanceId", "2"))]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_MultiplePredicates()
    {
        await _builder.Where(i => i.AppearanceId == 1).Where(i => i.AppearanceId == 2).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new SearchPredicate.Eq("appearanceId", "1"), new SearchPredicate.Eq("appearanceId", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Complex()
    {
        List<long?> firstContainer = [1, 2];
        List<string?> secondContainer = ["value1", "value2"];
        List<string?> thirdContainer = ["value3", "value4"];

        await _builder.Where(i => firstContainer.Contains(i.AppearanceId) || i.BonusIsSecret == true && i.Level > 50 && !secondContainer.Contains(i.Name.Fr))
            .Where(i => thirdContainer.Contains(i.Criteria))
            .ExecuteAsync()
            .ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<SearchQuery>(), It.IsAny<CancellationToken>()));

        SearchQuery query = (SearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should()
            .BeEquivalentTo<SearchPredicate>(
                [
                    new SearchPredicate.Or(
                        new SearchPredicate.In("appearanceId", "1", "2"),
                        new SearchPredicate.And(
                            new SearchPredicate.Eq("bonusIsSecret", "true"),
                            new SearchPredicate.GreaterThan("level", "50"),
                            new SearchPredicate.NotIn("name.fr", "value1", "value2")
                        )
                    ),
                    new SearchPredicate.In("criteria", "value3", "value4")
                ],
                opt => opt.RespectingRuntimeTypes()
            );
    }

    [Fact]
    public async Task Count_ShouldReturnNumberOfResults()
    {
        _clientMock.Setup(c => c.CountAsync(It.IsAny<IReadOnlyCollection<SearchPredicate>>(), It.IsAny<CancellationToken>())).ReturnsAsync(123);

        int result = await _builder.CountAsync();

        _clientMock.Verify(c => c.CountAsync(Array.Empty<SearchPredicate>(), It.IsAny<CancellationToken>()));

        result.Should().Be(123);
    }

    [Fact]
    public async Task Count_ShouldReturnNumberOfResults_MinusSkippedOnes()
    {
        _clientMock.Setup(c => c.CountAsync(It.IsAny<IReadOnlyCollection<SearchPredicate>>(), It.IsAny<CancellationToken>())).ReturnsAsync(123);

        int result = await _builder.Skip(10).CountAsync();

        _clientMock.Verify(c => c.CountAsync(Array.Empty<SearchPredicate>(), It.IsAny<CancellationToken>()));

        result.Should().Be(113);
    }

    [Fact]
    public async Task Count_ShouldReturnLimit()
    {
        _clientMock.Setup(c => c.CountAsync(It.IsAny<IReadOnlyCollection<SearchPredicate>>(), It.IsAny<CancellationToken>())).ReturnsAsync(123);

        int result = await _builder.Take(10).CountAsync();

        _clientMock.Verify(c => c.CountAsync(Array.Empty<SearchPredicate>(), It.IsAny<CancellationToken>()));

        result.Should().Be(10);
    }

    [Fact]
    public async Task Count_ShouldReturnNumberOfResults_WhenLimitIsBiggerThanTotal()
    {
        _clientMock.Setup(c => c.CountAsync(It.IsAny<IReadOnlyCollection<SearchPredicate>>(), It.IsAny<CancellationToken>())).ReturnsAsync(123);

        int result = await _builder.Take(456).CountAsync();

        _clientMock.Verify(c => c.CountAsync(Array.Empty<SearchPredicate>(), It.IsAny<CancellationToken>()));

        result.Should().Be(123);
    }

    [Fact]
    public async Task Count_ShouldSetPredicateParameter_Complex()
    {
        List<long?> firstContainer = [1, 2];
        List<string?> secondContainer = ["value1", "value2"];
        List<string?> thirdContainer = ["value3", "value4"];

        await _builder.Where(i => firstContainer.Contains(i.AppearanceId) || i.BonusIsSecret == true && i.Level > 50 && !secondContainer.Contains(i.Name.Fr))
            .Where(i => thirdContainer.Contains(i.Criteria))
            .CountAsync();

        _clientMock.Verify(c => c.CountAsync(It.IsAny<IReadOnlyCollection<SearchPredicate>>(), It.IsAny<CancellationToken>()));

        IReadOnlyCollection<SearchPredicate> predicates = (IReadOnlyCollection<SearchPredicate>)_clientMock.Invocations.Single().Arguments[0];
        predicates.Should()
            .BeEquivalentTo<SearchPredicate>(
                [
                    new SearchPredicate.Or(
                        new SearchPredicate.In("appearanceId", "1", "2"),
                        new SearchPredicate.And(
                            new SearchPredicate.Eq("bonusIsSecret", "true"),
                            new SearchPredicate.GreaterThan("level", "50"),
                            new SearchPredicate.NotIn("name.fr", "value1", "value2")
                        )
                    ),
                    new SearchPredicate.In("criteria", "value3", "value4")
                ],
                opt => opt.RespectingRuntimeTypes()
            );
    }
}
