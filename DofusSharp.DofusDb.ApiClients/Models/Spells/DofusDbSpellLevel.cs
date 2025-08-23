namespace DofusSharp.DofusDb.ApiClients.Models.Spells;

/// <summary>
///     A spell level in the game. Each spell has multiple levels with different properties.
/// </summary>
public class DofusDbSpellLevel : DofusDbEntity
{
    /// <summary>
    ///     The unique identifier of the spell associated with this level.
    /// </summary>
    public long? SpellId { get; init; }

    /// <summary>
    ///     The grade of the spell level.
    /// </summary>
    public int? Grade { get; init; }

    /// <summary>
    ///     The breed identifier associated with the spell.
    /// </summary>
    public long? SpellBreed { get; init; }

    /// <summary>
    ///     The action point cost to cast the spell at this level.
    /// </summary>
    public int? ApCost { get; init; }

    /// <summary>
    ///     The minimum range of the spell at this level.
    /// </summary>
    public int? MinRange { get; init; }

    /// <summary>
    ///     The maximum range of the spell at this level.
    /// </summary>
    public int? Range { get; init; }

    /// <summary>
    ///     The probability of a critical hit for the spell at this level.
    /// </summary>
    public int? CriticalHitProbability { get; init; }

    /// <summary>
    ///     The maximum number of times the spell can be stacked.
    /// </summary>
    public int? MaxStack { get; init; }

    /// <summary>
    ///     The maximum number of times the spell can be stacked per turn.
    /// </summary>
    public int? MaxStackPerTurn { get; init; }

    /// <summary>
    ///     The maximum number of times the spell can be cast on a single target per turn.
    /// </summary>
    public int? MaxCastPerTarget { get; init; }

    /// <summary>
    ///     The minimum interval between casts of the spell.
    /// </summary>
    public int? MinCastInterval { get; init; }

    /// <summary>
    ///     The criterion based on states required to cast the spell.
    /// </summary>
    public string? StatesCriterion { get; init; }

    /// <summary>
    ///     The collection of effects applied on a critical hit.
    /// </summary>
    public IReadOnlyCollection<DofusDbSpellEffect>? CriticalEffects { get; init; }

    /// <summary>
    ///     The collection of preview zones for the spell at this level.
    /// </summary>
    public IReadOnlyCollection<DofusDbSpellZoneDescription>? PreviewZones { get; init; }

    /// <summary>
    ///     The collection of effects applied when casting the spell.
    /// </summary>
    public IReadOnlyCollection<DofusDbSpellEffect>? Effects { get; init; }

    /// <summary>
    ///     Indicates whether the spell must be cast in a straight line.
    /// </summary>
    public bool? CastInLine { get; init; }

    /// <summary>
    ///     Indicates whether the spell can be cast in a diagonal.
    /// </summary>
    public bool? CastInDiagonal { get; init; }

    /// <summary>
    ///     Indicates whether line of sight is required to cast the spell.
    /// </summary>
    public bool? CastTestLos { get; init; }

    /// <summary>
    ///     Indicates whether the cell must be free to cast the spell.
    /// </summary>
    public bool? NeedFreeCell { get; init; }

    /// <summary>
    ///     Indicates whether the cell must be occupied to cast the spell.
    /// </summary>
    public bool? NeedTakenCell { get; init; }

    /// <summary>
    ///     Indicates whether the cell must be a free trap cell to cast the spell.
    /// </summary>
    public bool? NeedFreeTrapCell { get; init; }

    /// <summary>
    ///     Indicates whether the range of the spell can be boosted.
    /// </summary>
    public bool? RangeCanBeBoosted { get; init; }

    /// <summary>
    ///     Indicates whether the effects of the spell should be hidden.
    /// </summary>
    public bool? HideEffects { get; init; }

    /// <summary>
    ///     Indicates whether the spell is hidden.
    /// </summary>
    public bool? Hidden { get; init; }

    /// <summary>
    ///     Indicates whether the spell should play an animation when cast.
    /// </summary>
    public bool? PlayAnimation { get; init; }

    /// <summary>
    ///     Indicates whether the target entity must be visible to cast the spell.
    /// </summary>
    public bool? NeedVisibleEntity { get; init; }

    /// <summary>
    ///     Indicates whether the cell must not contain a portal to cast the spell.
    /// </summary>
    public bool? NeedCellWithoutPortal { get; init; }

    /// <summary>
    ///     Indicates whether portal projection is forbidden for this spell.
    /// </summary>
    public bool? PortalProjectionForbidden { get; init; }
}
