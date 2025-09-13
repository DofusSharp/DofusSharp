using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Ornaments;
using DofusSharp.DofusDb.ApiClients.Models.Titles;

namespace DofusSharp.DofusDb.ApiClients.Models.Achievements;

/// <inheritdoc cref="DofusDbAchievementReward" />
/// <remarks>
///     The BETA version of DofusDB uses a different type discriminator for weapons: the className fields is AchievementRewardData instead of AchievementRewards for the prod
///     environment.
///     This model is an exact copy of <see cref="DofusDbAchievementReward" /> that is mapped to the new discriminator.
/// </remarks>
public class DofusDbAchievementRewardBeta : DofusDbAchievementReward
{
    /// <summary>
    ///     The criteria required to obtain the reward.
    /// </summary>
    public string? Criterions { get; init; }
}

/// <summary>
///     A reward for completing an achievement.
/// </summary>
public class DofusDbAchievementReward : DofusDbResource
{
    /// <summary>
    ///     The unique identifier of the achievement this reward is for.
    /// </summary>
    public long? AchievementId { get; init; }

    /// <summary>
    ///     The criteria required to obtain the reward.
    /// </summary>
    public string? Criteria { get; init; }

    /// <summary>
    ///     The ratio of kamas rewarded.
    /// </summary>
    public double? KamasRatio { get; init; }

    /// <summary>
    ///     The ratio of experience rewarded.
    /// </summary>
    public double? ExperienceRatio { get; init; }

    /// <summary>
    ///     Whether the kamas reward scales with player level.
    /// </summary>
    public bool? KamasScaleWithPlayerLevel { get; init; }

    /// <summary>
    ///     The list of item IDs rewarded.
    /// </summary>
    public IReadOnlyCollection<long>? ItemsReward { get; init; }

    /// <summary>
    ///     The list of items rewarded.
    /// </summary>
    public IReadOnlyCollection<DofusDbItem>? Items { get; init; }

    /// <summary>
    ///     The quantities of each item rewarded.
    /// </summary>
    public IReadOnlyCollection<int>? ItemsQuantityReward { get; init; }

    /// <summary>
    ///     The list of emote IDs rewarded.
    /// </summary>
    public IReadOnlyCollection<long>? EmotesReward { get; init; }

    /// <summary>
    ///     The list of spell IDs rewarded.
    /// </summary>
    public IReadOnlyCollection<long>? SpellsReward { get; init; }

    /// <summary>
    ///     The list of title IDs rewarded.
    /// </summary>
    public IReadOnlyCollection<long>? TitlesReward { get; init; }

    /// <summary>
    ///     The list of title objects rewarded.
    /// </summary>
    public IReadOnlyCollection<DofusDbTitle>? Titles { get; init; }

    /// <summary>
    ///     The list of ornament IDs rewarded.
    /// </summary>
    public IReadOnlyCollection<long>? OrnamentsReward { get; init; }

    /// <summary>
    ///     The list of ornament objects rewarded.
    /// </summary>
    public IReadOnlyCollection<DofusDbOrnament>? Ornaments { get; init; }

    /// <summary>
    ///     The number of guild points rewarded.
    /// </summary>
    public int? GuildPoints { get; init; }
}
