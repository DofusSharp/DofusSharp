using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     An element within a map cell.
/// </summary>
public class MapElement
{
    /// <summary>
    ///     The unique identifier of the element.
    /// </summary>
    public int? ElementId { get; init; }

    /// <summary>
    ///     The hue value of the element.
    /// </summary>
    public int? Hue { get; init; }

    /// <summary>
    ///     The shadow value of the element.
    /// </summary>
    public int? Shadow { get; init; }

    /// <summary>
    ///     The offset of the element in the map.
    /// </summary>
    public DoubleCoordinates? Offset { get; init; }

    /// <summary>
    ///     The pixel offset of the element in the map.
    /// </summary>
    public IntCoordinates? PixelOffset { get; init; }

    /// <summary>
    ///     The attitude of the element.
    /// </summary>
    public int? Attitude { get; init; }

    /// <summary>
    ///     The identifier of the element.
    /// </summary>
    public int? Identifier { get; init; }
}
