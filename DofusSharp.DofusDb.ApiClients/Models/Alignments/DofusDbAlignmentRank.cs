using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Alignments;

/// <summary>
///     An alignment rank in the game.
/// </summary>
public class DofusDbAlignmentRank : DofusDbResource
{
    /// <summary>
    ///     The unique identifier of the alignment order this rank belongs to.
    /// </summary>
    public long? OrderId { get; init; }

    /// <summary>
    ///     The minimum alignment value required to reach this rank.
    /// </summary>
    public int? MinimumAlignment { get; init; }

    /// <summary>
    ///     The name of the alignment rank.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The description of the alignment rank.
    /// </summary>
    public DofusDbMultiLangString? Description { get; init; }
}