using DofusSharp.Dofocus.ApiClients;
using FluentAssertions;

namespace Tests.EndToEnd.Dofocus.ApiClients;

public class RunesClientTest
{
    [Fact]
    public async Task RunesClient_Should_GetRunes()
    {
        DofocusRunesClient client = DofocusClient.Runes();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        Func<Task> action = () => client.GetRunesAsync();

        await action.Should().NotThrowAsync();
    }
}
