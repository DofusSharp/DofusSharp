using System.Net.Http.Headers;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Servers;

namespace BestCrush.Domain.Services;

public class ServersService(ImageCache imageCache)
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

    public async Task<string> GetServerIconAsync(DofocusServer server)
    {
        byte[] content;

        string cacheKey = $"server-icon-{server.Name}.webp";
        if (await imageCache.HasImageAsync(cacheKey))
        {
            content = await imageCache.LoadImageAsync(cacheKey);
        }
        else
        {
            string url = $"https://dofocus.fr/servers/{server.Name.ToLower()}.webp";

            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
            content = await httpClient.GetByteArrayAsync(url);

            await imageCache.StoreImageAsync(cacheKey, content);
        }

        return $"image/webp;base64,{Convert.ToBase64String(content)}";
    }

    public Task<DofocusServer?> GetCurrentServer() => Task.FromResult(_currentServer);

    public Task SetCurrentServerAsync(DofocusServer server)
    {
        _currentServer = server;
        return Task.CompletedTask;
    }
}
