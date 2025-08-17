using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Maps;

namespace DofusSharp.DofusDb.ApiClients;

public class DofusDbQueryProvider(DofusDbClientsFactory factory)
{
    public DofusDbQuery<Item> Items() => new(factory.Items());
    public DofusDbQuery<ItemType> ItemTypes() => new(factory.ItemTypes());
    public DofusDbQuery<ItemSuperType> ItemSuperTypes() => new(factory.ItemSuperTypes());
    public DofusDbQuery<ItemSet> ItemSets() => new(factory.ItemSets());

    public DofusDbQuery<World> Worlds() => new(factory.Worlds());
    public DofusDbQuery<SuperArea> SuperAreas() => new(factory.SuperAreas());
    public DofusDbQuery<Area> Areas() => new(factory.Areas());
    public DofusDbQuery<SubArea> SubAreas() => new(factory.SubAreas());
    public DofusDbQuery<Map> Maps() => new(factory.Maps());
    public DofusDbQuery<MapPosition> MapPositions() => new(factory.MapPositions());
    public DofusDbQuery<Dungeon> Dungeons() => new(factory.Dungeons());
}
