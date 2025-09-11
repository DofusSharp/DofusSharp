# [DofusDB](https://dofusdb.fr) API Clients

> [!IMPORTANT]
> This project is under active development. Note that not all available APIs have been implemented at this time.

Provides API clients for the DofusDB API.
Most of the APIs are search APIs that allow you to query the DofusDB database for various resources such as items, monsters, spells, etc. For these APIs, the library providers both a low-level client (`IDofusDbTableClient<TResource>`) for maximum control and a high-level, fluent interface (`DofusDbQuery<TResource>`) for building requests using LINQ-like statements.
The remaining APIs are simple GET APIs that return a single resource, such as the version of the game data or the map images at various scales.

## Installation

Install via NuGet:

```
dotnet add package DofusSharp.DofusDb.ApiClients
```

## Usage

In both the examples below we will fetch the items from level 50 to 100, that are not consumables, select only the `name` field and order them by `realWeight` in descending order.

### Query interface (recommended)

The query interface returns an `IAsyncEnumerable<TResource>` that will fetch all the available pages automatically while iterating over the results.

```csharp
IDofusDbQuery<DofusDbItem> query = DofusDbQuery.Production().Items()
    .Select(i => i.Name)
    .OrderByDescending(i => i.RealWeight)
    .Where(i => i.Level >= 50 && i.Level <= 100 && i.Usable == false);
IAsyncEnumerable<DofusDbItem> itemsEnumerable = await query.ExecuteAsync();
DofusDbItem[] items = itemsEnumerable.ToArrayAsync();
```

> [!NOTE]
> All model fields are nullable because the API supports a `select` operator for partial field selection. 
> As a result, enabling nullable analysis may cause compiler warnings about possible null references in expression subtrees. 
> However, these warnings are safe to ignore in this context, since the expressions are only used to determine property names for request parameters and will not cause null reference exceptions at runtime.

### Low-level client

The low-level client grants direct access to the request parameters exposed by `FeatherJS`.

```csharp
IDofusDbTableClient<DofusDbItem> client = DofusDbTableClient.Production().Items();
SearchResult<DofusDbItem> items = await client.SearchAsync(
    new DofusDbSearchQuery
    {
        Limit = 50,
        Select = ["name"],
        Sort = new Dictionary<string, DofusDbSearchQuerySortOrder> { { "realWeight", DofusDbSearchQuerySortOrder.Descending } }, 
        Predicates =
        [
            new DofusDbSearchPredicate.GreaterThanOrEqual("level", "50"),
            new DofusDbSearchPredicate.LessThanOrEqual("level", "100"),
            new DofusDbSearchPredicate.Eq("usable", "false")
        ]
    }
);
```

**Note:** the query string generated from a `SearchQuery` can be computed by calling the `ToQueryString` extension method on the `SearchQuery` object. This can be useful for debugging or logging purposes. 
For example the search query above would generate the following query string:

```
limit=50&select=name&sort[realWeight]=desc&level[$gte]=50&level[$lte]=100&usable=false
```

## Contributing

Contributions are welcome! Please open issues or submit pull requests for bug fixes, features, or documentation improvements.

## License

This project is licensed under the terms of the [MIT License](../LICENSE.md).