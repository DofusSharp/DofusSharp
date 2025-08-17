namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     A cell within a map layer.
/// </summary>
public class MapLayerCell
{
    /// <summary>
    ///     The unique identifier of the cell.
    /// </summary>
    public int? CellId { get; init; }

    /// <summary>
    ///     The elements within the cell.
    /// </summary>
    public IReadOnlyCollection<MapElement>? Elements { get; init; }

    /// <summary>
    ///     The number of elements in the cell.
    /// </summary>
    public int? ElementsCount { get; init; }
}
