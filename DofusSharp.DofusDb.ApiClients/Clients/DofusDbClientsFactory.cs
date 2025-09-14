using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Mounts;
using DofusSharp.DofusDb.ApiClients.Models.Ornaments;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Models.Titles;

namespace DofusSharp.DofusDb.ApiClients.Clients;

/// <summary>
///     Factory for creating DofusDb API clients.
/// </summary>
/// <param name="baseAddress">The base URL of the API to query.</param>
/// <param name="referrer">The referer header to include in requests to the API.</param>
class DofusDbClientsFactory(Uri baseAddress, Uri? referrer = null) : IDofusDbClientsFactory
{
    // @formatter:max_line_length 9999
    public IDofusDbVersionClient Version() => new DofusDbVersionClient(new Uri(baseAddress, "version/"), referrer);
    public IDofusDbCriterionClient Criterion() => new DofusDbCriterionClient(new Uri(baseAddress, "criterion/"), referrer);

    public IDofusDbTableClient<DofusDbServer> Servers() => new DofusDbTableClient<DofusDbServer>(new Uri(baseAddress, "servers/"), referrer);

    public IDofusDbTableClient<DofusDbCharacteristic> Characteristics() => new DofusDbTableClient<DofusDbCharacteristic>(new Uri(baseAddress, "characteristics/"), referrer);

    public IDofusDbTableClient<DofusDbItem> Items() => new DofusDbTableClient<DofusDbItem>(new Uri(baseAddress, "items/"), referrer);
    public IDofusDbImageClient<long> ItemImages() => new DofusDbImageClient<long>(new Uri(baseAddress, "img/items/"), ImageFormat.Png, referrer);
    public IDofusDbTableClient<DofusDbItemType> ItemTypes() => new DofusDbTableClient<DofusDbItemType>(new Uri(baseAddress, "item-types/"), referrer);
    public IDofusDbTableClient<DofusDbItemSuperType> ItemSuperTypes() => new DofusDbTableClient<DofusDbItemSuperType>(new Uri(baseAddress, "item-super-types/"), referrer);
    public IDofusDbTableClient<DofusDbItemSet> ItemSets() => new DofusDbTableClient<DofusDbItemSet>(new Uri(baseAddress, "item-sets/"), referrer);

    public IDofusDbTableClient<DofusDbJob> Jobs() => new DofusDbTableClient<DofusDbJob>(new Uri(baseAddress, "jobs/"), referrer);
    public IDofusDbImageClient<long> JobImages() => new DofusDbImageClient<long>(new Uri(baseAddress, "img/jobs/"), ImageFormat.Jpeg, referrer);
    public IDofusDbTableClient<DofusDbRecipe> Recipes() => new DofusDbTableClient<DofusDbRecipe>(new Uri(baseAddress, "recipes/"), referrer);
    public IDofusDbTableClient<DofusDbSkill> Skills() => new DofusDbTableClient<DofusDbSkill>(new Uri(baseAddress, "skills/"), referrer);

    public IDofusDbTableClient<DofusDbSpell> Spells() => new DofusDbTableClient<DofusDbSpell>(new Uri(baseAddress, "spells/"), referrer);
    public IDofusDbImageClient<long> SpellImages() => new DofusDbImageClient<long>(new Uri(baseAddress, "img/spells/"), ImageFormat.Png, "sort_", referrer);
    public IDofusDbTableClient<DofusDbSpellLevel> SpellLevels() => new DofusDbTableClient<DofusDbSpellLevel>(new Uri(baseAddress, "spell-levels/"), referrer);
    public IDofusDbTableClient<DofusDbSpellState> SpellStates() => new DofusDbTableClient<DofusDbSpellState>(new Uri(baseAddress, "spell-states/"), referrer);
    public IDofusDbImageClient<string> SpellStateImages() => new DofusDbImageClient<string>(new Uri(baseAddress, "img/states/"), ImageFormat.Png, referrer);
    public IDofusDbTableClient<DofusDbSpellVariant> SpellVariants() => new DofusDbTableClient<DofusDbSpellVariant>(new Uri(baseAddress, "spell-variants/"), referrer);

