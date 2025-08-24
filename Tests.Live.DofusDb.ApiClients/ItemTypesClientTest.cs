using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class ItemTypesClientTest
{
    [Fact]
    public async Task ItemTypesClient_Should_GetItemType()
    {
        IDofusDbTableClient<DofusDbItemType> client = DofusDbClient.Beta(Constants.Referrer).ItemTypes();
        DofusDbItemType value = await client.GetAsync(1);
        await Verify(value);
    }

    [Fact]
    public async Task ItemTypesClient_Should_SearchItemTypes()
    {
        IDofusDbTableClient<DofusDbItemType> client = DofusDbClient.Beta(Constants.Referrer).ItemTypes();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbItemType[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync();

        results.Length.Should().Be(count);
    }
}
