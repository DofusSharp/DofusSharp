using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;

namespace DofusSharp.DofusDb.ApiClients;

public class DofusDbQuery<TResource>(IDofusDbTableClient<TResource> client) where TResource: DofusDbResource
{
    int? _limit;
    int? _skip;
    readonly Dictionary<string, DofusDbSearchQuerySortOrder> _sort = [];
    readonly List<string> _select = [];
    readonly List<DofusDbSearchPredicate> _predicates = [];

    /// <summary>
    ///     Sets the maximum number of items to return in the result set.
    /// </summary>
    /// <param name="count">The maximum number of items to return.</param>
    /// <returns>The current builder, for chaining.</returns>
    public DofusDbQuery<TResource> Take(int count)
    {
        _limit = count;
        return this;
    }

    /// <summary>
    ///     Sets the number of items to skip before starting to collect the result set.
    /// </summary>
    /// <param name="count">The number of items to skip.</param>
    /// <returns>The current builder, for chaining.</returns>
    public DofusDbQuery<TResource> Skip(int count)
    {
        _skip = count;
        return this;
    }

    /// <summary>
    ///     Sort the data by the specified property in ascending order.
    /// </summary>
    /// <param name="expression">The expression representing the property to sort by.</param>
    /// <returns>The current builder, for chaining.</returns>
    public DofusDbQuery<TResource> SortByAscending(Expression<Func<TResource, object?>> expression)
    {
        string propertyName = ExtractPropertyName(expression);
        _sort[propertyName] = DofusDbSearchQuerySortOrder.Ascending;
        return this;
    }

    /// <summary>
    ///     Sort the data by the specified property in descending order.
    /// </summary>
    /// <param name="expression">The expression representing the property to sort by.</param>
    /// <returns>The current builder, for chaining.</returns>
    public DofusDbQuery<TResource> SortByDescending(Expression<Func<TResource, object?>> expression)
    {
        string propertyName = ExtractPropertyName(expression);
        _sort[propertyName] = DofusDbSearchQuerySortOrder.Descending;
        return this;
    }

    /// <summary>
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public DofusDbQuery<TResource> Select(Expression<Func<TResource, object?>> expression)
    {
        string propertyName = ExtractPropertyName(expression);
        _select.Add(propertyName);
        return this;
    }

    /// <summary>
    ///     Adds a predicate to the search query, allowing for complex filtering of results.
    /// </summary>
    /// <param name="expression">The expression representing the predicate to apply.</param>
    /// <returns>The current builder, for chaining.</returns>
    public DofusDbQuery<TResource> Where(Expression<Func<TResource, bool>> expression)
    {
        DofusDbSearchPredicate predicate = ExtractPredicate(expression);
        _predicates.Add(predicate);
        return this;
    }

