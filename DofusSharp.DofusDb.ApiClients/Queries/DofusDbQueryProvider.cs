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

namespace DofusSharp.DofusDb.ApiClients.Queries;

class DofusDbQueryProvider(IDofusDbClientsFactory factory) : IDofusDbQueryProvider
{
    // @formatter:off
    
    public IDofusDbQuery<DofusDbAchievement>           Achievements()          => new DofusDbQuery<DofusDbAchievement>(factory.Achievements());
    public IDofusDbQuery<DofusDbAchievementCategory>   AchievementCategories() => new DofusDbQuery<DofusDbAchievementCategory>(factory.AchievementCategories());
    public IDofusDbQuery<DofusDbAchievementObjective>  AchievementObjectives() => new DofusDbQuery<DofusDbAchievementObjective>(factory.AchievementObjectives());
    public IDofusDbQuery<DofusDbAchievementReward>     AchievementRewards()    => new DofusDbQuery<DofusDbAchievementReward>(factory.AchievementRewards());
    public IDofusDbQuery<DofusDbAlmanaxCalendar>       AlmanaxCalendars()      => new DofusDbQuery<DofusDbAlmanaxCalendar>(factory.AlmanaxCalendars());
    public IDofusDbQuery<DofusDbAllianceRight>         AllianceRights()        => new DofusDbQuery<DofusDbAllianceRight>(factory.AllianceRights());
    public IDofusDbQuery<DofusDbAlignmentRank>         AlignmentRanks()        => new DofusDbQuery<DofusDbAlignmentRank>(factory.AlignmentRanks());
    public IDofusDbQuery<DofusDbAlignmentSide>         AlignmentSides()        => new DofusDbQuery<DofusDbAlignmentSide>(factory.AlignmentSides());
    public IDofusDbQuery<DofusDbArea>                  Areas()                 => new DofusDbQuery<DofusDbArea>(factory.Areas());
    public IDofusDbQuery<DofusDbCharacteristic>        Characteristics()       => new DofusDbQuery<DofusDbCharacteristic>(factory.Characteristics());
    public IDofusDbQuery<DofusDbDungeon>               Dungeons()              => new DofusDbQuery<DofusDbDungeon>(factory.Dungeons());
    public IDofusDbQuery<DofusDbGuildRight>            GuildRights()           => new DofusDbQuery<DofusDbGuildRight>(factory.GuildRights());
    public IDofusDbQuery<DofusDbItem>                  Items()                 => new DofusDbQuery<DofusDbItem>(factory.Items());
    public IDofusDbQuery<DofusDbItemSet>               ItemSets()              => new DofusDbQuery<DofusDbItemSet>(factory.ItemSets());
    public IDofusDbQuery<DofusDbItemSuperType>         ItemSuperTypes()        => new DofusDbQuery<DofusDbItemSuperType>(factory.ItemSuperTypes());
    public IDofusDbQuery<DofusDbItemType>              ItemTypes()             => new DofusDbQuery<DofusDbItemType>(factory.ItemTypes());
    public IDofusDbQuery<DofusDbJob>                   Jobs()                  => new DofusDbQuery<DofusDbJob>(factory.Jobs());
    public IDofusDbQuery<DofusDbMap>                   Maps()                  => new DofusDbQuery<DofusDbMap>(factory.Maps());
    public IDofusDbQuery<DofusDbMapPosition>           MapPositions()          => new DofusDbQuery<DofusDbMapPosition>(factory.MapPositions());
    public IDofusDbQuery<DofusDbMonster>               Monsters()              => new DofusDbQuery<DofusDbMonster>(factory.Monsters());
    public IDofusDbQuery<DofusDbMonsterRace>           MonsterRaces()          => new DofusDbQuery<DofusDbMonsterRace>(factory.MonsterRaces());
    public IDofusDbQuery<DofusDbMonsterSuperRace>      MonsterSuperRaces()     => new DofusDbQuery<DofusDbMonsterSuperRace>(factory.MonsterSuperRaces());
    public IDofusDbQuery<DofusDbMount>                 Mounts()                => new DofusDbQuery<DofusDbMount>(factory.Mounts());
    public IDofusDbQuery<DofusDbMountBehavior>         MountBehaviors()        => new DofusDbQuery<DofusDbMountBehavior>(factory.MountBehaviors());
    public IDofusDbQuery<DofusDbMountFamily>           MountFamilies()         => new DofusDbQuery<DofusDbMountFamily>(factory.MountFamilies());
    public IDofusDbQuery<DofusDbNpc>                   Npcs()                  => new DofusDbQuery<DofusDbNpc>(factory.Npcs());
    public IDofusDbQuery<DofusDbNpcMessage>            NpcMessages()           => new DofusDbQuery<DofusDbNpcMessage>(factory.NpcMessages());
    public IDofusDbQuery<DofusDbOrnament>              Ornaments()             => new DofusDbQuery<DofusDbOrnament>(factory.Ornaments());
    public IDofusDbQuery<DofusDbRecipe>                Recipes()               => new DofusDbQuery<DofusDbRecipe>(factory.Recipes());
    public IDofusDbQuery<DofusDbServer>                Servers()               => new DofusDbQuery<DofusDbServer>(factory.Servers());
    public IDofusDbQuery<DofusDbSkill>                 Skills()                => new DofusDbQuery<DofusDbSkill>(factory.Skills());
    public IDofusDbQuery<DofusDbSpell>                 Spells()                => new DofusDbQuery<DofusDbSpell>(factory.Spells());
    public IDofusDbQuery<DofusDbSpellLevel>            SpellLevels()           => new DofusDbQuery<DofusDbSpellLevel>(factory.SpellLevels());
    public IDofusDbQuery<DofusDbSpellState>            SpellStates()           => new DofusDbQuery<DofusDbSpellState>(factory.SpellStates());
    public IDofusDbQuery<DofusDbSpellVariant>          SpellVariants()         => new DofusDbQuery<DofusDbSpellVariant>(factory.SpellVariants());
    public IDofusDbQuery<DofusDbSubArea>               SubAreas()              => new DofusDbQuery<DofusDbSubArea>(factory.SubAreas());
    public IDofusDbQuery<DofusDbSuperArea>             SuperAreas()            => new DofusDbQuery<DofusDbSuperArea>(factory.SuperAreas());
    public IDofusDbQuery<DofusDbTitle>                 Titles()                => new DofusDbQuery<DofusDbTitle>(factory.Titles());
    public IDofusDbQuery<DofusDbWorld>                 Worlds()                => new DofusDbQuery<DofusDbWorld>(factory.Worlds());
    
    // @formatter:on
}
