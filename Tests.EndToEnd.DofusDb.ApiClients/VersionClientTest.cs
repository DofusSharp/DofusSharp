using DofusSharp.DofusDb.ApiClients;
using FluentAssertions;

namespace Tests.EndToEnd.DofusDb.ApiClients;

public class VersionClientTest
{
    [Fact]
    public async Task VersionClient_Should_GetVersion()
    {
        IDofusDbVersionClient client = DofusDbClient.Beta().Version();
        Version value = await client.GetVersionAsync();
        value.Should().NotBeNull(); // we just want to ensure that the version is parsed correctly
    }
}
