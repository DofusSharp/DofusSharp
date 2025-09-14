using System.Collections.Concurrent;
using BestCrush.Domain.Extensions;
using BestCrush.Domain.Models;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients;
using Microsoft.EntityFrameworkCore;

namespace BestCrush.Domain.Services;

public class ItemsService(BestCrushDbContext context, IDofusDbClientsFactory dofusDbClientsFactory, IDofocusClientFactory dofocusClientFactory, ImageCache imageCache)
{
    readonly ConcurrentDictionary<long, DofocusItem> _cachedItems = [];

    public async Task<IReadOnlyCollection<Equipment>> GetEquipmentsAsync() =>
        await context.Equipments.Include(i => i.Characteristics).Include(i => i.Recipe).ThenInclude(i => i.Resource).Where(i => i.Recipe.Count > 0).AsNoTracking().ToArrayAsync();

    public async Task<DofocusItem> GetItemAsync(long id, bool forceRefresh = false)
    {
        if (!forceRefresh && _cachedItems.TryGetValue(id, out DofocusItem? cachedItem))
        {
            return cachedItem;
        }

        IDofocusItemsClient client = dofocusClientFactory.Items();
        DofocusItem item = await client.GetItemAsync(id);
        _cachedItems.AddOrUpdate(id, item, (_, _) => item);

        return item;
    }

    public Task<string?> GetItemIconAsync(IItem item)
    {
        if (item.DofusDbIconId is null)
        {
            return Task.FromResult<string?>(null);
        }

        return GetItemIconAsync(item.DofusDbIconId.Value);
    }

    public async Task<string?> GetItemIconAsync(long iconId)
    {
        IDofusDbImagesClient<long> client = dofusDbClientsFactory.ItemImages();
        string cacheKey = $"item-icon-{iconId}.png";
        byte[] content = await imageCache.GetOrAddAsync(
            cacheKey,
            async () =>
            {
                await using Stream stream = await client.GetImageAsync(iconId);
                return stream.ReadToByteArray();
            }
        );

        return $"image/png;base64,{Convert.ToBase64String(content)}";
    }
}
