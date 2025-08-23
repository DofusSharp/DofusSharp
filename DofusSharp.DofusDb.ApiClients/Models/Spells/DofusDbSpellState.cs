using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Spells;

/// <summary>
///     A state that can be applied by spells in the game.
/// </summary>
public class DofusDbSpellState : DofusDbResource
{
    /// <summary>
    ///     Indicates whether the state prevents spell casting.
    /// </summary>
    public bool? PreventSpellCast { get; init; }

    /// <summary>
    ///     Indicates whether the state prevents participation in fights.
    /// </summary>
    public bool? PreventsFight { get; init; }

    /// <summary>
    ///     Indicates whether the state is silent (no visual or audio feedback).
    /// </summary>
    public bool? IsSilent { get; init; }

    /// <summary>
    ///     Indicates whether the entity cannot be moved while in this state.
    /// </summary>
    public bool? CantBeMoved { get; init; }

    /// <summary>
    ///     Indicates whether the entity cannot be pushed while in this state.
    /// </summary>
    public bool? CantBePushed { get; init; }

    /// <summary>
    ///     Indicates whether the entity cannot deal damage while in this state.
    /// </summary>
    public bool? CantDealDamage { get; init; }

    /// <summary>
    ///     Indicates whether the entity is invulnerable while in this state.
    /// </summary>
    public bool? Invulnerable { get; init; }

    /// <summary>
    ///     Indicates whether the entity cannot switch position while in this state.
    /// </summary>
    public bool? CantSwitchPosition { get; init; }

    /// <summary>
    ///     Indicates whether the entity is incurable while in this state.
    /// </summary>
    public bool? Incurable { get; init; }

    /// <summary>
    ///     The collection of effect identifiers associated with this state.
    /// </summary>
    public IReadOnlyCollection<long>? EffectIds { get; init; }

    /// <summary>
    ///     The icon representing this state.
    /// </summary>
    public string? Icon { get; init; }

    /// <summary>
    ///     The visibility mask for the icon of this state.
    /// </summary>
    public int? IconVisibilityMask { get; init; }

    /// <summary>
    ///     Indicates whether the entity is invulnerable to melee attacks while in this state.
    /// </summary>
    public bool? InvulnerableMelee { get; init; }

    /// <summary>
    ///     Indicates whether the entity is invulnerable to ranged attacks while in this state.
    /// </summary>
    public bool? InvulnerableRange { get; init; }

    /// <summary>
    ///     Indicates whether the entity cannot tackle others while in this state.
    /// </summary>
    public bool? CantTackle { get; init; }

    /// <summary>
    ///     Indicates whether the entity cannot be tackled while in this state.
    /// </summary>
    public bool? CantBeTackled { get; init; }

    /// <summary>
    ///     Indicates whether the remaining turns of the state should be displayed.
    /// </summary>
    public bool? DisplayTurnRemaining { get; init; }

    /// <summary>
    ///     The localized name of the state.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
