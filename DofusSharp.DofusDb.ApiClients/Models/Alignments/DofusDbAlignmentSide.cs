using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Alignments;

/// <summary>
///     An alignment side in the game.
/// </summary>
public class DofusDbAlignmentSide : DofusDbResource
{
    /// <summary>
    ///     The name of the alignment side.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
