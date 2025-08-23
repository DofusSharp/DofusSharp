using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class ItemSuperTypesClientTest
{
    [Fact]
    public async Task ItemSuperTypesClient_Should_GetItemSuperType()
    {
        IDofusDbTableClient<DofusDbItemSuperType> client = DofusDbClient.Beta(Constants.Referrer).ItemSuperTypes();
        DofusDbItemSuperType value = await client.GetAsync(1);
        await Verify(value);
    }

    [Fact]
    public async Task ItemSuperTypesClient_Should_SearchItemSuperTypes()
    {
        IDofusDbTableClient<DofusDbItemSuperType> client = DofusDbClient.Beta(Constants.Referrer).ItemSuperTypes();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbItemSuperType[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync().AsTask();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
