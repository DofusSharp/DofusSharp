using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Monsters;

/// <summary>
///     A race of monsters, used for categorization.
/// </summary>
public class DofusDbMonsterRace : DofusDbResource
{
    /// <summary>
    ///     The unique identifier of the super race of the race.
    /// </summary>
    public long? SuperRaceId { get; init; }

    /// <summary>
    ///     The size of the monsters' aggressive zone.
    /// </summary>
    public int? AgressiveZoneSize { get; init; }

    /// <summary>
    ///     The level difference for aggression.
    /// </summary>
    public int? AgressiveLevelDiff { get; init; }

    /// <summary>
    ///     The criterion for immunity to aggression.
    /// </summary>
    public string? AgressiveImmunityCriterion { get; init; }

    /// <summary>
    ///     The delay before the monster attacks aggressively.
    /// </summary>
    public long? AgressiveAttackDelay { get; init; }

    /// <summary>
    ///     The monsters that are in the race.
    /// </summary>
    public IReadOnlyCollection<long>? Monsters { get; init; }

    /// <summary>
    ///     The localized name of the race.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
