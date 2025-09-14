using System.Text.Json;
using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Models.Alignments;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;
using DofusSharp.DofusDb.ApiClients.Models.Alterations;
using DofusSharp.DofusDb.ApiClients.Models.Breeds;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Common;
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

namespace DofusSharp.DofusDb.ApiClients.Clients;

/// <inheritdoc />
/// <param name="baseAddress">The base URL of the API to query.</param>
/// <param name="referrer">The referer header to include in requests to the API.</param>
class DofusDbClientsFactory(Uri baseAddress, Uri? referrer, JsonSerializerOptions options) : IDofusDbClientsFactory
{
    // @formatter:off
    
    public IDofusDbAlmanaxCalendarClient                     Almanax()                => new DofusDbAlmanaxClient                             (new Uri(baseAddress, "almanax/"),                 referrer,            options);
    public IDofusDbCriterionClient                           Criterion()              => new DofusDbCriterionClient                           (new Uri(baseAddress, "criterion/"),               referrer,            options);
    public IDofusDbVersionClient                             Version()                => new DofusDbVersionClient                             (new Uri(baseAddress, "version/"),                 referrer);
    
    public IDofusDbTableClient<DofusDbAchievement>           Achievements()           => new DofusDbTableClient<DofusDbAchievement>           (new Uri(baseAddress, "achievements/"),            referrer,            options);
    public IDofusDbTableClient<DofusDbAchievementCategory>   AchievementCategories()  => new DofusDbTableClient<DofusDbAchievementCategory>   (new Uri(baseAddress, "achievement-categories/"),  referrer,            options);
    public IDofusDbTableClient<DofusDbAchievementObjective>  AchievementObjectives()  => new DofusDbTableClient<DofusDbAchievementObjective>  (new Uri(baseAddress, "achievement-objectives/"),  referrer,            options);
    public IDofusDbTableClient<DofusDbAchievementReward>     AchievementRewards()     => new DofusDbTableClient<DofusDbAchievementReward>     (new Uri(baseAddress, "achievement-rewards/"),     referrer,            options);
    public IDofusDbTableClient<DofusDbAlignmentRank>         AlignmentRanks()         => new DofusDbTableClient<DofusDbAlignmentRank>         (new Uri(baseAddress, "alignment-ranks/"),         referrer,            options);
    public IDofusDbTableClient<DofusDbAlignmentSide>         AlignmentSides()         => new DofusDbTableClient<DofusDbAlignmentSide>         (new Uri(baseAddress, "alignment-sides/"),         referrer,            options);
    public IDofusDbTableClient<DofusDbAllianceRight>         AllianceRights()         => new DofusDbTableClient<DofusDbAllianceRight>         (new Uri(baseAddress, "alliance-rights/"),         referrer,            options);
    public IDofusDbTableClient<DofusDbAlmanaxCalendar>       AlmanaxCalendars()       => new DofusDbTableClient<DofusDbAlmanaxCalendar>       (new Uri(baseAddress, "almanax-calendars/"),       referrer,            options);
    public IDofusDbTableClient<DofusDbAlteration>            Alterations()            => new DofusDbTableClient<DofusDbAlteration>            (new Uri(baseAddress, "alterations/"),             referrer,            options);
    public IDofusDbTableClient<DofusDbArea>                  Areas()                  => new DofusDbTableClient<DofusDbArea>                  (new Uri(baseAddress, "areas/"),                   referrer,            options);
    public IDofusDbTableClient<DofusDbBreed>                 Breeds()                 => new DofusDbTableClient<DofusDbBreed>                 (new Uri(baseAddress, "breeds/"),                  referrer,            options);
    public IDofusDbTableClient<DofusDbCharacteristic>        Characteristics()        => new DofusDbTableClient<DofusDbCharacteristic>        (new Uri(baseAddress, "characteristics/"),         referrer,            options);
    public IDofusDbTableClient<DofusDbDungeon>               Dungeons()               => new DofusDbTableClient<DofusDbDungeon>               (new Uri(baseAddress, "dungeons/"),                referrer,            options);
    public IDofusDbTableClient<DofusDbGuildRight>            GuildRights()            => new DofusDbTableClient<DofusDbGuildRight>            (new Uri(baseAddress, "guild-rights/"),            referrer,            options);
    public IDofusDbTableClient<DofusDbItem>                  Items()                  => new DofusDbTableClient<DofusDbItem>                  (new Uri(baseAddress, "items/"),                   referrer,            options);
    public IDofusDbTableClient<DofusDbItemSet>               ItemSets()               => new DofusDbTableClient<DofusDbItemSet>               (new Uri(baseAddress, "item-sets/"),               referrer,            options);
    public IDofusDbTableClient<DofusDbItemSuperType>         ItemSuperTypes()         => new DofusDbTableClient<DofusDbItemSuperType>         (new Uri(baseAddress, "item-super-types/"),        referrer,            options);
    public IDofusDbTableClient<DofusDbItemType>              ItemTypes()              => new DofusDbTableClient<DofusDbItemType>              (new Uri(baseAddress, "item-types/"),              referrer,            options);
    public IDofusDbTableClient<DofusDbJob>                   Jobs()                   => new DofusDbTableClient<DofusDbJob>                   (new Uri(baseAddress, "jobs/"),                    referrer,            options);
    public IDofusDbTableClient<DofusDbMap>                   Maps()                   => new DofusDbTableClient<DofusDbMap>                   (new Uri(baseAddress, "maps/"),                    referrer,            options);
    public IDofusDbTableClient<DofusDbMapPosition>           MapPositions()           => new DofusDbTableClient<DofusDbMapPosition>           (new Uri(baseAddress, "map-positions/"),           referrer,            options);
    public IDofusDbTableClient<DofusDbMonster>               Monsters()               => new DofusDbTableClient<DofusDbMonster>               (new Uri(baseAddress, "monsters/"),                referrer,            options);
    public IDofusDbTableClient<DofusDbMonsterRace>           MonsterRaces()           => new DofusDbTableClient<DofusDbMonsterRace>           (new Uri(baseAddress, "monster-races/"),           referrer,            options);
    public IDofusDbTableClient<DofusDbMonsterSuperRace>      MonsterSuperRaces()      => new DofusDbTableClient<DofusDbMonsterSuperRace>      (new Uri(baseAddress, "monster-super-races/"),     referrer,            options);
    public IDofusDbTableClient<DofusDbMount>                 Mounts()                 => new DofusDbTableClient<DofusDbMount>                 (new Uri(baseAddress, "mounts/"),                  referrer,            options);
    public IDofusDbTableClient<DofusDbMountBehavior>         MountBehaviors()         => new DofusDbTableClient<DofusDbMountBehavior>         (new Uri(baseAddress, "mount-behaviors/"),         referrer,            options);
    public IDofusDbTableClient<DofusDbMountFamily>           MountFamilies()          => new DofusDbTableClient<DofusDbMountFamily>           (new Uri(baseAddress, "mount-families/"),          referrer,            options);
    public IDofusDbTableClient<DofusDbNpc>                   Npcs()                   => new DofusDbTableClient<DofusDbNpc>                   (new Uri(baseAddress, "npcs/"),                    referrer,            options);
    public IDofusDbTableClient<DofusDbNpcMessage>            NpcMessages()            => new DofusDbTableClient<DofusDbNpcMessage>            (new Uri(baseAddress, "npc-messages/"),            referrer,            options);
    public IDofusDbTableClient<DofusDbOrnament>              Ornaments()              => new DofusDbTableClient<DofusDbOrnament>              (new Uri(baseAddress, "ornaments/"),               referrer,            options);
    public IDofusDbTableClient<DofusDbRecipe>                Recipes()                => new DofusDbTableClient<DofusDbRecipe>                (new Uri(baseAddress, "recipes/"),                 referrer,            options);
    public IDofusDbTableClient<DofusDbServer>                Servers()                => new DofusDbTableClient<DofusDbServer>                (new Uri(baseAddress, "servers/"),                 referrer,            options);
    public IDofusDbTableClient<DofusDbSkill>                 Skills()                 => new DofusDbTableClient<DofusDbSkill>                 (new Uri(baseAddress, "skills/"),                  referrer,            options);
    public IDofusDbTableClient<DofusDbSpell>                 Spells()                 => new DofusDbTableClient<DofusDbSpell>                 (new Uri(baseAddress, "spells/"),                  referrer,            options);
    public IDofusDbTableClient<DofusDbSpellLevel>            SpellLevels()            => new DofusDbTableClient<DofusDbSpellLevel>            (new Uri(baseAddress, "spell-levels/"),            referrer,            options);
    public IDofusDbTableClient<DofusDbSpellState>            SpellStates()            => new DofusDbTableClient<DofusDbSpellState>            (new Uri(baseAddress, "spell-states/"),            referrer,            options);
    public IDofusDbTableClient<DofusDbSpellVariant>          SpellVariants()          => new DofusDbTableClient<DofusDbSpellVariant>          (new Uri(baseAddress, "spell-variants/"),          referrer,            options);
    public IDofusDbTableClient<DofusDbSubArea>               SubAreas()               => new DofusDbTableClient<DofusDbSubArea>               (new Uri(baseAddress, "subareas/"),                referrer,            options);
    public IDofusDbTableClient<DofusDbSuperArea>             SuperAreas()             => new DofusDbTableClient<DofusDbSuperArea>             (new Uri(baseAddress, "super-areas/"),             referrer,            options);
    public IDofusDbTableClient<DofusDbTitle>                 Titles()                 => new DofusDbTableClient<DofusDbTitle>                 (new Uri(baseAddress, "titles/"),                  referrer,            options);
    public IDofusDbTableClient<DofusDbWorld>                 Worlds()                 => new DofusDbTableClient<DofusDbWorld>                 (new Uri(baseAddress, "worlds/"),                  referrer,            options);
    
