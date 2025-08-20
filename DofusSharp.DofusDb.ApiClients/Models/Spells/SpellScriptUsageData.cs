namespace DofusSharp.DofusDb.ApiClients.Models.Spells;

/// <summary>
///     A script bound to a spell.
/// </summary>
public class SpellScriptUsageData
{
    /// <summary>
    ///     The unique identifier of the script usage data.
    /// </summary>
    public long? Id { get; init; }

    /// <summary>
    ///     The order of the script usage data.
    /// </summary>
    public int? Order { get; init; }

    /// <summary>
    ///     The unique identifier of the script associated with this usage data.
    /// </summary>
    public long? ScriptId { get; init; }

    /// <summary>
    ///     The collection of spell level identifiers for this usage data.
    /// </summary>
    public IReadOnlyCollection<int>? SpellLevels { get; init; }

    /// <summary>
    ///     The criterion required for this script usage.
    /// </summary>
    public string? Criterion { get; init; }

    /// <summary>
    ///     The mask applied to the caster.
    /// </summary>
    public string? CasterMask { get; init; }

    /// <summary>
    ///     The mask applied to the target.
    /// </summary>
    public string? TargetMask { get; init; }

    /// <summary>
    ///     The zone applied to the target.
    /// </summary>
    public string? TargetZone { get; init; }

    /// <summary>
    ///     The mask applied for activation.
    /// </summary>
    public string? ActivationMask { get; init; }

    /// <summary>
    ///     The zone applied for activation.
    /// </summary>
    public string? ActivationZone { get; init; }

    /// <summary>
    ///     The random value for this script usage.
    /// </summary>
    public double? Random { get; init; }

    /// <summary>
    ///     The random group identifier for this script usage.
    /// </summary>
    public long? RandomGroup { get; init; }

    /// <summary>
    ///     The sequence group identifier for this script usage.
    /// </summary>
    public long? SequenceGroup { get; init; }

    /// <summary>
    ///     Indicates whether the target is treated as the caster.
    /// </summary>
    public bool? IsTargetTreatedAsCaster { get; init; }

    /// <summary>
    ///     Indicates whether targets are affected concurrently.
    /// </summary>
    public bool? AreTargetAffectedConcurrently { get; init; }
}
