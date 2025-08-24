using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class CharacteristicsClientTest
{
    [Fact]
    public async Task CharacteristicsClient_Should_GetCharacteristic()
    {
        IDofusDbTableClient<DofusDbCharacteristic> client = DofusDbClient.Beta(Constants.Referrer).Characteristics();
        DofusDbCharacteristic value = await client.GetAsync(11);
        await Verify(value);
    }

    [Fact]
    public async Task CharacteristicsClient_Should_SearchCharacteristics()
    {
        IDofusDbTableClient<DofusDbCharacteristic> client = DofusDbClient.Beta(Constants.Referrer).Characteristics();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbCharacteristic[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
