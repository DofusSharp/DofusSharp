using DofusSharp.Dofocus.ApiClients.Models.Common;

namespace DofusSharp.Dofocus.ApiClients.Models.Items;

/// <summary>
///     A characteristic of an item.
/// </summary>
public class DofocusItemCharacteristic
{
    /// <summary>
    ///     The unique identifier of the characteristic.
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    ///     The localized name of the characteristic.
    /// </summary>
    public required DofocusMultiLangString Name { get; init; }

    /// <summary>
    ///     The minimum value of the characteristic.
    /// </summary>
    public required int From { get; init; }

    /// <summary>
    ///     The maximum value of the characteristic.
    /// </summary>
    public required int To { get; init; }

    public static implicit operator DofocusItemCharacteristicMinimal(DofocusItemCharacteristic item) => item.AsMinimal();

    public DofocusItemCharacteristicMinimal AsMinimal() => new() { Id = Id };
}
