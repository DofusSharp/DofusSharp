using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Models.Alignments;
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
using DofusSharp.DofusDb.ApiClients.Models.Social;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Models.Titles;

namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     Factory for creating DofusDb query objects.
/// </summary>
public interface IDofusDbQueryProvider
{
    // @formatter:off
    
    IDofusDbQuery<DofusDbAchievement>           Achievements();
    IDofusDbQuery<DofusDbAchievementCategory>   AchievementCategories();
    IDofusDbQuery<DofusDbAchievementObjective>  AchievementObjectives();
    IDofusDbQuery<DofusDbAchievementReward>     AchievementRewards();
    IDofusDbQuery<DofusDbAlmanaxCalendar>       AlmanaxCalendars();
    IDofusDbQuery<DofusDbAllianceRight>         AllianceRights();
    IDofusDbQuery<DofusDbAlignmentRank>         AlignmentRanks();
    IDofusDbQuery<DofusDbAlignmentSide>         AlignmentSides();
    IDofusDbQuery<DofusDbArea>                  Areas();
    IDofusDbQuery<DofusDbCharacteristic>        Characteristics();
    IDofusDbQuery<DofusDbDungeon>               Dungeons();
    IDofusDbQuery<DofusDbGuildRight>            GuildRights();
    IDofusDbQuery<DofusDbItem>                  Items();
    IDofusDbQuery<DofusDbItemSet>               ItemSets();
    IDofusDbQuery<DofusDbItemSuperType>         ItemSuperTypes();
    IDofusDbQuery<DofusDbItemType>              ItemTypes();
    IDofusDbQuery<DofusDbJob>                   Jobs();
    IDofusDbQuery<DofusDbMap>                   Maps();
    IDofusDbQuery<DofusDbMapPosition>           MapPositions();
    IDofusDbQuery<DofusDbMonster>               Monsters();
    IDofusDbQuery<DofusDbMonsterRace>           MonsterRaces();
    IDofusDbQuery<DofusDbMonsterSuperRace>      MonsterSuperRaces();
    IDofusDbQuery<DofusDbMount>                 Mounts();
    IDofusDbQuery<DofusDbMountBehavior>         MountBehaviors();
    IDofusDbQuery<DofusDbMountFamily>           MountFamilies();
    IDofusDbQuery<DofusDbNpc>                   Npcs();
    IDofusDbQuery<DofusDbNpcMessage>            NpcMessages();
    IDofusDbQuery<DofusDbOrnament>              Ornaments();
    IDofusDbQuery<DofusDbRecipe>                Recipes();
    IDofusDbQuery<DofusDbServer>                Servers();
    IDofusDbQuery<DofusDbSkill>                 Skills();
    IDofusDbQuery<DofusDbSpell>                 Spells();
    IDofusDbQuery<DofusDbSpellLevel>            SpellLevels();
    IDofusDbQuery<DofusDbSpellState>            SpellStates();
    IDofusDbQuery<DofusDbSpellVariant>          SpellVariants();
    IDofusDbQuery<DofusDbSubArea>               SubAreas();
    IDofusDbQuery<DofusDbSuperArea>             SuperAreas();
    IDofusDbQuery<DofusDbTitle>                 Titles();
    IDofusDbQuery<DofusDbWorld>                 Worlds();
    
    // @formatter:on
}
