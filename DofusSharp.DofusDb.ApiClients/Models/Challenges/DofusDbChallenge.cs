using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Servers;

namespace DofusSharp.DofusDb.ApiClients.Models.Challenges;

/// <summary>
///     A challenge in the game.
/// </summary>
public class DofusDbChallenge : DofusDbResource
{
    /// <summary>
    ///     The collection of challenge IDs that are incompatible with this challenge.
    /// </summary>
    public IReadOnlyCollection<long>? IncompatibleChallenges { get; init; }

    /// <summary>
    ///     The category ID of the challenge.
    /// </summary>
    public long? CategoryId { get; init; }

    /// <summary>
    ///     The icon ID associated with the challenge.
    /// </summary>
    public long? IconId { get; init; }

    /// <summary>
    ///     The criterion required to complete the challenge.
    /// </summary>
    public string? CompletionCriterion { get; init; }

    /// <summary>
    ///     The criterion required to activate the challenge.
    /// </summary>
    public string? ActivationCriterion { get; init; }

    /// <summary>
    ///     The ID of the target monster for the challenge, if any.
    /// </summary>
    public long? TargetMonsterId { get; init; }

    /// <summary>
    ///     The name of the challenge.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The description of the challenge.
    /// </summary>
    public DofusDbMultiLangString? Description { get; init; }

    /// <summary>
    ///     The slug (lower-case, diacritic-free name) of the challenge.
    /// </summary>
    public DofusDbMultiLangString? Slug { get; init; }

    /// <summary>
    ///     The image associated with the challenge.
    /// </summary>
    public string? Img { get; init; }
}

public static class DofusDbChallengeImagesExtensions
{
    /// <summary>
    ///     Gets the image stream for the challenge's icon.
    /// </summary>
    /// <param name="challenge">The challenge for which to get the image stream.</param>
    /// <param name="factory">The factory to use to create the image client.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static Task<Stream> GetIconAsync(this DofusDbChallenge challenge, IDofusDbClientsFactory factory, CancellationToken cancellationToken = default) =>
        challenge.IconId.HasValue
            ? factory.ChallengeImages().GetImageAsync(challenge.IconId.Value, cancellationToken)
            : throw new InvalidOperationException("Challenge does not have an associated icon.");

    /// <summary>
    ///     Get the parsed completion criterion for the challenge.
    /// </summary>
    /// <param name="challenge">The challenge for which to get the completion criterion.</param>
    /// <param name="factory">The factory to use to create the criterion client.</param>
    /// <param name="language">The language to use for the textual parts of the criterion.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static Task<DofusDbCriterion?> GetCompletionCriterionAsync(
        this DofusDbChallenge challenge,
        IDofusDbClientsFactory factory,
        DofusDbLanguage? language = null,
        CancellationToken cancellationToken = default
    ) =>
        challenge.CompletionCriterion is not null
            ? factory.Criterion().ParseCriterionAsync(challenge.CompletionCriterion, language, cancellationToken)
            : throw new InvalidOperationException("Challenge does not have a completion criterion.");

    /// <summary>
    ///     Get the parsed activation criterion for the challenge.
    /// </summary>
    /// <param name="challenge">The challenge for which to get the activation criterion.</param>
    /// <param name="factory">The factory to use to create the criterion client.</param>
    /// <param name="language">The language to use for the textual parts of the criterion.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static Task<DofusDbCriterion?> GetActivationCriterionAsync(
        this DofusDbChallenge challenge,
        IDofusDbClientsFactory factory,
        DofusDbLanguage? language = null,
        CancellationToken cancellationToken = default
    ) =>
        challenge.ActivationCriterion is not null
            ? factory.Criterion().ParseCriterionAsync(challenge.ActivationCriterion, language, cancellationToken)
            : throw new InvalidOperationException("Challenge does not have a activation criterion.");

    /// <summary>
    ///     Get the target monster for the challenge.
    /// </summary>
    /// <param name="challenge">The challenge for which to get the target monster.</param>
    /// <param name="factory">The factory to use to create the monster client.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static Task<DofusDbMonster> GetTargetMonsterAsync(this DofusDbChallenge challenge, IDofusDbClientsFactory factory, CancellationToken cancellationToken = default) =>
        challenge.TargetMonsterId.HasValue
            ? factory.Monsters().GetAsync(challenge.TargetMonsterId.Value, cancellationToken)
            : throw new InvalidOperationException("Challenge does not have a target monster.");
}
