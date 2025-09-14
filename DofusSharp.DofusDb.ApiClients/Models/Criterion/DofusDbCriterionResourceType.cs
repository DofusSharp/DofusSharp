using DofusSharp.DofusDb.ApiClients.Models.Maps;

namespace DofusSharp.DofusDb.ApiClients.Models.Criterion;

/// <summary>
///     Resource types available in <see cref="DofusDbCriterionResource" />.
/// </summary>
/// <remarks>
///     Some of the values do not correspond to the <c>className</c> discriminator of type <see cref="DofusDbResource" />, e.g. the resource class name <c>AlignmentSides</c>
///     corresponds to the type <c>alignments</c>.
/// </remarks>
public enum DofusDbCriterionResourceType
{
    AchievementCategories,
    AchievementObjectives,
    AchievementRewards,
    Achievements,
    AlmanaxCalendars,
    AlignmentRanks,
    /// <remarks>
    ///     Corresponds to resource type AlignmentSide.
    /// </remarks>
    Alignments,
    AllianceRights,
    Alterations,
    Areas,
    Breeds,
    Challenges,
    Characteristics,
    Collectables,
    Companions,
    CustomModeBreedSpells,
    Documents,
    Dungeons,
    Effects,
    Emoticons,
    FeatureDescriptions,
    FinishMoves,
    GuildRights,
    HavenbagFurnitures,
    HavenbagThemes,
    Hints,
    InfoMessages,
    Interactives,
    ItemSets,
    Items,
    ItemSuperTypes,
    ItemTypes,
    Jobs,
    LegendaryPowerCategories,
    LegendaryTreasureHunts,
    LivingObjectSkinJntMood,
    /// <remarks>
    ///     Corresponds to resource type <see cref="DofusDbMapPosition" />.
    /// </remarks>
    Positions,
    MapReferences,
    Maps,
    Modsters,
    Monsters,
    MonsterRaces,
    MonsterSuperRaces,
    Months,
    MountBehaviors,
    MountFamilies,
    Mounts,
    NpcMessages,
    Npcs,
    Ornaments,
    PointOfInterest,
    QuestCategories,
    QuestObjectiveTypes,
    QuestObjectives,
    QuestStepRewards,
    QuestSteps,
    Quests,
    RandomDropGroups,
    Recipes,
    ServerSeasons,
    Servers,
    ServerGameTypes,
    Skills,
    SmileyPacks,
    Spells,
    SpellLevels,
    SpellPairs,
    SpellStates,
    SpellVariants,
    Subareas,
    SuperAreas,
    Titles,
    WorldEvents,
    WorldEventRewards,
    Worlds
}

