using DofusSharp.Dofocus.ApiClients;
using FluentAssertions;

namespace Tests.EndToEnd.Dofocus.ApiClients;

public class ItemsClientTest
{
    [Fact]
    public async Task ItemsClient_Should_GetItems()
    {
        DofocusItemsClient client = DofocusClient.Items();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        Func<Task> action = () => client.GetItemsAsync();

        await action.Should().NotThrowAsync();
    }
}
