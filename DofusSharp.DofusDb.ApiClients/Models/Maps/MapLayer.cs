namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     A layer within a map.
/// </summary>
public class MapLayer
{
    /// <summary>
    ///     The unique identifier of the layer.
    /// </summary>
    public int? LayerId { get; init; }

    /// <summary>
    ///     The cells in the map.
    /// </summary>
    public IReadOnlyCollection<MapLayerCell>? Cells { get; init; }

    /// <summary>
    ///     The number of cells in the layer.
    /// </summary>
    public int? CellsCount { get; init; }
}
