using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     A world in the game.
/// </summary>
public class DofusDbWorld : DofusDbResource
{
    /// <summary>
    ///     The X coordinate of the origin of the world.
    /// </summary>
    public int? OrigineX { get; init; }

    /// <summary>
    ///     The Y coordinate of the origin of the world.
    /// </summary>
    public int? OrigineY { get; init; }

    /// <summary>
    ///     The width of the map, in world units.
    /// </summary>
    public double? MapWidth { get; init; }

    /// <summary>
    ///     The height of the map, in world units.
    /// </summary>
    public double? MapHeight { get; init; }

    /// <summary>
    ///     Whether the world is viewable everywhere.
    /// </summary>
    public bool? ViewableEverywhere { get; init; }

    /// <summary>
    ///     The minimum scale allowed for the world map.
    /// </summary>
    public double? MinScale { get; init; }

    /// <summary>
    ///     The maximum scale allowed for the world map.
    /// </summary>
    public double? MaxScale { get; init; }

    /// <summary>
    ///     The initial scale for the world map.
    /// </summary>
    public double? StartScale { get; init; }

    /// <summary>
    ///     The total width of the world, in pixels.
    /// </summary>
    public int? TotalWidth { get; init; }

    /// <summary>
    ///     The total height of the world, in pixels.
    /// </summary>
    public int? TotalHeight { get; init; }

    /// <summary>
    ///     The available zoom levels for the world map.
    /// </summary>
    public IReadOnlyCollection<double>? Zoom { get; init; }

    /// <summary>
    ///     Whether the world is visible on the map.
    /// </summary>
    public bool? VisibleOnMap { get; init; }

    /// <summary>
    ///     The name of the world.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The custom scales for the world map.
    /// </summary>
    public IReadOnlyCollection<DofusDbWorldCustomScale>? CustomScales { get; init; }
}
