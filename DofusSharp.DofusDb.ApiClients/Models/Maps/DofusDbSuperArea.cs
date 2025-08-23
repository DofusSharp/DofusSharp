using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     A super area within a world.
/// </summary>
public class DofusDbSuperArea : DofusDbEntity
{
    /// <summary>
    ///     The unique identifier of the world map associated with the super area.
    /// </summary>
    public int? WorldMapId { get; init; }

    /// <summary>
    ///     Whether the super area has a world map.
    /// </summary>
    public bool? HasWorldMap { get; init; }

    /// <summary>
    ///     The name of the super area.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}