using DofusSharp.DofusDb.ApiClients.Models.Spells;

namespace DofusSharp.DofusDb.ApiClients.Models.Effects;

/// <summary>
///     An effect of a spell.
/// </summary>
public class DofusDbEffectInstance
{
    /// <summary>
    ///     The unique identifier of the effect.
    /// </summary>
    public long? EffectUId { get; init; }

    /// <summary>
    ///     The base effect identifier.
    /// </summary>
    public long? BaseEffectId { get; init; }

    /// <summary>
    ///     The effect identifier.
    /// </summary>
    public long? EffectId { get; init; }

    /// <summary>
    ///     The order of the effect in the effect list.
    /// </summary>
    public int? Order { get; init; }

    /// <summary>
    ///     The target identifier for the effect.
    /// </summary>
    public long? TargetId { get; init; }

    /// <summary>
    ///     The mask applied to the target for this effect.
    /// </summary>
    public string? TargetMask { get; init; }

    /// <summary>
    ///     The duration of the effect.
    /// </summary>
    public int? Duration { get; init; }

    /// <summary>
    ///     The random value for this effect.
    /// </summary>
    public double? Random { get; init; }

    /// <summary>
    ///     The group identifier for this effect.
    /// </summary>
    public long? Group { get; init; }

    /// <summary>
    ///     The modificator value for this effect.
    /// </summary>
    public long? Modificator { get; init; }

    /// <summary>
    ///     Indicates whether the effect is dispellable.
    /// </summary>
    public int? Dispellable { get; init; }

    /// <summary>
    ///     The delay before the effect is applied.
    /// </summary>
    public int? Delay { get; init; }

    /// <summary>
    ///     The triggers for this effect.
    /// </summary>
    public string? Triggers { get; init; }

    /// <summary>
    ///     The element type of the effect.
    /// </summary>
    public int? EffectElement { get; init; }

    /// <summary>
    ///     The spell identifier associated with this effect.
    /// </summary>
    public long? SpellId { get; init; }

    /// <summary>
    ///     The duration of the effect trigger.
    /// </summary>
    public int? EffectTriggerDuration { get; init; }

    /// <summary>
    ///     The zone description for this effect.
    /// </summary>
    public DofusDbSpellZoneDescription? ZoneDescr { get; init; }

    /// <summary>
    ///     The value of the effect.
    /// </summary>
    public int? Value { get; init; }

    /// <summary>
    ///     The number of dice for the effect.
    /// </summary>
    public int? DiceNum { get; init; }

    /// <summary>
    ///     The number of sides on the dice for the effect.
    /// </summary>
    public int? DiceSide { get; init; }

    /// <summary>
    ///     Indicates whether zero values should be displayed.
    /// </summary>
    public bool? DisplayZero { get; init; }

    /// <summary>
    ///     Indicates whether the effect is visible in the tooltip.
    /// </summary>
    public bool? VisibleInTooltip { get; init; }

    /// <summary>
    ///     Indicates whether the effect is visible in the buff UI.
    /// </summary>
    public bool? VisibleInBuffUi { get; init; }

    /// <summary>
    ///     Indicates whether the effect is visible in the fight log.
    /// </summary>
    public bool? VisibleInFightLog { get; init; }

    /// <summary>
    ///     Indicates whether the effect is visible on the terrain.
    /// </summary>
    public bool? VisibleOnTerrain { get; init; }

    /// <summary>
    ///     Indicates whether the effect is for client only.
    /// </summary>
    public bool? ForClientOnly { get; init; }

    /// <summary>
    ///     Indicates whether the effect is a trigger.
    /// </summary>
    public bool? Trigger { get; init; }
}