    public IDofusDbTableClient<DofusDbMonster> Monsters() => new DofusDbTableClient<DofusDbMonster>(new Uri(baseAddress, "monsters/"), referrer);
    public IDofusDbImageClient<long> MonsterImages() => new DofusDbImageClient<long>(new Uri(baseAddress, "img/monsters/"), ImageFormat.Png, referrer);
    public IDofusDbTableClient<DofusDbMonsterRace> MonsterRaces() => new DofusDbTableClient<DofusDbMonsterRace>(new Uri(baseAddress, "monster-races/"), referrer);
    public IDofusDbTableClient<DofusDbMonsterSuperRace> MonsterSuperRaces() => new DofusDbTableClient<DofusDbMonsterSuperRace>(new Uri(baseAddress, "monster-super-races/"), referrer);

    public IDofusDbTableClient<DofusDbMount> Mounts() => new DofusDbTableClient<DofusDbMount>(new Uri(baseAddress, "mounts/"), referrer);
    public IDofusDbTableClient<DofusDbMountFamily> MountFamilies() => new DofusDbTableClient<DofusDbMountFamily>(new Uri(baseAddress, "mount-families/"), referrer);
    public IDofusDbTableClient<DofusDbMountBehavior> MountBehaviors() => new DofusDbTableClient<DofusDbMountBehavior>(new Uri(baseAddress, "mount-behaviors/"), referrer);

    public IDofusDbTableClient<DofusDbWorld> Worlds() => new DofusDbTableClient<DofusDbWorld>(new Uri(baseAddress, "worlds/"), referrer);
    public IDofusDbTableClient<DofusDbSuperArea> SuperAreas() => new DofusDbTableClient<DofusDbSuperArea>(new Uri(baseAddress, "super-areas/"), referrer);
    public IDofusDbTableClient<DofusDbArea> Areas() => new DofusDbTableClient<DofusDbArea>(new Uri(baseAddress, "areas/"), referrer);
    public IDofusDbTableClient<DofusDbSubArea> SubAreas() => new DofusDbTableClient<DofusDbSubArea>(new Uri(baseAddress, "subareas/"), referrer);
    public IDofusDbTableClient<DofusDbMap> Maps() => new DofusDbTableClient<DofusDbMap>(new Uri(baseAddress, "maps/"), referrer);
    public IDofusDbScalableImageClient<long> MapImages() => new DofusDbScalableImageClient<long>(new Uri(baseAddress, "img/maps/"), ImageFormat.Jpeg, referrer);
    public IDofusDbTableClient<DofusDbMapPosition> MapPositions() => new DofusDbTableClient<DofusDbMapPosition>(new Uri(baseAddress, "map-positions/"), referrer);
    public IDofusDbTableClient<DofusDbDungeon> Dungeons() => new DofusDbTableClient<DofusDbDungeon>(new Uri(baseAddress, "dungeons/"), referrer);

    public IDofusDbTableClient<DofusDbAchievement> Achievements() => new DofusDbTableClient<DofusDbAchievement>(new Uri(baseAddress, "achievements/"), referrer);
    public IDofusDbImageClient<long> AchievementImages() => new DofusDbImageClient<long>(new Uri(baseAddress, "img/achievements/"), ImageFormat.Png, referrer);
    public IDofusDbTableClient<DofusDbAchievementObjective> AchievementObjectives() => new DofusDbTableClient<DofusDbAchievementObjective>(new Uri(baseAddress, "achievement-objectives/"), referrer);
    public IDofusDbTableClient<DofusDbAchievementReward> AchievementRewards() => new DofusDbTableClient<DofusDbAchievementReward>(new Uri(baseAddress, "achievement-rewards/"), referrer);
    public IDofusDbTableClient<DofusDbAchievementCategory> AchievementCategories() => new DofusDbTableClient<DofusDbAchievementCategory>(new Uri(baseAddress, "achievement-categories/"), referrer);

    public IDofusDbTableClient<DofusDbTitle> Titles() => new DofusDbTableClient<DofusDbTitle>(new Uri(baseAddress, "titles/"), referrer);
    public IDofusDbTableClient<DofusDbOrnament> Ornaments() => new DofusDbTableClient<DofusDbOrnament>(new Uri(baseAddress, "ornaments/"), referrer);

    public IDofusDbImageClient<long> OrnamentImages() => new DofusDbImageClient<long>(new Uri(baseAddress, "img/ornaments/"), ImageFormat.Png, referrer);
    // @formatter:max_line_length restore
}
