using System.Collections.Concurrent;
using System.Net.Http.Headers;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Items;

namespace BestCrush.Services;

public class ItemsService(ImageCache imageCache)
{
    IReadOnlyCollection<DofocusItemMinimal>? _items;
    readonly SemaphoreSlim _itemsSemaphore = new(1, 1);

    readonly ConcurrentDictionary<long, DofocusItem> _cachedItems = [];

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

        string urlAsPath = item.ImageUrl.Replace("://", "_").Replace(".", "_").Replace("/", "_");
        if (urlAsPath.StartsWith("https_"))
        {
            urlAsPath = urlAsPath[6..];
        }
        if (urlAsPath.StartsWith("http_"))
        {
            urlAsPath = urlAsPath[5..];
        }
        if (urlAsPath.StartsWith("api_dofusdb_fr_img_items_"))
        {
            urlAsPath = urlAsPath[25..];
        }
        if (urlAsPath.EndsWith("_png"))
        {
            urlAsPath = urlAsPath[..^4] + ".png";
        }
        if (urlAsPath.EndsWith("_jpg"))
        {
            urlAsPath = urlAsPath[..^4] + ".jpg";
        }
        if (urlAsPath.EndsWith("_webp"))
        {
            urlAsPath = urlAsPath[..^5] + ".webp";
        }

        string urlAsPathWithoutExtension = Path.GetFileNameWithoutExtension(urlAsPath);
        string extension = Path.GetExtension(urlAsPath) is { } ext ? ext[1..] : "png";
        string cacheKey = $"item-icon-{urlAsPathWithoutExtension}.{extension}";

        byte[] content;
        if (await imageCache.HasImageAsync(cacheKey))
        {
            content = await imageCache.LoadImageAsync(cacheKey);
        }

        else
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
            content = await httpClient.GetByteArrayAsync(item.ImageUrl);

            await imageCache.StoreImageAsync(cacheKey, content);
        }

        return $"image/{extension};base64,{Convert.ToBase64String(content)}";

    }
}
