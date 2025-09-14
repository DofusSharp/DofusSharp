using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Mounts;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Models.Titles;

namespace DofusSharp.DofusDb.ApiClients;

public interface IDofusDbClientsFactory
{
    IDofusDbTableClient<DofusDbArea> Areas();
    IDofusDbTableClient<DofusDbCharacteristic> Characteristics();
    IDofusDbCriterionClient Criterion();
    IDofusDbTableClient<DofusDbDungeon> Dungeons();
    IDofusDbImageClient<long> ItemImages();
    IDofusDbTableClient<DofusDbItemSet> ItemSets();
    IDofusDbTableClient<DofusDbItemSuperType> ItemSuperTypes();
    IDofusDbTableClient<DofusDbItemType> ItemTypes();
    IDofusDbTableClient<DofusDbItem> Items();
    IDofusDbImageClient<long> JobImages();
    IDofusDbTableClient<DofusDbJob> Jobs();
    IDofusDbScalableImageClient<long> MapImages();
    IDofusDbTableClient<DofusDbMapPosition> MapPositions();
    IDofusDbTableClient<DofusDbMap> Maps();
    IDofusDbImageClient<long> MonsterImages();
    IDofusDbTableClient<DofusDbMonsterRace> MonsterRaces();
    IDofusDbTableClient<DofusDbMonsterSuperRace> MonsterSuperRaces();
    IDofusDbTableClient<DofusDbMonster> Monsters();
    IDofusDbTableClient<DofusDbMountBehavior> MountBehaviors();
    IDofusDbTableClient<DofusDbMountFamily> MountFamilies();
    IDofusDbTableClient<DofusDbMount> Mounts();
    IDofusDbImageClient<long> OrnamentImages();
    IDofusDbTableClient<DofusDbOrnament> Ornaments();
    IDofusDbTableClient<DofusDbRecipe> Recipes();
    IDofusDbTableClient<DofusDbServer> Servers();
    IDofusDbTableClient<DofusDbSkill> Skills();
    IDofusDbImageClient<long> SpellImages();
    IDofusDbTableClient<DofusDbSpellLevel> SpellLevels();
    IDofusDbImageClient<string> SpellStateImages();
    IDofusDbTableClient<DofusDbSpellState> SpellStates();
    IDofusDbTableClient<DofusDbSpellVariant> SpellVariants();
    IDofusDbTableClient<DofusDbSpell> Spells();
    IDofusDbTableClient<DofusDbSubArea> SubAreas();
    IDofusDbTableClient<DofusDbSuperArea> SuperAreas();
    IDofusDbTableClient<DofusDbTitle> Titles();
    IDofusDbVersionClient Version();
    IDofusDbTableClient<DofusDbWorld> Worlds();
}
