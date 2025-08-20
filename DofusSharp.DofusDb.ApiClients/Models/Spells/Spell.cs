using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Spells;

/// <summary>
///     A spell in the game.
/// </summary>
public class Spell : DofusDbEntity
{
    /// <summary>
    ///     The unique identifier of the type of the spell.
    /// </summary>
    public long? TypeId { get; init; }

    /// <summary>
    ///     The order of the spell in its category.
    /// </summary>
    public int? Order { get; init; }

    /// <summary>
    ///     The script parameters for the spell.
    /// </summary>
    public string? ScriptParams { get; init; }

    /// <summary>
    ///     The script parameters for the spell when a critical hit occurs.
    /// </summary>
    public string? ScriptParamsCritical { get; init; }

    /// <summary>
    ///     The unique identifier of the script associated with the spell.
    /// </summary>
    public long? ScriptId { get; init; }

    /// <summary>
    ///     The unique identifier of the script associated with the spell when a critical hit occurs.
    /// </summary>
    public long? ScriptIdCritical { get; init; }

    /// <summary>
    ///     The unique identifier of the icon representing the spell.
    /// </summary>
    public long? IconId { get; init; }

    /// <summary>
    ///     The collection of spell level identifiers for this spell.
    /// </summary>
    public IReadOnlyCollection<int>? SpellLevels { get; init; }

    /// <summary>
    ///     The collection of script usage data bound to this spell.
    /// </summary>
    public IReadOnlyCollection<SpellScriptUsageData>? BoundScriptUsageData { get; init; }

    /// <summary>
    ///     The collection of script usage data bound to this spell for critical hits.
    /// </summary>
    public IReadOnlyCollection<SpellScriptUsageData>? CriticalHitBoundScriptUsageData { get; init; }

    /// <summary>
    ///     The base preview zone description for the spell.
    /// </summary>
    public SpellZoneDescription? BasePreviewZoneDescr { get; init; }

    /// <summary>
    ///     The administrative name of the spell.
    /// </summary>
    public string? AdminName { get; init; }

    /// <summary>
    ///     The localized name of the spell.
    /// </summary>
    public MultiLangString? Name { get; init; }

    /// <summary>
    ///     The localized description of the spell.
    /// </summary>
    public MultiLangString? Description { get; init; }

    /// <summary>
    ///     Indicates whether the spell cast should be verbose.
    /// </summary>
    public bool? VerboseCast { get; init; }

    /// <summary>
    ///     Indicates whether the spell bypasses the summoning limit.
    /// </summary>
    public bool? BypassSummoningLimit { get; init; }

    /// <summary>
    ///     Indicates whether the spell can always trigger other spells.
    /// </summary>
    public bool? CanAlwaysTriggerSpells { get; init; }

    /// <summary>
    ///     Indicates whether the cast conditions of the spell should be hidden.
    /// </summary>
    public bool? HideCastConditions { get; init; }
}
