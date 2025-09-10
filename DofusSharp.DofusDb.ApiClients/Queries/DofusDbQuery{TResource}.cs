using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;

namespace DofusSharp.DofusDb.ApiClients.Queries;

class DofusDbQuery<TResource>(IDofusDbTableClient<TResource> client) : IDofusDbQuery<TResource> where TResource: DofusDbResource
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
    public IDofusDbQuery<TResource> Take(int count)
    {
        _limit = count;
        return this;
    }

    /// <summary>
    ///     Sets the number of items to skip before starting to collect the result set.
    /// </summary>
    /// <param name="count">The number of items to skip.</param>
    /// <returns>The current builder, for chaining.</returns>
    public IDofusDbQuery<TResource> Skip(int count)
    {
        _skip = count;
        return this;
    }

    /// <summary>
    ///     Sort the data by the specified property in ascending order.
    /// </summary>
    /// <param name="expression">The expression representing the property to sort by.</param>
    /// <returns>The current builder, for chaining.</returns>
    public IDofusDbQuery<TResource> SortByAscending(Expression<Func<TResource, object?>> expression)
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
    public IDofusDbQuery<TResource> SortByDescending(Expression<Func<TResource, object?>> expression)
    {
        string propertyName = ExtractPropertyName(expression);
        _sort[propertyName] = DofusDbSearchQuerySortOrder.Descending;
        return this;
    }

    /// <summary>
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IDofusDbQuery<TResource> Select(Expression<Func<TResource, object?>> expression)
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
    public IDofusDbQuery<TResource> Where(Expression<Func<TResource, bool>> expression)
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
    public IAsyncEnumerable<TResource> ExecuteAsync(CancellationToken cancellationToken = default) => ExecuteAsync(null, cancellationToken);

    /// <summary>
    ///     Executes the search query and returns an asynchronous enumerable of resources matching the query.
    ///     This method will perform as many requests as necessary to retrieve the requested number of results.
    /// </summary>
    /// <returns>The search result containing all resources matching the query.</returns>
    public IAsyncEnumerable<TResource> ExecuteAsync(IProgress<(int Loaded, int Total)>? progress, CancellationToken cancellationToken = default)
    {
        DofusDbSearchQuery query = BuildQuery();
        return client.MultiQuerySearchAsync(query, progress, cancellationToken);
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
                    if (memberExpression.Expression is not null
                        && memberExpression.Expression.Type.IsGenericType
                        && memberExpression.Expression.Type.GetGenericTypeDefinition() == typeof(Nullable<>)
                        && memberExpression.Member.Name == "Value")
                    {
                        // Skip the .Value part of the expression chain when it is used on a nullable type.
                        return ExtractPropertyChainRecursive(root, memberExpression.Expression, path);
                    }

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
                switch (e.Arguments.Count)
                {
                    case 1: // the Contains method with one parameter is a class method on collections, e.g. when called on a List<>
                    {
                        if (e.Object is null)
                        {
                            throw new ArgumentException("The 'Contains' method must be called on a collection.", nameof(expression));
                        }

                        string left = ExtractPropertyChain(root, e.Arguments[0]);
                        string[] right = ExtractCollectionValuesAsString(e.Object) ?? throw new ArgumentException("Collection is null.", nameof(expression));
                        return new DofusDbSearchPredicate.In(left, right);
                    }
                    case 2: // the Contains method with two parameters is an extension method on collections, e.g. when called on an IEnumerable
                    case 3: // the Contains method with tree parameters is an extension method on ReadOnlySpan<>
                    {
                        string left = ExtractPropertyChain(root, e.Arguments[1]);
                        string[] right = ExtractCollectionValuesAsString(e.Arguments[0]) ?? throw new ArgumentException("Collection is null.", nameof(expression));
                        return new DofusDbSearchPredicate.In(left, right);
                    }
                    default:
                        throw new ArgumentException($"Invalid call to method 'Contains': {expression}.", nameof(expression));
                }
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
        object? value = Expression.Lambda(expression).Compile().DynamicInvoke();
        return value switch
        {
            bool b => b ? "true" : "false",
            _ => value?.ToString() ?? "null"
        };
    }

    static string[]? ExtractCollectionValuesAsString(Expression expression)
    {
        Type type = expression.Type;

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ReadOnlySpan<>))
        {
            // by-ref structs like ReadOnlySpan<T> cannot be boxed so we need to extract the values manually by building the expression trees that access each element
            MemberInfo lengthMember = type.GetMember(nameof(ReadOnlySpan<object>.Length)).First();
            int length = Expression.Lambda<Func<int>>(Expression.MakeMemberAccess(expression, lengthMember)).Compile()();
            PropertyInfo indexer = type.GetProperty("Item") ?? throw new InvalidOperationException("Internal error.");
            object?[] spanContent = Enumerable
                .Range(0, length)
                .Select(index => Expression.Lambda<Func<object?>>(Expression.MakeIndex(expression, indexer, [Expression.Constant(index)])).Compile()())
                .ToArray();

            return FormatObjects(spanContent);
        }

        object? values = Expression.Lambda(expression).Compile().DynamicInvoke();
        if (values is null)
        {
            return null;
        }

        if (values is IEnumerable<bool> boolEnumerable)
        {
            return FormatBools(boolEnumerable);
        }

        if (values is IEnumerable enumerable)
        {
            return FormatObjects(enumerable.Cast<object?>());
        }

        throw new InvalidOperationException($"Could not extract collection values from value of type {values.GetType()}.");

        string[] FormatBools(IEnumerable<bool> bools)
        {
            return bools.Select(b => b ? "true" : "false").ToArray();
        }

        string[] FormatObjects(IEnumerable<object?> objects)
        {
            return objects.Select(o => o?.ToString() ?? "null").ToArray();
        }
    }
}
