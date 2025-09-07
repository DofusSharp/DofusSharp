using System.Net.Http.Headers;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Servers;

namespace BestCrush.Domain.Services;

public class ServersService(ImageCache imageCache, IDofocusClientFactory dofocusClientFactory)
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
                IDofocusServersClient serversClient = dofocusClientFactory.Servers();
                _servers = await serversClient.GetServersAsync();
            }

            return _servers;
        }
        finally
        {
            _serversLock.Release();
        }
    }

    public async Task<string> GetServerIconAsync(DofocusServer server)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
        string cacheKey = $"server-icon-{server.Name}.webp";
        byte[] content = await imageCache.GetOrAddAsync(cacheKey, async () => await httpClient.GetByteArrayAsync($"https://dofocus.fr/servers/{server.Name.ToLower()}.webp"));
        return $"image/webp;base64,{Convert.ToBase64String(content)}";
    }

    public Task<DofocusServer?> GetCurrentServer() => Task.FromResult(_currentServer);

    public Task SetCurrentServerAsync(DofocusServer server)
    {
        _currentServer = server;
        return Task.CompletedTask;
    }
}
