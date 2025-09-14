using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Models.Alignments;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;
using DofusSharp.DofusDb.ApiClients.Models.Alterations;
using DofusSharp.DofusDb.ApiClients.Models.Breeds;
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
///     Factory for creating DofusDb API clients.
/// </summary>
public interface IDofusDbClientsFactory
{
    // @formatter:off
    
    IDofusDbAlmanaxCalendarClient   Almanax();
    IDofusDbCriterionClient         Criterion();
    IDofusDbVersionClient           Version();

    IDofusDbTableClient<DofusDbAchievement>           Achievements();
    IDofusDbTableClient<DofusDbAchievementCategory>   AchievementCategories();
    IDofusDbTableClient<DofusDbAchievementObjective>  AchievementObjectives();
    IDofusDbTableClient<DofusDbAchievementReward>     AchievementRewards();
    IDofusDbTableClient<DofusDbAlmanaxCalendar>       AlmanaxCalendars();
    IDofusDbTableClient<DofusDbAlignmentRank>         AlignmentRanks();
    IDofusDbTableClient<DofusDbAlignmentSide>         AlignmentSides();
    IDofusDbTableClient<DofusDbAllianceRight>         AllianceRights();
    IDofusDbTableClient<DofusDbAlteration>            Alterations();
    IDofusDbTableClient<DofusDbArea>                  Areas();
    IDofusDbTableClient<DofusDbBreed>                 Breeds();
    IDofusDbTableClient<DofusDbCharacteristic>        Characteristics();
    IDofusDbTableClient<DofusDbDungeon>               Dungeons();
    IDofusDbTableClient<DofusDbGuildRight>            GuildRights();
    IDofusDbTableClient<DofusDbItem>                  Items();
    IDofusDbTableClient<DofusDbItemSet>               ItemSets();
    IDofusDbTableClient<DofusDbItemSuperType>         ItemSuperTypes();
    IDofusDbTableClient<DofusDbItemType>              ItemTypes();
    IDofusDbTableClient<DofusDbJob>                   Jobs();
    IDofusDbTableClient<DofusDbMap>                   Maps();
    IDofusDbTableClient<DofusDbMapPosition>           MapPositions();
    IDofusDbTableClient<DofusDbMonster>               Monsters();
    IDofusDbTableClient<DofusDbMonsterRace>           MonsterRaces();
    IDofusDbTableClient<DofusDbMonsterSuperRace>      MonsterSuperRaces();
    IDofusDbTableClient<DofusDbMount>                 Mounts();
    IDofusDbTableClient<DofusDbMountBehavior>         MountBehaviors();
    IDofusDbTableClient<DofusDbMountFamily>           MountFamilies();
    IDofusDbTableClient<DofusDbNpc>                   Npcs();
    IDofusDbTableClient<DofusDbNpcMessage>            NpcMessages();
    IDofusDbTableClient<DofusDbOrnament>              Ornaments();
    IDofusDbTableClient<DofusDbRecipe>                Recipes();
    IDofusDbTableClient<DofusDbServer>                Servers();
    IDofusDbTableClient<DofusDbSkill>                 Skills();
    IDofusDbTableClient<DofusDbSpell>                 Spells();
    IDofusDbTableClient<DofusDbSpellLevel>            SpellLevels();
    IDofusDbTableClient<DofusDbSpellState>            SpellStates();
    IDofusDbTableClient<DofusDbSpellVariant>          SpellVariants();
    IDofusDbTableClient<DofusDbSubArea>               SubAreas();
    IDofusDbTableClient<DofusDbSuperArea>             SuperAreas();
    IDofusDbTableClient<DofusDbTitle>                 Titles();
    IDofusDbTableClient<DofusDbWorld>                 Worlds();

    IDofusDbImageClient<long>     AchievementImages();
    IDofusDbImageClient<long>     ItemImages();
    IDofusDbImageClient<long>     JobImages();
    IDofusDbImageClient<long>     MonsterImages();
    IDofusDbImageClient<long>     OrnamentImages();
    IDofusDbImageClient<long>     SpellImages();
    IDofusDbImageClient<string>   SpellStateImages();

    IDofusDbScalableImageClient<long>   MapImages();
    
    // @formatter:on
}