public static class DofusDbCriterionResourceTypeExtensions
{
    /// <summary>
    ///     Creates an <see cref="IDofusDbTableClient" /> for fetching resources of the specified type.
    /// </summary>
    /// <param name="clientsFactory">The factory used to build the client.</param>
    /// <param name="type">The type of resource to fetch.</param>
    /// <returns>A client capable of fetching resources of the given type.</returns>
    public static IDofusDbTableClient CreateClientForCriterionResourceOfType(this IDofusDbClientsFactory clientsFactory, DofusDbCriterionResourceType type) =>
        type switch
        {
            // @formatter:off
            
            DofusDbCriterionResourceType.AchievementCategories    => clientsFactory.AchievementCategories(),
            DofusDbCriterionResourceType.AchievementObjectives    => clientsFactory.AchievementObjectives(),
            DofusDbCriterionResourceType.AchievementRewards       => clientsFactory.AchievementRewards(),
            DofusDbCriterionResourceType.Achievements             => clientsFactory.Achievements(),
            DofusDbCriterionResourceType.AlmanaxCalendars         => clientsFactory.AlmanaxCalendars(),
            DofusDbCriterionResourceType.AlignmentRanks           => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Alignments               => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.AllianceRights           => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Alterations              => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Areas                    => clientsFactory.Areas(),
            DofusDbCriterionResourceType.Breeds                   => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Challenges               => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Characteristics          => clientsFactory.Characteristics(),
            DofusDbCriterionResourceType.Collectables             => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Companions               => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.CustomModeBreedSpells    => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Documents                => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Dungeons                 => clientsFactory.Dungeons(),
            DofusDbCriterionResourceType.Effects                  => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Emoticons                => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.FeatureDescriptions      => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.FinishMoves              => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.GuildRights              => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.HavenbagFurnitures       => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.HavenbagThemes           => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Hints                    => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.InfoMessages             => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Interactives             => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.ItemSets                 => clientsFactory.ItemSets(),
            DofusDbCriterionResourceType.Items                    => clientsFactory.Items(),
            DofusDbCriterionResourceType.ItemSuperTypes           => clientsFactory.ItemSuperTypes(),
            DofusDbCriterionResourceType.ItemTypes                => clientsFactory.ItemTypes(),
            DofusDbCriterionResourceType.Jobs                     => clientsFactory.Jobs(),
            DofusDbCriterionResourceType.LegendaryPowerCategories => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.LegendaryTreasureHunts   => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.LivingObjectSkinJntMood  => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.MapReferences            => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Maps                     => clientsFactory.Maps(),
            DofusDbCriterionResourceType.Modsters                 => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.MonsterRaces             => clientsFactory.MonsterRaces(),
            DofusDbCriterionResourceType.MonsterSuperRaces        => clientsFactory.MonsterSuperRaces(),
            DofusDbCriterionResourceType.Monsters                 => clientsFactory.Monsters(),
            DofusDbCriterionResourceType.Months                   => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.MountBehaviors           => clientsFactory.MountBehaviors(),
            DofusDbCriterionResourceType.MountFamilies            => clientsFactory.MountFamilies(),
            DofusDbCriterionResourceType.Mounts                   => clientsFactory.Mounts(),
            DofusDbCriterionResourceType.NpcMessages              => clientsFactory.NpcMessages(),
            DofusDbCriterionResourceType.Npcs                     => clientsFactory.Npcs(),
            DofusDbCriterionResourceType.Ornaments                => clientsFactory.Ornaments(),
            DofusDbCriterionResourceType.PointOfInterest          => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Positions                => clientsFactory.MapPositions(),
            DofusDbCriterionResourceType.QuestCategories          => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.QuestObjectiveTypes      => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.QuestObjectives          => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.QuestStepRewards         => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.QuestSteps               => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Quests                   => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.RandomDropGroups         => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Recipes                  => clientsFactory.Recipes(),
            DofusDbCriterionResourceType.ServerGameTypes          => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.ServerSeasons            => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Servers                  => clientsFactory.Servers(),
            DofusDbCriterionResourceType.Skills                   => clientsFactory.Skills(),
            DofusDbCriterionResourceType.SmileyPacks              => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.SpellLevels              => clientsFactory.SpellLevels(),
            DofusDbCriterionResourceType.SpellPairs               => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.SpellStates              => clientsFactory.SpellStates(),
            DofusDbCriterionResourceType.SpellVariants            => clientsFactory.SpellVariants(),
            DofusDbCriterionResourceType.Spells                   => clientsFactory.Spells(),
            DofusDbCriterionResourceType.Subareas                 => clientsFactory.SubAreas(),
            DofusDbCriterionResourceType.SuperAreas               => clientsFactory.SuperAreas(),
            DofusDbCriterionResourceType.Titles                   => clientsFactory.Titles(),
            DofusDbCriterionResourceType.WorldEventRewards        => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.WorldEvents              => throw new NotImplementedException("Not implemented yet."),
            DofusDbCriterionResourceType.Worlds                   => clientsFactory.Worlds(),
            _                                                     => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            
            // @formatter:on
        };
}
