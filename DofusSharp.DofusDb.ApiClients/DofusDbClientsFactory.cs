using DofusSharp.DofusDb.ApiClients.Clients;
using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Spells;

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
    public IDofusDbImageClient<int> ItemImages() => new DofusDbImageClient<int>(new Uri(baseAddress, "img/items/"), ImageFormat.Png, referrer);
    public IDofusDbTableClient<ItemType> ItemTypes() => new DofusDbTableClient<ItemType>(new Uri(baseAddress, "item-types/"), referrer);
    public IDofusDbTableClient<ItemSuperType> ItemSuperTypes() => new DofusDbTableClient<ItemSuperType>(new Uri(baseAddress, "item-super-types/"), referrer);
    public IDofusDbTableClient<ItemSet> ItemSets() => new DofusDbTableClient<ItemSet>(new Uri(baseAddress, "item-sets/"), referrer);

    public IDofusDbTableClient<Job> Jobs() => new DofusDbTableClient<Job>(new Uri(baseAddress, "jobs/"), referrer);
    public IDofusDbImageClient<int> JobImages() => new DofusDbImageClient<int>(new Uri(baseAddress, "img/jobs/"), ImageFormat.Jpeg, referrer);
    public IDofusDbTableClient<Recipe> Recipes() => new DofusDbTableClient<Recipe>(new Uri(baseAddress, "recipes/"), referrer);
    public IDofusDbTableClient<Skill> Skills() => new DofusDbTableClient<Skill>(new Uri(baseAddress, "skills/"), referrer);

    public IDofusDbTableClient<Spell> Spells() => new DofusDbTableClient<Spell>(new Uri(baseAddress, "spells/"), referrer);
    public IDofusDbImageClient<int> SpellImages() => new DofusDbImageClient<int>(new Uri(baseAddress, "img/spells/"), ImageFormat.Png, "sort_", referrer);
    public IDofusDbTableClient<SpellLevel> SpellLevels() => new DofusDbTableClient<SpellLevel>(new Uri(baseAddress, "spell-levels/"), referrer);
    public IDofusDbTableClient<SpellState> SpellStates() => new DofusDbTableClient<SpellState>(new Uri(baseAddress, "spell-states/"), referrer);
    public IDofusDbImageClient<string> SpellStateImages() => new DofusDbImageClient<string>(new Uri(baseAddress, "img/states/"), ImageFormat.Png, referrer);
    public IDofusDbTableClient<SpellVariant> SpellVariants() => new DofusDbTableClient<SpellVariant>(new Uri(baseAddress, "spell-variants/"), referrer);

    public IDofusDbTableClient<World> Worlds() => new DofusDbTableClient<World>(new Uri(baseAddress, "worlds/"), referrer);
    public IDofusDbTableClient<SuperArea> SuperAreas() => new DofusDbTableClient<SuperArea>(new Uri(baseAddress, "super-areas/"), referrer);
    public IDofusDbTableClient<Area> Areas() => new DofusDbTableClient<Area>(new Uri(baseAddress, "areas/"), referrer);
    public IDofusDbTableClient<SubArea> SubAreas() => new DofusDbTableClient<SubArea>(new Uri(baseAddress, "subareas/"), referrer);
    public IDofusDbTableClient<Map> Maps() => new DofusDbTableClient<Map>(new Uri(baseAddress, "maps/"), referrer);
    public IDofusDbScalableImageClient<int> MapImages() => new DofusDbScalableImageClient<int>(new Uri(baseAddress, "img/maps/"), ImageFormat.Jpeg, referrer);
    public IDofusDbTableClient<MapPosition> MapPositions() => new DofusDbTableClient<MapPosition>(new Uri(baseAddress, "map-positions/"), referrer);
    public IDofusDbTableClient<Dungeon> Dungeons() => new DofusDbTableClient<Dungeon>(new Uri(baseAddress, "dungeons/"), referrer);
}
