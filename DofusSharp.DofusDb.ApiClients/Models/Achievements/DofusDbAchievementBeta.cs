namespace DofusSharp.DofusDb.ApiClients.Models.Achievements;

/// <inheritdoc cref="DofusDbAchievement" />
/// <remarks>
///     The BETA version of DofusDB uses a different type discriminator for achievements: the className fields is AchievementData instead of Achievements for the prod environment.
///     This model is an exact copy of <see cref="DofusDbAchievement" /> that is mapped to the new discriminator.
/// </remarks>
public class DofusDbAchievementBeta : DofusDbAchievement;
