using DofusSharp.Dofocus.ApiClients.Models.Common;

namespace DofusSharp.Dofocus.ApiClients.Models.Runes;

/// <summary>
///     Represents a rune in Dofocus, used for item improvement.
/// </summary>
public class DofocusRune
{
    /// <summary>
    ///     The unique identifier of the rune.
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    ///     The localized name of the rune.
    /// </summary>
    public required DofocusMultiLangString Name { get; init; }

    /// <summary>
    ///     The unique identifier of the characteristic affected by the rune.
    /// </summary>
    public required long CharacteristicId { get; init; }

    /// <summary>
    ///     The localized name of the characteristic affected by the rune.
    /// </summary>
    public required DofocusMultiLangString CharacteristicName { get; init; }

    /// <summary>
    ///     The value provided by the rune to the characteristic.
    /// </summary>
    public required int Value { get; init; }

    /// <summary>
    ///     The weight of the rune, used in item improvement calculations.
    /// </summary>
    public required double Weight { get; init; }

    /// <summary>
    ///     The latest price records for the rune across different servers.
    /// </summary>
    public required IReadOnlyCollection<DofocusPriceRecord> LatestPrices { get; init; }
}
