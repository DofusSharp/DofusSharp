using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     A fixture on a map.
/// </summary>
public class DofusDbMapFixture
{
    /// <summary>
    ///     The unique identifier of the fixture.
    /// </summary>
    public int? FixtureId { get; init; }

    /// <summary>
    ///     The offset coordinates of the fixture on the map.
    /// </summary>
    public DofusDbCoordinatesInt? Offset { get; init; }

    /// <summary>
    ///     The rotation of the fixture, in degrees.
    /// </summary>
    public int? Rotation { get; init; }

    /// <summary>
    ///     The horizontal scale of the fixture.
    /// </summary>
    public int? XScale { get; init; }

    /// <summary>
    ///     The vertical scale of the fixture.
    /// </summary>
    public int? YScale { get; init; }

    /// <summary>
    ///     The red color multiplier for the fixture.
    /// </summary>
    public int? RedMultiplier { get; init; }

    /// <summary>
    ///     The green color multiplier for the fixture.
    /// </summary>
    public int? GreenMultiplier { get; init; }

    /// <summary>
    ///     The blue color multiplier for the fixture.
    /// </summary>
    public int? BlueMultiplied { get; init; }

    /// <summary>
    ///     The hue value for the fixture.
    /// </summary>
    public int? Hue { get; init; }

    /// <summary>
    ///     The alpha (transparency) value for the fixture.
    /// </summary>
    public int? Alpha { get; init; }
}
