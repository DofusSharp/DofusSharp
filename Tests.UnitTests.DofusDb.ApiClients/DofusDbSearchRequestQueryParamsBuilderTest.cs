using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;
using JetBrains.Annotations;

namespace Tests.UnitTests.DofusDb.ApiClients;

[TestSubject(typeof(DofusDbSearchRequestQueryParamsBuilder))]
public class DofusDbSearchRequestQueryParamsBuilderTest
{
    readonly DofusDbSearchRequestQueryParamsBuilder _builder = new();

    [Fact]
    public void ShouldSetLimitParameter()
    {
        DofusDbSearchQuery query = new() { Limit = 123 };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("$limit=123");
    }

    [Fact]
    public void ShouldSetSkipParameter()
    {
        DofusDbSearchQuery query = new() { Skip = 123 };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("$skip=123");
    }

    [Theory]
    [InlineData(DofusDbSearchQuerySortOrder.Ascending, 1)]
    [InlineData(DofusDbSearchQuerySortOrder.Descending, -1)]
    public void ShouldSetSortParameter(DofusDbSearchQuerySortOrder sortOrder, int expectedValue)
    {
        DofusDbSearchQuery query = new() { Sort = new Dictionary<string, DofusDbSearchQuerySortOrder> { { "parameter", sortOrder } } };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be($"$sort[parameter]={expectedValue}");
    }

    [Fact]
    public void ShouldSkipSortParameter_WhenOrderIsNone()
    {
        DofusDbSearchQuery query = new() { Sort = new Dictionary<string, DofusDbSearchQuerySortOrder> { { "parameter", DofusDbSearchQuerySortOrder.None } } };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().BeEmpty();
    }

    [Fact]
    public void ShouldSetSortParameter_ForMultipleFields()
    {
        DofusDbSearchQuery query = new()
        {
            Sort = new Dictionary<string, DofusDbSearchQuerySortOrder>
                { { "parameter1", DofusDbSearchQuerySortOrder.Ascending }, { "parameter2", DofusDbSearchQuerySortOrder.Descending } }
        };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("$sort[parameter1]=1&$sort[parameter2]=-1");
    }

    [Fact]
    public void ShouldSetSelectParameter()
    {
        DofusDbSearchQuery query = new() { Select = ["parameter"] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("$select[]=parameter");
    }

    [Fact]
    public void ShouldSetSelectParameter_ForMultipleFields()
    {
        DofusDbSearchQuery query = new() { Select = ["parameter1", "parameter2"] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("$select[]=parameter1&$select[]=parameter2");
    }

    [Fact]
    public void ShouldSetPredicateParameter_Eq()
    {
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.Eq("parameter", "value")] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("parameter=value");
    }

    [Fact]
    public void ShouldSetPredicateParameter_Neq()
    {
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.NotEq("parameter", "value")] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("parameter[$neq]=value");
    }

    [Fact]
    public void ShouldSetPredicateParameter_In()
    {
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.In("parameter", "value1", "value2")] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("parameter[$in][]=value1&parameter[$in][]=value2");
    }

    [Fact]
    public void ShouldSetPredicateParameter_Nin()
    {
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.NotIn("parameter", "value1", "value2")] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("parameter[$nin][]=value1&parameter[$nin][]=value2");
    }

    [Fact]
    public void ShouldSetPredicateParameter_Gt()
    {
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.GreaterThan("parameter", "value")] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("parameter[$gt]=value");
    }

    [Fact]
    public void ShouldSetPredicateParameter_Gte()
    {
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.GreaterThanOrEqual("parameter", "value")] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("parameter[$gte]=value");
    }

    [Fact]
    public void ShouldSetPredicateParameter_Lt()
    {
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.LessThan("parameter", "value")] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("parameter[$lt]=value");
    }

    [Fact]
    public void ShouldSetPredicateParameter_Lte()
    {
        DofusDbSearchQuery query = new() { Predicates = [new DofusDbSearchPredicate.LessThanOrEquals("parameter", "value")] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("parameter[$lte]=value");
    }

    [Fact]
    public void ShouldSetPredicateParameter_And()
    {
        DofusDbSearchQuery query = new()
            { Predicates = [new DofusDbSearchPredicate.And(new DofusDbSearchPredicate.Eq("parameter1", "value1"), new DofusDbSearchPredicate.NotEq("parameter2", "value2"))] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("$and[0][parameter1]=value1&$and[1][parameter2][$neq]=value2");
    }

    [Fact]
    public void ShouldSetPredicateParameter_Or()
    {
        DofusDbSearchQuery query = new()
            { Predicates = [new DofusDbSearchPredicate.Or(new DofusDbSearchPredicate.Eq("parameter1", "value1"), new DofusDbSearchPredicate.NotEq("parameter2", "value2"))] };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should().Be("$or[0][parameter1]=value1&$or[1][parameter2][$neq]=value2");
    }

    [Fact]
    public void ShouldSetPredicateParameter_Complex()
    {
        DofusDbSearchQuery query = new()
        {
            Predicates =
            [
                new DofusDbSearchPredicate.Or(
                    new DofusDbSearchPredicate.In("parameter1", "value11", "value12"),
                    new DofusDbSearchPredicate.And(
                        new DofusDbSearchPredicate.Eq("parameter2", "value2"),
                        new DofusDbSearchPredicate.GreaterThan("parameter3", "value3"),
                        new DofusDbSearchPredicate.NotIn("parameter4", "value41", "value42")
                    )
                ),
                new DofusDbSearchPredicate.In("parameter5", "value51", "value52")
            ]
        };
        string queryParams = _builder.BuildQueryParams(query);
        queryParams.Should()
            .Be(
                "$or[0][parameter1][$in][]=value11&"
                + "$or[0][parameter1][$in][]=value12&"
                + "$or[1][$and][0][parameter2]=value2&"
                + "$or[1][$and][1][parameter3][$gt]=value3&"
                + "$or[1][$and][2][parameter4][$nin][]=value41&"
                + "$or[1][$and][2][parameter4][$nin][]=value42&"
                + "parameter5[$in][]=value51&"
                + "parameter5[$in][]=value52"
            );
    }
}
