namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     A custom scale for the world map.
/// </summary>
public class WorldCustomScale
{
    /// <summary>
    ///     The X coordinate for the custom scale.
    /// </summary>
    public double? X { get; init; }

    /// <summary>
    ///     The Y coordinate for the custom scale.
    /// </summary>
    public double? Y { get; init; }

    /// <summary>
    ///     The name of the custom scale.
    /// </summary>
    public string? Name { get; init; }
}
