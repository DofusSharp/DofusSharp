namespace DofusSharp.DofusDb.ApiClients.Models.Monsters;

/// <summary>
///     Specific drop modifier when the given criteria are met.
/// </summary>
public class DofusDbMonsterDropSpecificCoefficient
{
    /// <summary>
    ///     The unique identifier of the monster.
    /// </summary>
    public long? MonsterId { get; init; }

    /// <summary>
    ///     The grade of the monster for which the coefficient applies.
    /// </summary>
    public int? MonsterGrade { get; init; }

    /// <summary>
    ///     The drop coefficient applied when criteria are met.
    /// </summary>
    public double? DropCoefficient { get; init; }

    /// <summary>
    ///     The criteria required for the coefficient to apply.
    /// </summary>
    public string? Criteria { get; init; }
}
