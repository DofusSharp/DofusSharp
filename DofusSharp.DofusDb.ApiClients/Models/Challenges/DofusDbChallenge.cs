using DofusSharp.DofusDb.ApiClients.Models.Common;

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
