using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Models.Titles;

namespace DofusSharp.DofusDb.ApiClients;

public interface IDofusDbClientsFactory
{
    IDofusDbVersionClient Version();
    IDofusDbCriterionClient Criterion();
    IDofusDbTableClient<DofusDbServer> Servers();
    IDofusDbTableClient<DofusDbCharacteristic> Characteristics();
    IDofusDbTableClient<DofusDbItem> Items();
    IDofusDbImageClient<long> ItemImages();
    IDofusDbTableClient<DofusDbItemType> ItemTypes();
    IDofusDbTableClient<DofusDbItemSuperType> ItemSuperTypes();
    IDofusDbTableClient<DofusDbItemSet> ItemSets();
    IDofusDbTableClient<DofusDbJob> Jobs();
    IDofusDbImageClient<long> JobImages();
    IDofusDbTableClient<DofusDbRecipe> Recipes();
    IDofusDbTableClient<DofusDbSkill> Skills();
    IDofusDbTableClient<DofusDbSpell> Spells();
    IDofusDbImageClient<long> SpellImages();
    IDofusDbTableClient<DofusDbSpellLevel> SpellLevels();
    IDofusDbTableClient<DofusDbSpellState> SpellStates();
    IDofusDbImageClient<string> SpellStateImages();
    IDofusDbTableClient<DofusDbSpellVariant> SpellVariants();
    IDofusDbTableClient<DofusDbMonster> Monsters();
    IDofusDbImageClient<long> MonsterImages();
    IDofusDbTableClient<DofusDbMonsterRace> MonsterRaces();
    IDofusDbTableClient<DofusDbMonsterSuperRace> MonsterSuperRaces();
    IDofusDbTableClient<DofusDbWorld> Worlds();
    IDofusDbTableClient<DofusDbSuperArea> SuperAreas();
    IDofusDbTableClient<DofusDbArea> Areas();
    IDofusDbTableClient<DofusDbSubArea> SubAreas();
    IDofusDbTableClient<DofusDbMap> Maps();
    IDofusDbScalableImageClient<long> MapImages();
    IDofusDbTableClient<DofusDbMapPosition> MapPositions();
    IDofusDbTableClient<DofusDbDungeon> Dungeons();
    IDofusDbTableClient<DofusDbTitle> Titles();
    IDofusDbTableClient<DofusDbOrnament> Ornaments();
    IDofusDbImageClient<long> OrnamentImages();
}
