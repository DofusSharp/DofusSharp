using DofusSharp.DofusDb.ApiClients.Clients;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
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

    public IDofusDbTableClient<DofusDbCharacteristic> Characteristics() => new DofusDbTableClient<DofusDbCharacteristic>(new Uri(baseAddress, "characteristics/"), referrer);
    
    public IDofusDbTableClient<DofusDbItem> Items() => new DofusDbTableClient<DofusDbItem>(new Uri(baseAddress, "items/"), referrer);
    public IDofusDbImageClient<int> ItemImages() => new DofusDbImageClient<int>(new Uri(baseAddress, "img/items/"), ImageFormat.Png, referrer);
    public IDofusDbTableClient<DofusDbItemType> ItemTypes() => new DofusDbTableClient<DofusDbItemType>(new Uri(baseAddress, "item-types/"), referrer);
    public IDofusDbTableClient<DofusDbItemSuperType> ItemSuperTypes() => new DofusDbTableClient<DofusDbItemSuperType>(new Uri(baseAddress, "item-super-types/"), referrer);
    public IDofusDbTableClient<DofusDbItemSet> ItemSets() => new DofusDbTableClient<DofusDbItemSet>(new Uri(baseAddress, "item-sets/"), referrer);

    public IDofusDbTableClient<DofusDbJob> Jobs() => new DofusDbTableClient<DofusDbJob>(new Uri(baseAddress, "jobs/"), referrer);
    public IDofusDbImageClient<int> JobImages() => new DofusDbImageClient<int>(new Uri(baseAddress, "img/jobs/"), ImageFormat.Jpeg, referrer);
    public IDofusDbTableClient<DofusDbRecipe> Recipes() => new DofusDbTableClient<DofusDbRecipe>(new Uri(baseAddress, "recipes/"), referrer);
    public IDofusDbTableClient<DofusDbSkill> Skills() => new DofusDbTableClient<DofusDbSkill>(new Uri(baseAddress, "skills/"), referrer);

    public IDofusDbTableClient<DofusDbSpell> Spells() => new DofusDbTableClient<DofusDbSpell>(new Uri(baseAddress, "spells/"), referrer);
    public IDofusDbImageClient<int> SpellImages() => new DofusDbImageClient<int>(new Uri(baseAddress, "img/spells/"), ImageFormat.Png, "sort_", referrer);
    public IDofusDbTableClient<DofusDbSpellLevel> SpellLevels() => new DofusDbTableClient<DofusDbSpellLevel>(new Uri(baseAddress, "spell-levels/"), referrer);
    public IDofusDbTableClient<DofusDbSpellState> SpellStates() => new DofusDbTableClient<DofusDbSpellState>(new Uri(baseAddress, "spell-states/"), referrer);
    public IDofusDbImageClient<string> SpellStateImages() => new DofusDbImageClient<string>(new Uri(baseAddress, "img/states/"), ImageFormat.Png, referrer);
    public IDofusDbTableClient<DofusDbSpellVariant> SpellVariants() => new DofusDbTableClient<DofusDbSpellVariant>(new Uri(baseAddress, "spell-variants/"), referrer);

    public IDofusDbTableClient<DofusDbWorld> Worlds() => new DofusDbTableClient<DofusDbWorld>(new Uri(baseAddress, "worlds/"), referrer);
    public IDofusDbTableClient<DofusDbSuperArea> SuperAreas() => new DofusDbTableClient<DofusDbSuperArea>(new Uri(baseAddress, "super-areas/"), referrer);
    public IDofusDbTableClient<DofusDbArea> Areas() => new DofusDbTableClient<DofusDbArea>(new Uri(baseAddress, "areas/"), referrer);
    public IDofusDbTableClient<DofusDbSubArea> SubAreas() => new DofusDbTableClient<DofusDbSubArea>(new Uri(baseAddress, "subareas/"), referrer);
    public IDofusDbTableClient<DofusDbMap> Maps() => new DofusDbTableClient<DofusDbMap>(new Uri(baseAddress, "maps/"), referrer);
    public IDofusDbScalableImageClient<int> MapImages() => new DofusDbScalableImageClient<int>(new Uri(baseAddress, "img/maps/"), ImageFormat.Jpeg, referrer);
    public IDofusDbTableClient<DofusDbMapPosition> MapPositions() => new DofusDbTableClient<DofusDbMapPosition>(new Uri(baseAddress, "map-positions/"), referrer);
    public IDofusDbTableClient<DofusDbDungeon> Dungeons() => new DofusDbTableClient<DofusDbDungeon>(new Uri(baseAddress, "dungeons/"), referrer);
}
