using DofusSharp.Dofocus.ApiClients.Models.Common;
using DofusSharp.Dofocus.ApiClients.Models.Runes;

namespace DofusSharp.Dofocus.ApiClients.Models.Items;

/// <summary>
///     Minimal representation of an item in Dofocus.
/// </summary>
public class DofocusItem
{
    /// <summary>
    ///     The unique identifier of the item.
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    ///     The localized name of the item.
    /// </summary>
    public required DofocusMultiLangString Name { get; init; }

    /// <summary>
    ///     The level of the item.
    /// </summary>
    public required int Level { get; init; }

    /// <summary>
    ///     The super type of the item.
    /// </summary>
    public required DofocusSuperType SuperType { get; init; }

    /// <summary>
    ///     The super type of the item.
    /// </summary>
    public required DofocusType Type { get; init; }

    /// <summary>
    ///     The URL of the item's image.
    /// </summary>
    public required string ImageUrl { get; init; }

    /// <summary>
    ///     The minimal characteristics of the item.
    /// </summary>
    public required IReadOnlyCollection<DofocusItemCharacteristics> Characteristics { get; init; }

    /// <summary>
    ///     The coefficients recorded for the item across different servers.
    /// </summary>
    public required IReadOnlyCollection<DofocusCoefficientRecord> Coefficients { get; init; }

    /// <summary>
    ///     The price recorded for the item across different servers.
    /// </summary>
    public required IReadOnlyCollection<DofocusPriceRecord> Prices { get; init; }
}
