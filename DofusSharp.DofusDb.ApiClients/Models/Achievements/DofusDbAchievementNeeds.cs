namespace DofusSharp.DofusDb.ApiClients.Models.Achievements;

/// <summary>
///     Represents the requirements needed to complete an achievement.
/// </summary>
public class DofusDbAchievementNeeds
{
    /// <summary>
    ///     The list of item IDs required.
    /// </summary>
    public IReadOnlyCollection<long>? Items { get; init; }

    /// <summary>
    ///     The quantities of each item required.
    /// </summary>
    public IReadOnlyCollection<long>? Quantities { get; init; }

    /// <summary>
    ///     The list of quest IDs required.
    /// </summary>
    public IReadOnlyCollection<long>? Quests { get; init; }

    /// <summary>
    ///     The list of achievement IDs required.
    /// </summary>
    public IReadOnlyCollection<long>? Achievements { get; init; }
}