    /// <summary>
    ///     Executes the search query and returns an asynchronous enumerable of resources matching the query.
    ///     This method will perform as many requests as necessary to retrieve the requested number of results.
    /// </summary>
    /// <returns>The search result containing all resources matching the query.</returns>
    public IAsyncEnumerable<TResource> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        DofusDbSearchQuery query = BuildQuery();
        return client.MultiQuerySearchAsync(query, cancellationToken);
    }

    /// <summary>
    ///     Count the number of results that the `ExecuteAsync` method would return.
    ///     This method only performs one request and does not retrieve the actual resources.
    /// </summary>
    /// <returns>The search result containing all resources matching the query.</returns>
    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        int totalCount = await client.CountAsync(_predicates, cancellationToken);
        int countMinusSkipped = totalCount - (_skip ?? 0);
        return Math.Clamp(countMinusSkipped, 0, _limit ?? int.MaxValue);
    }

    DofusDbSearchQuery BuildQuery() =>
        new()
        {
            Limit = _limit,
            Skip = _skip,
            Sort = _sort,
            Select = _select,
            Predicates = _predicates
        };

    static string ExtractPropertyName(Expression<Func<TResource, object?>> expression) => ExtractPropertyChain(expression.Parameters.Single(), expression.Body);

    static string ExtractPropertyChain(ParameterExpression root, Expression expression)
    {
        string[] chain = ExtractPropertyChainRecursive(root, expression, []);
        return string.Join('.', chain.Select(p => p.ToCamelCase()));

        static string[] ExtractPropertyChainRecursive(ParameterExpression root, Expression expression, string[] path)
        {
            if (expression == root)
            {
                return path;
            }

            switch (expression)
            {
                case UnaryExpression { NodeType: ExpressionType.Convert } unaryExpression:
                    return ExtractPropertyChainRecursive(root, unaryExpression.Operand, path);
                case MemberExpression memberExpression:
                    string[] newPath = [memberExpression.Member.Name, ..path];
                    return memberExpression.Expression == null ? newPath : ExtractPropertyChainRecursive(root, memberExpression.Expression, newPath);
            }

            throw new ArgumentException("Expression must be a property chain.", nameof(expression));
        }
    }

    static DofusDbSearchPredicate ExtractPredicate(Expression<Func<TResource, bool>> expression) => ExtractPredicate(expression.Parameters.Single(), expression.Body);

    static DofusDbSearchPredicate ExtractPredicate(ParameterExpression root, Expression expression)
    {
        switch (expression)
        {
            case BinaryExpression { NodeType: ExpressionType.Equal } e:
            {
                string left = ExtractPropertyChain(root, e.Left);
                string right = ExtractValueAsString(e.Right);
                return new DofusDbSearchPredicate.Eq(left, right);
            }
            case BinaryExpression { NodeType: ExpressionType.NotEqual } e:
            {
                string left = ExtractPropertyChain(root, e.Left);
                string right = ExtractValueAsString(e.Right);
                return new DofusDbSearchPredicate.NotEq(left, right);
            }
            case MethodCallExpression { Method.Name: "Contains" } e:
            {
                if (e.Object is null)
                {
                    throw new ArgumentException("The 'Contains' method must be called on a collection.", nameof(expression));
                }

                string left = ExtractPropertyChain(root, e.Arguments.Single());
                string[] right = ExtractCollectionValuesAsString(e.Object);
                return new DofusDbSearchPredicate.In(left, right);
            }
            case UnaryExpression { NodeType: ExpressionType.Not } e:
            {
                DofusDbSearchPredicate predicate = ExtractPredicate(root, e.Operand);
                return NegatePredicate(predicate);
            }
            case BinaryExpression { NodeType: ExpressionType.GreaterThan } e:
            {
                string left = ExtractPropertyChain(root, e.Left);
                string right = ExtractValueAsString(e.Right);
                return new DofusDbSearchPredicate.GreaterThan(left, right);
            }
            case BinaryExpression { NodeType: ExpressionType.GreaterThanOrEqual } e:
            {
                string left = ExtractPropertyChain(root, e.Left);
                string right = ExtractValueAsString(e.Right);
                return new DofusDbSearchPredicate.GreaterThanOrEqual(left, right);
            }
            case BinaryExpression { NodeType: ExpressionType.LessThan } e:
            {
                string left = ExtractPropertyChain(root, e.Left);
                string right = ExtractValueAsString(e.Right);
                return new DofusDbSearchPredicate.LessThan(left, right);
            }
            case BinaryExpression { NodeType: ExpressionType.LessThanOrEqual } e:
            {
                string left = ExtractPropertyChain(root, e.Left);
                string right = ExtractValueAsString(e.Right);
                return new DofusDbSearchPredicate.LessThanOrEquals(left, right);
            }
            case BinaryExpression { NodeType: ExpressionType.AndAlso } e:
            {
                DofusDbSearchPredicate left = ExtractPredicate(root, e.Left);
                IReadOnlyList<DofusDbSearchPredicate> flattenedLeft = left is DofusDbSearchPredicate.And andLeft ? andLeft.Predicates : [left];

                DofusDbSearchPredicate right = ExtractPredicate(root, e.Right);
                IReadOnlyList<DofusDbSearchPredicate> flattenedRight = right is DofusDbSearchPredicate.And andRight ? andRight.Predicates : [right];

                return new DofusDbSearchPredicate.And([..flattenedLeft, ..flattenedRight]);
            }
            case BinaryExpression { NodeType: ExpressionType.OrElse } e:
            {
                DofusDbSearchPredicate left = ExtractPredicate(root, e.Left);
                IReadOnlyList<DofusDbSearchPredicate> flattenedLeft = left is DofusDbSearchPredicate.Or orLeft ? orLeft.Predicates : [left];

                DofusDbSearchPredicate right = ExtractPredicate(root, e.Right);
                IReadOnlyList<DofusDbSearchPredicate> flattenedRight = left is DofusDbSearchPredicate.Or orRight ? orRight.Predicates : [right];

                return new DofusDbSearchPredicate.Or([..flattenedLeft, ..flattenedRight]);
            }
        }

        throw new ArgumentException($"Could not extract predicate from expression {expression}.", nameof(expression));
    }

    static DofusDbSearchPredicate NegatePredicate(DofusDbSearchPredicate predicate) =>
        predicate switch
        {
            DofusDbSearchPredicate.Eq p => new DofusDbSearchPredicate.NotEq(p.Field, p.Value),
            DofusDbSearchPredicate.NotEq p => new DofusDbSearchPredicate.Eq(p.Field, p.Value),
            DofusDbSearchPredicate.In p => new DofusDbSearchPredicate.NotIn(p.Field, p.Value),
            DofusDbSearchPredicate.NotIn p => new DofusDbSearchPredicate.In(p.Field, p.Value),
            DofusDbSearchPredicate.GreaterThan p => new DofusDbSearchPredicate.LessThanOrEquals(p.Field, p.Value),
            DofusDbSearchPredicate.GreaterThanOrEqual p => new DofusDbSearchPredicate.LessThan(p.Field, p.Value),
            DofusDbSearchPredicate.LessThan p => new DofusDbSearchPredicate.GreaterThanOrEqual(p.Field, p.Value),
            DofusDbSearchPredicate.LessThanOrEquals p => new DofusDbSearchPredicate.GreaterThan(p.Field, p.Value),
            DofusDbSearchPredicate.And p => new DofusDbSearchPredicate.Or(p.Predicates.Select(NegatePredicate).ToArray()),
            DofusDbSearchPredicate.Or p => new DofusDbSearchPredicate.And(p.Predicates.Select(NegatePredicate).ToArray()),
            _ => throw new ArgumentOutOfRangeException(nameof(predicate))
        };

    static string ExtractValueAsString(Expression expression)
    {
        object? value = ExtractValue(expression);
        return value switch
        {
            bool b => b ? "true" : "false",
            _ => value?.ToString() ?? "null"
        };
    }

    static object? ExtractValue(Expression expression) =>
        expression switch
        {
            ConstantExpression constantExpression => constantExpression.Value,
            UnaryExpression { NodeType: ExpressionType.Convert } unaryExpression => ExtractValue(unaryExpression.Operand),
            _ => throw new ArgumentException($"Could not evaluate expression {expression}.", nameof(expression))
        };

    static string[] ExtractCollectionValuesAsString(Expression expression)
    {
        IEnumerable values = ExtractCollectionValues(expression);
        return values switch
        {
            IEnumerable<bool> => values.Cast<bool>().Select(v => v ? "true" : "false").ToArray(),
            _ => values.Cast<object?>().Select(o => o?.ToString() ?? "null").ToArray()
        };
    }

    static IEnumerable ExtractCollectionValues(Expression expression)
    {
        switch (expression)
        {
            case MemberExpression memberExpression:
                if (memberExpression.Expression is ConstantExpression constantExpression)
                {
                    if (memberExpression.Member is PropertyInfo property && property.GetValue(constantExpression.Value) is IEnumerable propertyValue)
                    {
                        return propertyValue;
                    }

                    if (memberExpression.Member is FieldInfo field && field.GetValue(constantExpression.Value) is IEnumerable fieldValue)
                    {
                        return fieldValue;
                    }
                }
                break;
        }

        throw new ArgumentException($"Could not evaluate collection {expression}.", nameof(expression));
    }
}
