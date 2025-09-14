using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Spells;

/// <summary>
///     A spell in the game.
/// </summary>
public class DofusDbSpell : DofusDbResource
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
    public IReadOnlyCollection<DofusDbSpellScriptUsageData>? BoundScriptUsageData { get; init; }

    /// <summary>
    ///     The collection of script usage data bound to this spell for critical hits.
    /// </summary>
    public IReadOnlyCollection<DofusDbSpellScriptUsageData>? CriticalHitBoundScriptUsageData { get; init; }

    /// <summary>
    ///     The base preview zone description for the spell.
    /// </summary>
    public DofusDbSpellZoneDescription? BasePreviewZoneDescr { get; init; }

    /// <summary>
    ///     The administrative name of the spell.
    /// </summary>
    public string? AdminName { get; init; }

    /// <summary>
    ///     The localized name of the spell.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The localized description of the spell.
    /// </summary>
    public DofusDbMultiLangString? Description { get; init; }

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

public static class DofusDbSpellImagesExtensions
{
    /// <summary>
    ///     Get the icon image stream for the specified spell using the provided factory to create the images client.
    /// </summary>
    /// <param name="spell">The spell for which to fetch the icon image.</param>
    /// <param name="factory">The factory to create the images client.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static Task<Stream> GetIconAsync(this DofusDbSpell spell, IDofusDbClientsFactory factory, CancellationToken cancellationToken = default) =>
        spell.IconId.HasValue ? factory.SpellImages().GetImageAsync(spell.IconId.Value, cancellationToken) : throw new ArgumentNullException(nameof(spell.IconId));
}
