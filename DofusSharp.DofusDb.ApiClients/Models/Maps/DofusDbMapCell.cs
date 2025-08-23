namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     A cell within a map.
/// </summary>
public class DofusDbMapCell
{
    /// <summary>
    ///     The cell number.
    /// </summary>
    public int? Nb { get; init; }

    /// <summary>
    ///     The floor value of the cell.
    /// </summary>
    public int? Floor { get; init; }

    /// <summary>
    ///     Whether the cell is movable.
    /// </summary>
    public bool? Mov { get; init; }

    /// <summary>
    ///     Whether the cell is non-walkable during fight.
    /// </summary>
    public bool? NonWalkableDuringFight { get; init; }

    /// <summary>
    ///     Whether the cell is non-walkable during roleplay.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public bool? NonWalkableDuringRP { get; init; }

    /// <summary>
    ///     Whether the cell allows line of sight.
    /// </summary>
    public bool? Los { get; init; }

    /// <summary>
    ///     Whether the cell is a starting cell for the blue team.
    /// </summary>
    public bool? Blue { get; init; }

    /// <summary>
    ///     Whether the cell is a starting cell for the red team.
    /// </summary>
    public bool? Red { get; init; }

    /// <summary>
    ///     Whether the cell is visible.
    /// </summary>
    public bool? Visible { get; init; }

    /// <summary>
    ///     Whether the haven bag can be accessed while standing on the cell.
    /// </summary>
    public bool? HavenBagCell { get; init; }

    /// <summary>
    ///     The speed value for the cell.
    /// </summary>
    public double? Speed { get; init; }

    /// <summary>
    ///     The map change data for the cell.
    /// </summary>
    public int? MapChangeData { get; init; }

    /// <summary>
    ///     The move zone value for the cell.
    /// </summary>
    public int? MoveZone { get; init; }
}
