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
    readonly Mock<IDofusDbTableClient<DofusDbItem>> _clientMock;
    readonly DofusDbQuery<DofusDbItem> _builder;

    public DofusDbQueryTest()
    {
        _clientMock = new Mock<IDofusDbTableClient<DofusDbItem>>();
        _clientMock
            .Setup(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbItem>
                {
                    Total = 0, Limit = 0, Skip = 0, Data = []
                }
            );

        _builder = new DofusDbQuery<DofusDbItem>(_clientMock.Object);
    }

    [Fact]
    public async Task Execute_ShouldSetLimitParameter()
    {
        await _builder.Take(123).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Limit.Should().Be(123);
    }

    [Fact]
    public async Task Execute_ShouldSetSkipParameter()
    {
        await _builder.Skip(123).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Skip.Should().Be(123);
    }

    [Fact]
    public async Task Execute_ShouldSetSortParameter_Ascending()
    {
        await _builder.SortByAscending(i => i.AppearanceId).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Sort.Should().BeEquivalentTo(new Dictionary<string, DofusDbSearchQuerySortOrder> { { "appearanceId", DofusDbSearchQuerySortOrder.Ascending } });
    }

    [Fact]
    public async Task Execute_ShouldSetSortParameter_Descending()
    {
        await _builder.SortByDescending(i => i.AppearanceId).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Sort.Should().BeEquivalentTo(new Dictionary<string, DofusDbSearchQuerySortOrder> { { "appearanceId", DofusDbSearchQuerySortOrder.Descending } });
    }

    [Fact]
    public async Task Execute_ShouldSetSortParameter_NestedProperty()
    {
        await _builder.SortByDescending(i => i.Name.Fr).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Sort.Should().BeEquivalentTo(new Dictionary<string, DofusDbSearchQuerySortOrder> { { "name.fr", DofusDbSearchQuerySortOrder.Descending } });
    }

    [Fact]
    public async Task Execute_ShouldSetSortParameter_ForMultipleFields()
    {
        await _builder.SortByAscending(i => i.Name).SortByDescending(i => i.AppearanceId).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query
            .Sort.Should()
            .BeEquivalentTo(
                new Dictionary<string, DofusDbSearchQuerySortOrder> { { "name", DofusDbSearchQuerySortOrder.Ascending }, { "appearanceId", DofusDbSearchQuerySortOrder.Descending } }
            );
    }

    [Fact]
    public async Task Execute_ShouldSetSelectParameter()
    {
        await _builder.Select(i => i.AppearanceId).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Select.Should().BeEquivalentTo("appearanceId");
    }

    [Fact]
    public async Task Execute_ShouldSetSelectParameter_ForNestedFields()
    {
        await _builder.Select(i => i.Name.Fr).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Select.Should().BeEquivalentTo("name.fr");
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Eq()
    {
        await _builder.Where(i => i.AppearanceId == 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.Eq("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Neq()
    {
        await _builder.Where(i => i.AppearanceId != 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.NotEq("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_In_List()
    {
        List<long?> collection = [1, 2];
        await _builder.Where(i => collection.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.In("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_In_Array()
    {
        long?[] collection = [1, 2];
        await _builder.Where(i => collection.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.In("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_In_HashSet()
    {
        HashSet<long?> collection = [1, 2];
        await _builder.Where(i => collection.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.In("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_In_InlineEnumerable()
    {
        Dictionary<long, long?> collection = new() { { 1, 1 }, { 2, 2 } };
        await _builder.Where(i => collection.Values.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.In("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_In_Enumerable()
    {
        IEnumerable<long?> collection = [1, 2];
        await _builder.Where(i => collection.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.In("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_In_CollectionIsNotNullable()
    {
        IEnumerable<long> collection = [1, 2];
        await _builder.Where(i => collection.Contains(i.AppearanceId.Value)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.In("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Nin()
    {
        List<long?> collection = [1, 2];
        await _builder.Where(i => !collection.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.NotIn("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Gt()
    {
        await _builder.Where(i => i.AppearanceId > 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.GreaterThan("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Gte()
    {
        await _builder.Where(i => i.AppearanceId >= 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.GreaterThanOrEqual("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Lt()
    {
        await _builder.Where(i => i.AppearanceId < 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.LessThan("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Lte()
    {
        await _builder.Where(i => i.AppearanceId <= 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.LessThanOrEquals("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_And()
    {
        await _builder.Where(i => i.AppearanceId == 1 && i.AppearanceId != 2).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query
            .Predicates.Should()
            .BeEquivalentTo([new DofusDbSearchPredicate.And(new DofusDbSearchPredicate.Eq("appearanceId", "1"), new DofusDbSearchPredicate.NotEq("appearanceId", "2"))]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Or()
    {
        await _builder.Where(i => i.AppearanceId == 1 || i.AppearanceId != 2).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query
            .Predicates.Should()
            .BeEquivalentTo([new DofusDbSearchPredicate.Or(new DofusDbSearchPredicate.Eq("appearanceId", "1"), new DofusDbSearchPredicate.NotEq("appearanceId", "2"))]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Eq()
    {
        // ReSharper disable once NegativeEqualityExpression
        await _builder.Where(i => !(i.AppearanceId == 1)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.NotEq("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Neq()
    {
        // ReSharper disable once NegativeEqualityExpression
        await _builder.Where(i => !(i.AppearanceId != 1)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.Eq("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_In()
    {
        List<long?> collection = [1, 2];
        await _builder.Where(i => !collection.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.NotIn("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Nin()
    {
        List<long?> collection = [1, 2];
        // ReSharper disable once DoubleNegationOperator
        await _builder.Where(i => !!collection.Contains(i.AppearanceId)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.In("appearanceId", "1", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Gt()
    {
        await _builder.Where(i => !(i.AppearanceId > 1)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.LessThanOrEquals("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Gte()
    {
        await _builder.Where(i => i.AppearanceId >= 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.LessThan("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Lt()
    {
        await _builder.Where(i => i.AppearanceId < 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.GreaterThanOrEqual("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Lte()
    {
        await _builder.Where(i => i.AppearanceId <= 1).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.GreaterThan("appearanceId", "1")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_And()
    {
        await _builder.Where(i => !(i.AppearanceId == 1 && i.AppearanceId != 2)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query
            .Predicates.Should()
            .BeEquivalentTo([new DofusDbSearchPredicate.Or(new DofusDbSearchPredicate.NotEq("appearanceId", "1"), new DofusDbSearchPredicate.Eq("appearanceId", "2"))]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Not_Or()
    {
        await _builder.Where(i => !(i.AppearanceId == 1 || i.AppearanceId != 2)).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query
            .Predicates.Should()
            .BeEquivalentTo([new DofusDbSearchPredicate.And(new DofusDbSearchPredicate.NotEq("appearanceId", "1"), new DofusDbSearchPredicate.Eq("appearanceId", "2"))]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_MultiplePredicates()
    {
        await _builder.Where(i => i.AppearanceId == 1).Where(i => i.AppearanceId == 2).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.Eq("appearanceId", "1"), new DofusDbSearchPredicate.Eq("appearanceId", "2")]);
    }

    [Fact]
    public async Task ShouldSetPredicateParameter_DynamicValueInExpression()
    {
        int id = 2;
        await _builder.Where(i => i.Type.SuperType.Id == id).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.Eq("type.superType.id", "2")]);
    }

    [Fact]
    public async Task ShouldSetPredicateParameter_NestedDynamicValueInExpression()
    {
        var value = new { Content = new { Id = 2 } };
        await _builder.Where(i => i.Type.SuperType.Id == value.Content.Id).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.Eq("type.superType.id", "2")]);
    }

    [Fact]
    public async Task ShouldSetPredicateParameter_MemberChain()
    {
        await _builder.Where(i => i.Type.SuperType.Id == 2).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.Eq("type.superType.id", "2")]);
    }

    [Fact]
    public async Task ShouldSetPredicateParameter_MemberChain_ShouldIgnoreValuePropertyOfNullableField()
    {
        await _builder.Where(i => i.Type.SuperType.Id.Value == 2).ExecuteAsync().ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query.Predicates.Should().BeEquivalentTo([new DofusDbSearchPredicate.Eq("type.superType.id", "2")]);
    }

    [Fact]
    public async Task Execute_ShouldSetPredicateParameter_Complex()
    {
        List<long?> firstContainer = [1, 2];
        List<string?> secondContainer = ["value1", "value2"];
        List<string?> thirdContainer = ["value3", "value4"];

        await _builder
            .Where(i => firstContainer.Contains(i.AppearanceId) || i.BonusIsSecret == true && i.Level > 50 && !secondContainer.Contains(i.Name.Fr))
            .Where(i => thirdContainer.Contains(i.Criteria))
            .ExecuteAsync()
            .ToArrayAsync();

        _clientMock.Verify(c => c.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));

        DofusDbSearchQuery query = (DofusDbSearchQuery)_clientMock.Invocations.Single().Arguments[0];
        query
            .Predicates.Should()
            .BeEquivalentTo<DofusDbSearchPredicate>(
                [
                    new DofusDbSearchPredicate.Or(
                        new DofusDbSearchPredicate.In("appearanceId", "1", "2"),
                        new DofusDbSearchPredicate.And(
                            new DofusDbSearchPredicate.Eq("bonusIsSecret", "true"),
                            new DofusDbSearchPredicate.GreaterThan("level", "50"),
                            new DofusDbSearchPredicate.NotIn("name.fr", "value1", "value2")
                        )
                    ),
                    new DofusDbSearchPredicate.In("criteria", "value3", "value4")
                ],
                opt => opt.RespectingRuntimeTypes()
            );
    }

    [Fact]
    public async Task Count_ShouldReturnNumberOfResults()
    {
        _clientMock.Setup(c => c.CountAsync(It.IsAny<IReadOnlyCollection<DofusDbSearchPredicate>>(), It.IsAny<CancellationToken>())).ReturnsAsync(123);

        int result = await _builder.CountAsync();

        _clientMock.Verify(c => c.CountAsync(Array.Empty<DofusDbSearchPredicate>(), It.IsAny<CancellationToken>()));

        result.Should().Be(123);
    }

    [Fact]
    public async Task Count_ShouldReturnNumberOfResults_MinusSkippedOnes()
    {
        _clientMock.Setup(c => c.CountAsync(It.IsAny<IReadOnlyCollection<DofusDbSearchPredicate>>(), It.IsAny<CancellationToken>())).ReturnsAsync(123);

        int result = await _builder.Skip(10).CountAsync();

        _clientMock.Verify(c => c.CountAsync(Array.Empty<DofusDbSearchPredicate>(), It.IsAny<CancellationToken>()));

        result.Should().Be(113);
    }

    [Fact]
    public async Task Count_ShouldReturnLimit()
    {
        _clientMock.Setup(c => c.CountAsync(It.IsAny<IReadOnlyCollection<DofusDbSearchPredicate>>(), It.IsAny<CancellationToken>())).ReturnsAsync(123);

        int result = await _builder.Take(10).CountAsync();

        _clientMock.Verify(c => c.CountAsync(Array.Empty<DofusDbSearchPredicate>(), It.IsAny<CancellationToken>()));

        result.Should().Be(10);
    }

    [Fact]
    public async Task Count_ShouldReturnNumberOfResults_WhenLimitIsBiggerThanTotal()
    {
        _clientMock.Setup(c => c.CountAsync(It.IsAny<IReadOnlyCollection<DofusDbSearchPredicate>>(), It.IsAny<CancellationToken>())).ReturnsAsync(123);

        int result = await _builder.Take(456).CountAsync();

        _clientMock.Verify(c => c.CountAsync(Array.Empty<DofusDbSearchPredicate>(), It.IsAny<CancellationToken>()));

        result.Should().Be(123);
    }

    [Fact]
    public async Task Count_ShouldSetPredicateParameter_Complex()
    {
        List<long?> firstContainer = [1, 2];
        List<string?> secondContainer = ["value1", "value2"];
        List<string?> thirdContainer = ["value3", "value4"];

        await _builder
            .Where(i => firstContainer.Contains(i.AppearanceId) || i.BonusIsSecret == true && i.Level > 50 && !secondContainer.Contains(i.Name.Fr))
            .Where(i => thirdContainer.Contains(i.Criteria))
            .CountAsync();

        _clientMock.Verify(c => c.CountAsync(It.IsAny<IReadOnlyCollection<DofusDbSearchPredicate>>(), It.IsAny<CancellationToken>()));

        IReadOnlyCollection<DofusDbSearchPredicate> predicates = (IReadOnlyCollection<DofusDbSearchPredicate>)_clientMock.Invocations.Single().Arguments[0];
        predicates
            .Should()
            .BeEquivalentTo<DofusDbSearchPredicate>(
                [
                    new DofusDbSearchPredicate.Or(
                        new DofusDbSearchPredicate.In("appearanceId", "1", "2"),
                        new DofusDbSearchPredicate.And(
                            new DofusDbSearchPredicate.Eq("bonusIsSecret", "true"),
                            new DofusDbSearchPredicate.GreaterThan("level", "50"),
                            new DofusDbSearchPredicate.NotIn("name.fr", "value1", "value2")
                        )
                    ),
                    new DofusDbSearchPredicate.In("criteria", "value3", "value4")
                ],
                opt => opt.RespectingRuntimeTypes()
            );
    }
}
