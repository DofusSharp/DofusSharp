using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     An area within a super area.
/// </summary>
public class DofusDbArea : DofusDbEntity
{
    /// <summary>
    ///     The unique identifier of the super area that the area belongs to.
    /// </summary>
    public long? SuperAreaId { get; init; }

    /// <summary>
    ///     Whether the area contains houses.
    /// </summary>
    public bool? ContainsHouses { get; init; }

    /// <summary>
    ///     Whether the area contains paddocks.
    /// </summary>
    public bool? ContainsPaddocks { get; init; }

    /// <summary>
    ///     The bounds of the area.
    /// </summary>
    public DofusDbBounds? Bounds { get; init; }

    /// <summary>
    ///     The unique identifier of the world map associated with the area.
    /// </summary>
    public long? WorldMapId { get; init; }

    /// <summary>
    ///     Whether the area has a world map.
    /// </summary>
    public bool? HasWorldMap { get; init; }

    /// <summary>
    ///     Whether the area has suggestions.
    /// </summary>
    public bool? HasSuggestions { get; init; }

    /// <summary>
    ///     The name of the area.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
