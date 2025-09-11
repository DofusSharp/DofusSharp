using System.Text.Json.Serialization.Metadata;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;

namespace DofusSharp.DofusDb.ApiClients.Clients;

/// <summary>
///     Factory for creating DofusDb API clients.
/// </summary>
/// <param name="baseAddress">The base URL of the API to query.</param>
/// <param name="referrer">The referer header to include in requests to the API.</param>
class DofusDbClientsFactory(Uri baseAddress, IJsonTypeInfoResolver typeInfoResolver, Uri? referrer = null) : IDofusDbClientsFactory
{
    public IDofusDbVersionClient Version() => new DofusDbVersionClient(new Uri(baseAddress, "version/"), referrer);

    public IDofusDbTableClient<DofusDbServer> Servers() => new DofusDbTableClient<DofusDbServer>(typeInfoResolver, new Uri(baseAddress, "servers/"), referrer);

    public IDofusDbTableClient<DofusDbCharacteristic> Characteristics() =>
        new DofusDbTableClient<DofusDbCharacteristic>(typeInfoResolver, new Uri(baseAddress, "characteristics/"), referrer);

    public IDofusDbTableClient<DofusDbItem> Items() => new DofusDbTableClient<DofusDbItem>(typeInfoResolver, new Uri(baseAddress, "items/"), referrer);
    public IDofusDbImageClient<long> ItemImages() => new DofusDbImageClient<long>(new Uri(baseAddress, "img/items/"), ImageFormat.Png, referrer);
    public IDofusDbTableClient<DofusDbItemType> ItemTypes() => new DofusDbTableClient<DofusDbItemType>(typeInfoResolver, new Uri(baseAddress, "item-types/"), referrer);

    public IDofusDbTableClient<DofusDbItemSuperType> ItemSuperTypes() =>
        new DofusDbTableClient<DofusDbItemSuperType>(typeInfoResolver, new Uri(baseAddress, "item-super-types/"), referrer);

    public IDofusDbTableClient<DofusDbItemSet> ItemSets() => new DofusDbTableClient<DofusDbItemSet>(typeInfoResolver, new Uri(baseAddress, "item-sets/"), referrer);

    public IDofusDbTableClient<DofusDbJob> Jobs() => new DofusDbTableClient<DofusDbJob>(typeInfoResolver, new Uri(baseAddress, "jobs/"), referrer);
    public IDofusDbImageClient<long> JobImages() => new DofusDbImageClient<long>(new Uri(baseAddress, "img/jobs/"), ImageFormat.Jpeg, referrer);
    public IDofusDbTableClient<DofusDbRecipe> Recipes() => new DofusDbTableClient<DofusDbRecipe>(typeInfoResolver, new Uri(baseAddress, "recipes/"), referrer);
    public IDofusDbTableClient<DofusDbSkill> Skills() => new DofusDbTableClient<DofusDbSkill>(typeInfoResolver, new Uri(baseAddress, "skills/"), referrer);

    public IDofusDbTableClient<DofusDbSpell> Spells() => new DofusDbTableClient<DofusDbSpell>(typeInfoResolver, new Uri(baseAddress, "spells/"), referrer);
    public IDofusDbImageClient<long> SpellImages() => new DofusDbImageClient<long>(new Uri(baseAddress, "img/spells/"), ImageFormat.Png, "sort_", referrer);
    public IDofusDbTableClient<DofusDbSpellLevel> SpellLevels() => new DofusDbTableClient<DofusDbSpellLevel>(typeInfoResolver, new Uri(baseAddress, "spell-levels/"), referrer);
    public IDofusDbTableClient<DofusDbSpellState> SpellStates() => new DofusDbTableClient<DofusDbSpellState>(typeInfoResolver, new Uri(baseAddress, "spell-states/"), referrer);
    public IDofusDbImageClient<string> SpellStateImages() => new DofusDbImageClient<string>(new Uri(baseAddress, "img/states/"), ImageFormat.Png, referrer);

    public IDofusDbTableClient<DofusDbSpellVariant> SpellVariants() =>
        new DofusDbTableClient<DofusDbSpellVariant>(typeInfoResolver, new Uri(baseAddress, "spell-variants/"), referrer);

    public IDofusDbTableClient<DofusDbMonster> Monsters() => new DofusDbTableClient<DofusDbMonster>(typeInfoResolver, new Uri(baseAddress, "monsters/"), referrer);
    public IDofusDbImageClient<long> MonsterImages() => new DofusDbImageClient<long>(new Uri(baseAddress, "img/monsters/"), ImageFormat.Png, referrer);
    public IDofusDbTableClient<DofusDbMonsterRace> MonsterRaces() => new DofusDbTableClient<DofusDbMonsterRace>(typeInfoResolver, new Uri(baseAddress, "monster-races/"), referrer);

    public IDofusDbTableClient<DofusDbMonsterSuperRace> MonsterSuperRaces() =>
        new DofusDbTableClient<DofusDbMonsterSuperRace>(typeInfoResolver, new Uri(baseAddress, "monster-super-races/"), referrer);

    public IDofusDbTableClient<DofusDbWorld> Worlds() => new DofusDbTableClient<DofusDbWorld>(typeInfoResolver, new Uri(baseAddress, "worlds/"), referrer);
    public IDofusDbTableClient<DofusDbSuperArea> SuperAreas() => new DofusDbTableClient<DofusDbSuperArea>(typeInfoResolver, new Uri(baseAddress, "super-areas/"), referrer);
    public IDofusDbTableClient<DofusDbArea> Areas() => new DofusDbTableClient<DofusDbArea>(typeInfoResolver, new Uri(baseAddress, "areas/"), referrer);
    public IDofusDbTableClient<DofusDbSubArea> SubAreas() => new DofusDbTableClient<DofusDbSubArea>(typeInfoResolver, new Uri(baseAddress, "subareas/"), referrer);
    public IDofusDbTableClient<DofusDbMap> Maps() => new DofusDbTableClient<DofusDbMap>(typeInfoResolver, new Uri(baseAddress, "maps/"), referrer);
    public IDofusDbScalableImageClient<long> MapImages() => new DofusDbScalableImageClient<long>(new Uri(baseAddress, "img/maps/"), ImageFormat.Jpeg, referrer);
    public IDofusDbTableClient<DofusDbMapPosition> MapPositions() => new DofusDbTableClient<DofusDbMapPosition>(typeInfoResolver, new Uri(baseAddress, "map-positions/"), referrer);
    public IDofusDbTableClient<DofusDbDungeon> Dungeons() => new DofusDbTableClient<DofusDbDungeon>(typeInfoResolver, new Uri(baseAddress, "dungeons/"), referrer);
}
