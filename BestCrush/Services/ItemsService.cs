using System.Collections.Concurrent;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Items;

namespace BestCrush.Services;

public class ItemsService
{
    IReadOnlyCollection<DofocusItemMinimal>? _items;
    readonly SemaphoreSlim _itemsSemaphore = new(1, 1);

    readonly ConcurrentDictionary<long, DofocusItem> _cachedItems = [];
    readonly ConcurrentDictionary<string, string> _cachedIcons = [];

    public async Task<IReadOnlyCollection<DofocusItemMinimal>> GetItemsAsync()
    {
        if (_items != null)
        {
            return _items;
        }

        await _itemsSemaphore.WaitAsync();
        try
        {
            if (_items != null)
            {
                return _items;
            }

            DofocusItemsClient client = DofocusClient.Items();
            _items = await client.GetItemsAsync();
            return _items;
        }
        finally
        {
            _itemsSemaphore.Release();
        }
    }

    public async Task<DofocusItem> GetItemAsync(long id, bool forceRefresh = false)
    {
        if (!forceRefresh && _cachedItems.TryGetValue(id, out DofocusItem? cachedItem))
        {
            return cachedItem;
        }

        DofocusItemsClient client = DofocusClient.Items();
        DofocusItem item = await client.GetItemAsync(id);
        _cachedItems.AddOrUpdate(id, item, (_, _) => item);

        return item;
    }

    public async Task<string?> GetItemIconAsync(DofocusItem item)
    {
        if (string.IsNullOrWhiteSpace(item.ImageUrl))
        {
            return null;
        }

        string cacheKey = item.ImageUrl.Replace("://", "_").Replace(".", "_").Replace("/", "_");
        if (_cachedIcons.TryGetValue(cacheKey, out string? icon))
        {
            return icon;
        }

        using HttpClient httpClient = new();
        byte[] iconBytes = await httpClient.GetByteArrayAsync(item.ImageUrl);
        icon = $"image/png;base64,{Convert.ToBase64String(iconBytes)}";

        _cachedIcons.TryAdd(cacheKey, icon);

        return icon;
    }
}
