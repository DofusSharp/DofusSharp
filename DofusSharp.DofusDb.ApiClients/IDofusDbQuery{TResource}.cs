using System.Linq.Expressions;
using DofusSharp.DofusDb.ApiClients.Models;

namespace DofusSharp.DofusDb.ApiClients;

public interface IDofusDbQuery<TResource> where TResource: DofusDbResource
{
    /// <summary>
    ///     Sets the maximum number of items to return in the result set.
    /// </summary>
    /// <param name="count">The maximum number of items to return.</param>
    /// <returns>The current builder, for chaining.</returns>
    IDofusDbQuery<TResource> Take(int count);

    /// <summary>
    ///     Sets the number of items to skip before starting to collect the result set.
    /// </summary>
    /// <param name="count">The number of items to skip.</param>
    /// <returns>The current builder, for chaining.</returns>
    IDofusDbQuery<TResource> Skip(int count);

    /// <summary>
    ///     Sort the data by the specified property in ascending order.
    /// </summary>
    /// <param name="expression">The expression representing the property to sort by.</param>
    /// <returns>The current builder, for chaining.</returns>
    IDofusDbQuery<TResource> SortByAscending(Expression<Func<TResource, object?>> expression);

    /// <summary>
    ///     Sort the data by the specified property in descending order.
    /// </summary>
    /// <param name="expression">The expression representing the property to sort by.</param>
    /// <returns>The current builder, for chaining.</returns>
    IDofusDbQuery<TResource> SortByDescending(Expression<Func<TResource, object?>> expression);

    /// <summary>
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    IDofusDbQuery<TResource> Select(Expression<Func<TResource, object?>> expression);

    /// <summary>
    ///     Adds a predicate to the search query, allowing for complex filtering of results.
    /// </summary>
    /// <param name="expression">The expression representing the predicate to apply.</param>
    /// <returns>The current builder, for chaining.</returns>
    IDofusDbQuery<TResource> Where(Expression<Func<TResource, bool>> expression);

    /// <summary>
    ///     Executes the search query and returns an asynchronous enumerable of resources matching the query.
    ///     This method will perform as many requests as necessary to retrieve the requested number of results.
    /// </summary>
    /// <returns>The search result containing all resources matching the query.</returns>
    IAsyncEnumerable<TResource> ExecuteAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Executes the search query and returns an asynchronous enumerable of resources matching the query.
    ///     This method will perform as many requests as necessary to retrieve the requested number of results.
    /// </summary>
    /// <returns>The search result containing all resources matching the query.</returns>
    IAsyncEnumerable<TResource> ExecuteAsync(IProgress<(int Loaded, int Total)>? progress, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Count the number of results that the `ExecuteAsync` method would return.
    ///     This method only performs one request and does not retrieve the actual resources.
    /// </summary>
    /// <returns>The search result containing all resources matching the query.</returns>
    Task<int> CountAsync(CancellationToken cancellationToken = default);
}
