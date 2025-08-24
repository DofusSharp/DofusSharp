using System.Collections.Concurrent;
using BestCrush.Domain.Extensions;
using BestCrush.Domain.Models;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients;
using Microsoft.EntityFrameworkCore;

namespace BestCrush.Domain.Services;

public class ItemsService(BestCrushDbContext context, ImageCache imageCache)
{
    readonly ConcurrentDictionary<long, DofocusItem> _cachedItems = [];

    public async Task<IReadOnlyCollection<Item>> GetItemsAsync() => await context.Items.ToArrayAsync();

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

    public async Task<string?> GetItemIconAsync(Item item)
    {
        if (item.DofusDbIconId is null)
        {
            return null;
        }

        IDofusDbImageClient<long> client = DofusDbClient.Production(Constants.Referrer).ItemImages();
        string cacheKey = $"item-icon-{item.DofusDbIconId}.png";
        byte[] content = await imageCache.GetOrAddAsync(
            cacheKey,
            async () =>
            {
                await using Stream stream = await client.GetImageAsync(item.DofusDbIconId.Value);
                return stream.ReadToByteArray();
            }
        );
        return $"image/png;base64,{Convert.ToBase64String(content)}";

    }
}
