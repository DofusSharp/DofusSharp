namespace DofusSharp.DofusDb.ApiClients.Models.Monsters;

/// <summary>
///     An item that can be dropped by a monster.
/// </summary>
public class DofusDbMonsterDrop
{
    /// <summary>
    ///     The unique identifier of the monster.
    /// </summary>
    public long? MonsterId { get; init; }

    /// <summary>
    ///     The unique identifier of the dropped object.
    /// </summary>
    public long? ObjectId { get; init; }

    /// <summary>
    ///     The percent drop rate for grade 1.
    /// </summary>
    public double? PercentDropForGrade1 { get; init; }

    /// <summary>
    ///     The percent drop rate for grade 2.
    /// </summary>
    public double? PercentDropForGrade2 { get; init; }

    /// <summary>
    ///     The percent drop rate for grade 3.
    /// </summary>
    public double? PercentDropForGrade3 { get; init; }

    /// <summary>
    ///     The percent drop rate for grade 4.
    /// </summary>
    public double? PercentDropForGrade4 { get; init; }

    /// <summary>
    ///     The percent drop rate for grade 5.
    /// </summary>
    public double? PercentDropForGrade5 { get; init; }

    /// <summary>
    ///     The number of items dropped.
    /// </summary>
    public int? Count { get; init; }

    /// <summary>
    ///     The criteria required for the drop.
    /// </summary>
    public string? Criteria { get; init; }

    /// <summary>
    ///     Whether the drop has criteria.
    /// </summary>
    public bool? HasCriteria { get; init; }

    /// <summary>
    ///     Whether the drop is hidden if criteria are invalid.
    /// </summary>
    public bool? HiddenIfInvalidCriteria { get; init; }

    /// <summary>
    ///     Specific coefficients for the drop when criteria are met.
    /// </summary>
    public IReadOnlyCollection<DofusDbMonsterDropSpecificCoefficient>? SpecificCoefficients { get; init; }
}
