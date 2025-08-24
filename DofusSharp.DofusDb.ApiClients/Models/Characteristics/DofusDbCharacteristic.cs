using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Characteristics;

/// <summary>
///     A characteristic in the game, such as Strength, Vitality, or Wisdom.
/// </summary>
public class DofusDbCharacteristic : DofusDbResource
{
    /// <summary>
    ///     The keyword associated with the characteristic, used for identification or filtering.
    /// </summary>
    public string? Keyword { get; init; }

    /// <summary>
    ///     The asset name or path related to the characteristic, such as an icon or image.
    /// </summary>
    public string? Asset { get; init; }

    /// <summary>
    ///     The unique identifier of the category to which the characteristic belongs.
    /// </summary>
    public long? CategoryId { get; init; }

    /// <summary>
    ///     Indicates whether the characteristic is visible in the user interface.
    /// </summary>
    public bool? Visible { get; init; }

    /// <summary>
    ///     The display order of the characteristic among others.
    /// </summary>
    public int? Order { get; init; }

    /// <summary>
    ///     The unique identifier of the formula used to scale the characteristic.
    /// </summary>
    public long? ScaleFormulaId { get; init; }

    /// <summary>
    ///     Indicates whether the characteristic can be upgraded.
    /// </summary>
    public bool? Upgradable { get; init; }

    /// <summary>
    ///     The localized name of the characteristic.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
