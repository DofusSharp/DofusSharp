using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Achievements;

/// <summary>
///     An achievement in the game.
/// </summary>
public class DofusDbAchievement : DofusDbResource
{
    /// <summary>
    ///     The unique identifier of the category this achievement belongs to.
    /// </summary>
    public long? CategoryId { get; init; }

    /// <summary>
    ///     The unique identifier of the icon for the achievement.
    /// </summary>
    public long? IconId { get; init; }

    /// <summary>
    ///     The list of objective IDs for the achievement.
    /// </summary>
    public IReadOnlyCollection<long>? ObjectiveIds { get; init; }

    /// <summary>
    ///     The list of reward IDs for the achievement.
    /// </summary>
    public IReadOnlyCollection<long>? RewardIds { get; init; }

    /// <summary>
    ///     The name of the achievement.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The description of the achievement.
    /// </summary>
    public DofusDbMultiLangString? Description { get; init; }

    /// <summary>
    ///     The slug (URL-friendly name) of the achievement.
    /// </summary>
    public DofusDbMultiLangString? Slug { get; init; }

    /// <summary>
    ///     The requirements needed to complete the achievement.
    /// </summary>
    public DofusDbAchievementNeeds? Need { get; init; }

    /// <summary>
    ///     The image URL for the achievement.
    /// </summary>
    public string? Img { get; init; }
}

public static class DofusDbAchievementImagesExtensions
{
    /// <summary>
    ///     Retrieves the image stream for the achievement's icon using the provided factory.
    /// </summary>
    /// <param name="achievement">The achievement whose icon image is to be retrieved.</param>
    /// <param name="factory">The factory used to create the image client.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static async Task<Stream?> GetIconAsync(this DofusDbAchievement achievement, IDofusDbClientsFactory factory, CancellationToken cancellationToken = default) =>
        achievement.IconId.HasValue ? await factory.AchievementImages().GetImageAsync(achievement.IconId.Value, cancellationToken) : null;
}
