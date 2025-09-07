using DofusSharp.Dofocus.ApiClients;
using FluentAssertions;

namespace Tests.Live.Dofocus.ApiClients;

public class ServersClientTest
{
    [Fact]
    public async Task ServersClient_Should_GetServers()
    {
        IDofocusServersClient client = DofocusClient.Production().Servers();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        Func<Task> action = () => client.GetServersAsync();

        await action.Should().NotThrowAsync();
    }
}
