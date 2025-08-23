using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     A dungeon in the game.
/// </summary>
public class DofusDbDungeon : DofusDbEntity
{
    /// <summary>
    ///     The optimal player level for the dungeon.
    /// </summary>
    public int? OptimalPlayerLevel { get; init; }

    /// <summary>
    ///     The unique identifiers of maps in the dungeon.
    /// </summary>
    public IReadOnlyCollection<long>? MapIds { get; init; }

    /// <summary>
    ///     The unique identifier of the entrance map for the dungeon.
    /// </summary>
    public long? EntranceMapId { get; init; }

    /// <summary>
    ///     The unique identifier of the exit map for the dungeon.
    /// </summary>
    public long? ExitMapId { get; init; }

    /// <summary>
    ///     The name of the dungeon.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The slug for the dungeon.
    /// </summary>
    public DofusDbMultiLangString? Slug { get; init; }

    /// <summary>
    ///     The unique identifiers of monsters present in the dungeon.
    /// </summary>
    public IReadOnlyCollection<long>? Monsters { get; init; }

    /// <summary>
    ///     The unique identifier of the subarea that the dungeon belongs to.
    /// </summary>
    public long? Subarea { get; init; }
}
