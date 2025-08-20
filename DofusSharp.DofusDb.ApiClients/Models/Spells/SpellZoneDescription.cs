namespace DofusSharp.DofusDb.ApiClients.Models.Spells;

/// <summary>
///     The preview zone for a spell.
/// </summary>
public class SpellZoneDescription
{
    /// <summary>
    ///     The collection of cell identifiers in the preview zone.
    /// </summary>
    public IReadOnlyCollection<long>? CellIds { get; init; }

    /// <summary>
    ///     The shape of the preview zone.
    /// </summary>
    public long? Shape { get; init; }

    /// <summary>
    ///     The first parameter for the preview zone.
    /// </summary>
    public long? Param1 { get; init; }

    /// <summary>
    ///     The second parameter for the preview zone.
    /// </summary>
    public long? Param2 { get; init; }

    /// <summary>
    ///     The percentage step for damage decrease in the preview zone.
    /// </summary>
    public int? DamageDecreaseStepPercent { get; init; }

    /// <summary>
    ///     The maximum number of times damage decrease can be applied.
    /// </summary>
    public int? MaxDamageDecreaseApplyCount { get; init; }

    /// <summary>
    ///     Indicates whether the preview zone stops at the target.
    /// </summary>
    public bool? IsStopAtTarget { get; init; }
}
