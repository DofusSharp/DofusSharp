using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Achievements;

/// <summary>
///     A category of achievements in the game.
/// </summary>
public class DofusDbAchievementCategory : DofusDbResource
{
    /// <summary>
    ///     The unique identifier of the parent category, if any.
    /// </summary>
    public long? ParentId { get; init; }

    /// <summary>
    ///     The icon associated with the category.
    /// </summary>
    public string? Icon { get; init; }

    /// <summary>
    ///     The order in which the category is displayed.
    /// </summary>
    public int? Order { get; init; }

    /// <summary>
    ///     The color code for the category.
    /// </summary>
    public string? Color { get; init; }

    /// <summary>
    ///     The list of achievement IDs belonging to this category.
    /// </summary>
    public IReadOnlyCollection<long>? AchievementIds { get; init; }

    /// <summary>
    ///     The criterion for the visibility of the category.
    /// </summary>
    public string? VisibilityCriterion { get; init; }

    /// <summary>
    ///     The name of the category.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}