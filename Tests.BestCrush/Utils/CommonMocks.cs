using System.Linq.Expressions;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;
using Moq;

namespace Tests.BestCrush.Utils;

static class CommonMocks
{
    public static Mock<IDofusDbQuery<T>> Query<T>() where T: DofusDbResource
    {
        Mock<IDofusDbQuery<T>> result = new();
        result.Setup(q => q.Where(It.IsAny<Expression<Func<T, bool>>>())).Returns(result.Object);
        result.Setup(q => q.Select(It.IsAny<Expression<Func<T, object?>>>())).Returns(result.Object);
        result.Setup(q => q.Take(It.IsAny<int>())).Returns(result.Object);
        result.Setup(q => q.Skip(It.IsAny<int>())).Returns(result.Object);
        result.Setup(q => q.SortByAscending(It.IsAny<Expression<Func<T, object?>>>())).Returns(result.Object);
        result.Setup(q => q.SortByDescending(It.IsAny<Expression<Func<T, object?>>>())).Returns(result.Object);
        result
            .Setup(q => q.ExecuteAsync(It.IsAny<IProgress<DofusDbTableClientExtensions.MultiSearchQueryProgress>?>(), It.IsAny<CancellationToken>()))
            .Returns(AsyncEnumerable.Empty<T>());
        result.Setup(q => q.CountAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
        return result;
    }

    public static Mock<IDofusDbTableClient<T>> TableClient<T>() where T: DofusDbResource
    {
        Mock<IDofusDbTableClient<T>> result = new();
        result
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DofusDbSearchResult<T> { Data = [], Total = 0, Limit = 0, Skip = 0 });
        result.Setup(q => q.CountAsync(It.IsAny<IReadOnlyCollection<DofusDbSearchPredicate>>(), It.IsAny<CancellationToken>())).ReturnsAsync(0);
        return result;
    }
}
