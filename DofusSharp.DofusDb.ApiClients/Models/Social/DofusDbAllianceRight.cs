using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Social;

/// <summary>
///     Represents an alliance right in the game.
/// </summary>
public class DofusDbAllianceRight : DofusDbResource
{
    /// <summary>
    ///     The order in which the alliance right appears in the list.
    /// </summary>
    public int? Order { get; init; }

    /// <summary>
    ///     The unique identifier of the group this alliance right belongs to.
    /// </summary>
    public int? GroupId { get; init; }

    /// <summary>
    ///     The name of the alliance right.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
