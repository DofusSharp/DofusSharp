using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Mounts;
using DofusSharp.DofusDb.ApiClients.Models.Npcs;
using DofusSharp.DofusDb.ApiClients.Models.Ornaments;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Models.Titles;

namespace DofusSharp.DofusDb.ApiClients;

public interface IDofusDbQueryProvider
{
    IDofusDbQuery<DofusDbAlmanaxCalendar> AlmanaxCalendars();
    IDofusDbQuery<DofusDbServer> Servers();
    IDofusDbQuery<DofusDbCharacteristic> Characteristics();
    IDofusDbQuery<DofusDbItem> Items();
    IDofusDbQuery<DofusDbItemType> ItemTypes();
    IDofusDbQuery<DofusDbItemSuperType> ItemSuperTypes();
    IDofusDbQuery<DofusDbItemSet> ItemSets();
    IDofusDbQuery<DofusDbJob> Jobs();
    IDofusDbQuery<DofusDbRecipe> Recipes();
    IDofusDbQuery<DofusDbSkill> Skills();
    IDofusDbQuery<DofusDbSpell> Spells();
    IDofusDbQuery<DofusDbSpellLevel> SpellLevels();
    IDofusDbQuery<DofusDbSpellState> SpellStates();
    IDofusDbQuery<DofusDbSpellVariant> SpellVariants();
    IDofusDbQuery<DofusDbMonster> Monsters();
    IDofusDbQuery<DofusDbMonsterRace> MonsterRaces();
    IDofusDbQuery<DofusDbMonsterSuperRace> MonsterSuperRaces();
    IDofusDbQuery<DofusDbMount> Mounts();
    IDofusDbQuery<DofusDbMountFamily> MountFamilies();
    IDofusDbQuery<DofusDbMountBehavior> MountBehaviors();
    IDofusDbQuery<DofusDbNpc> Npcs();
    IDofusDbQuery<DofusDbNpcMessage> NpcMessages();
    IDofusDbQuery<DofusDbWorld> Worlds();
    IDofusDbQuery<DofusDbSuperArea> SuperAreas();
    IDofusDbQuery<DofusDbArea> Areas();
    IDofusDbQuery<DofusDbSubArea> SubAreas();
    IDofusDbQuery<DofusDbMap> Maps();
    IDofusDbQuery<DofusDbMapPosition> MapPositions();
    IDofusDbQuery<DofusDbDungeon> Dungeons();
    IDofusDbQuery<DofusDbAchievement> Achievements();
    IDofusDbQuery<DofusDbAchievementObjective> AchievementObjectives();
    IDofusDbQuery<DofusDbAchievementReward> AchievementRewards();
    IDofusDbQuery<DofusDbAchievementCategory> AchievementCategories();
    IDofusDbQuery<DofusDbTitle> Titles();
    IDofusDbQuery<DofusDbOrnament> Ornaments();
}
