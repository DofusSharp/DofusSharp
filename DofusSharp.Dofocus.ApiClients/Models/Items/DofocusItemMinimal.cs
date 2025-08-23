using DofusSharp.Dofocus.ApiClients.Models.Common;

namespace DofusSharp.Dofocus.ApiClients.Models.Items;

/// <summary>
///     Minimal representation of an item in Dofocus.
/// </summary>
public class DofocusItemMinimal
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
    public required DofocusSuperTypeMinimal SuperType { get; init; }

    /// <summary>
    ///     The URL of the item's image.
    /// </summary>
    public required string ImageUrl { get; init; }

    /// <summary>
    ///     The minimal characteristics of the item.
    /// </summary>
    public required IReadOnlyCollection<DofocusItemCharacteristicMinimal> Characteristics { get; init; }
}