    public IDofusDbImagesClient<long>                         AchievementImages()      => new DofusDbImagesClient<long>                         (new Uri(baseAddress, "img/achievements/"),        ImageFormat.Png,     referrer);
    public IDofusDbBreedImagesClient                         BreedImages()            => new DofusDbBreedImagesClient                         (new Uri(baseAddress, "img/"),                     referrer);
    public IDofusDbImagesClient<long>                         ItemImages()             => new DofusDbImagesClient<long>                         (new Uri(baseAddress, "img/items/"),               ImageFormat.Png,     referrer);
    public IDofusDbImagesClient<long>                         JobImages()              => new DofusDbImagesClient<long>                         (new Uri(baseAddress, "img/jobs/"),                ImageFormat.Jpeg,    referrer);
    public IDofusDbImagesClient<long>                         MonsterImages()          => new DofusDbImagesClient<long>                         (new Uri(baseAddress, "img/monsters/"),            ImageFormat.Png,     referrer);
    public IDofusDbImagesClient<long>                         OrnamentImages()         => new DofusDbImagesClient<long>                         (new Uri(baseAddress, "img/ornaments/"),           ImageFormat.Png,     referrer);
    public IDofusDbImagesClient<long>                         SpellImages()            => new DofusDbImagesClient<long>                         (new Uri(baseAddress, "img/spells/"),              ImageFormat.Png,     referrer, "sort_");
    public IDofusDbImagesClient<string>                       SpellStateImages()       => new DofusDbImagesClient<string>                       (new Uri(baseAddress, "img/states/"),              ImageFormat.Png,     referrer);
    
    public IDofusDbScalableImagesClient<long>                 MapImages()              => new DofusDbScalableImagesClient<long>                 (new Uri(baseAddress, "img/maps/"),                ImageFormat.Jpeg,    referrer);

    // @formatter:on
}
