using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Servers;

namespace BestCrush.Services;

public class ServersService
{
    IReadOnlyCollection<DofocusServer>? _servers;
    readonly SemaphoreSlim _serversLock = new(1, 1);
    DofocusServer? _currentServer;

    public async Task<IReadOnlyCollection<DofocusServer>> GetServers()
    {
        if (_servers is not null)
        {
            return _servers;
        }

        await _serversLock.WaitAsync();
        try
        {
            if (_servers is null)
            {
                DofocusServersClient serversClient = DofocusClient.Servers();
                _servers = await serversClient.GetServersAsync();
            }

            return _servers;
        }
        finally
        {
            _serversLock.Release();
        }
    }

    public Task<DofocusServer?> GetCurrentServer() => Task.FromResult(_currentServer);

    public Task SetCurrentServerAsync(DofocusServer server)
    {
        _currentServer = server;
        return Task.CompletedTask;
    }
}
