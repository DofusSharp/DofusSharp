using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Maps;

namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     Factory for creating DofusDb API clients.
/// </summary>
/// <param name="baseAddress">The base URL of the API to query.</param>
/// <param name="referrer">The referer header to include in requests to the API.</param>
public class DofusDbClientsFactory(Uri baseAddress, Uri? referrer = null)
{
    public IDofusDbVersionClient Version() => new DofusDbVersionClient(new Uri(baseAddress, "version/"), referrer);

    public IDofusDbTableClient<Item> Items() => new DofusDbTableClient<Item>(new Uri(baseAddress, "items/"), referrer);
    public IDofusDbTableClient<ItemType> ItemTypes() => new DofusDbTableClient<ItemType>(new Uri(baseAddress, "item-types/"), referrer);
    public IDofusDbTableClient<ItemSuperType> ItemSuperTypes() => new DofusDbTableClient<ItemSuperType>(new Uri(baseAddress, "item-super-types/"), referrer);
    public IDofusDbTableClient<ItemSet> ItemSets() => new DofusDbTableClient<ItemSet>(new Uri(baseAddress, "item-sets/"), referrer);

    public IDofusDbTableClient<World> Worlds() => new DofusDbTableClient<World>(new Uri(baseAddress, "worlds/"), referrer);
    public IDofusDbTableClient<SuperArea> SuperAreas() => new DofusDbTableClient<SuperArea>(new Uri(baseAddress, "super-areas/"), referrer);
    public IDofusDbTableClient<Area> Areas() => new DofusDbTableClient<Area>(new Uri(baseAddress, "areas/"), referrer);
    public IDofusDbTableClient<SubArea> SubAreas() => new DofusDbTableClient<SubArea>(new Uri(baseAddress, "subareas/"), referrer);
    public IDofusDbTableClient<Map> Maps() => new DofusDbTableClient<Map>(new Uri(baseAddress, "maps/"), referrer);
    public IDofusDbTableClient<Dungeon> Dungeons() => new DofusDbTableClient<Dungeon>(new Uri(baseAddress, "dungeons/"), referrer);
}
