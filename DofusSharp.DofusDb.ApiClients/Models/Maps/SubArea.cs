using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     A sub area within an area.
/// </summary>
public class SubArea : DofusDbEntity
{
    /// <summary>
    ///     The unique identifier of the area that the sub area belongs to.
    /// </summary>
    public int? AreaId { get; init; }

    /// <summary>
    ///     The unique identifiers of the maps in the sub area.
    /// </summary>
    public IReadOnlyCollection<long>? MapIds { get; init; }

    /// <summary>
    ///     The bounds of the sub area.
    /// </summary>
    public Bounds? Bounds { get; init; }

    /// <summary>
    ///     The shape definition of the sub area.
    /// </summary>
    public IReadOnlyCollection<int>? Shape { get; init; }

    /// <summary>
    ///     The unique identifiers of custom world maps for the sub area.
    /// </summary>
    public IReadOnlyCollection<int>? CustomWorldMap { get; init; }

    /// <summary>
    ///     The unique identifier of the pack associated with the sub area.
    /// </summary>
    public int? PackId { get; init; }

    /// <summary>
    ///     The recommended level for the sub area.
    /// </summary>
    public int? Level { get; init; }

    /// <summary>
    ///     Whether the sub area is a conquest village.
    /// </summary>
    public bool? IsConquestVillage { get; init; }

    /// <summary>
    ///     Whether basic accounts are allowed in the sub area.
    /// </summary>
    public bool? BasicAccountAllowed { get; init; }

    /// <summary>
    ///     Whether the sub area is displayed on the world map.
    /// </summary>
    public bool? DisplayOnWorldMap { get; init; }

    /// <summary>
    ///     Whether mount auto trip is allowed in the sub area.
    /// </summary>
    public bool? MountAutoTripAllowed { get; init; }

    /// <summary>
    ///     Whether psi is allowed in the sub area.
    /// </summary>
    public bool? PsiAllowed { get; init; }

    /// <summary>
    ///     The unique identifiers of monsters present in the sub area.
    /// </summary>
    public IReadOnlyCollection<int>? Monsters { get; init; }

    /// <summary>
    ///     The unique identifiers of entrance maps for the sub area.
    /// </summary>
    public IReadOnlyCollection<int>? EntranceMapIds { get; init; }

    /// <summary>
    ///     The unique identifiers of exit maps for the sub area.
    /// </summary>
    public IReadOnlyCollection<int>? ExitMapIds { get; init; }

    /// <summary>
    ///     Whether the sub area is capturable.
    /// </summary>
    public bool? Capturable { get; init; }

    /// <summary>
    ///     The unique identifiers of achievements for the sub area.
    /// </summary>
    public IReadOnlyCollection<int>? Achievements { get; init; }

    /// <summary>
    ///     The unique identifier of the explore achievement for the sub area.
    /// </summary>
    public int? ExploreAchievementId { get; init; }

    /// <summary>
    ///     The unique identifiers of harvestables for the sub area.
    /// </summary>
    public IReadOnlyCollection<int>? Harvestables { get; init; }

    /// <summary>
    ///     The unique identifier of the associated zaap map for the sub area.
    /// </summary>
    public int? AssociatedZaapMapId { get; init; }

    /// <summary>
    ///     The unique identifiers of neighboring sub areas.
    /// </summary>
    public IReadOnlyCollection<int>? Neighbors { get; init; }

    /// <summary>
    ///     The unique identifier of the dungeon associated with the sub area.
    /// </summary>
    public int? DungeonId { get; init; }

    /// <summary>
    ///     The name of the sub area.
    /// </summary>
    public MultiLangString? Name { get; init; }

    /// <summary>
    ///     The unique identifiers of quests for the sub area.
    /// </summary>
    public IReadOnlyCollection<int>? QuestsIds { get; init; }

    /// <summary>
    ///     The unique identifiers of NPCs for the sub area.
    /// </summary>
    public IReadOnlyCollection<int>? NpcsIds { get; init; }
}
