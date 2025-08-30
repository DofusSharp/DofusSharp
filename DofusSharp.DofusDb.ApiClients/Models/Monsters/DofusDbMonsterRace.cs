using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Monsters;

/// <summary>
///     A race of monsters, used for categorization.
/// </summary>
public class DofusDbMonsterRace : DofusDbResource
{
    public long? SuperRaceId { get; init; }
    public int? AgressiveZoneSize { get; init; }
    public int? AgressiveLevelDiff { get; init; }
    public string? AgressiveImmunityCriterion { get; init; }
    public long? AgressiveAttackDelay { get; init; }
    public IReadOnlyCollection<long>? Monsters { get; init; }
    public DofusDbMultiLangString? Name { get; init; }
}
