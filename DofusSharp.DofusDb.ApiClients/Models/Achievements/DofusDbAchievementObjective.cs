using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;

namespace DofusSharp.DofusDb.ApiClients.Models.Achievements;

/// <summary>
///     An objective within an achievement.
/// </summary>
public class DofusDbAchievementObjective : DofusDbResource
{
    /// <summary>
    ///     The unique identifier of the achievement this objective belongs to.
    /// </summary>
    public long? AchievementId { get; init; }

    /// <summary>
    ///     The order in which the objective is displayed.
    /// </summary>
    public int? Order { get; init; }

    /// <summary>
    ///     The criterion required to complete the objective.
    /// </summary>
    public string? Criterion { get; init; }

    /// <summary>
    ///     The name of the objective.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     A readable version of the criterion, as a list of strings or resources.
    /// </summary>
    public DofusDbCriterion? ReadableCriterion { get; init; }
}
