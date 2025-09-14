using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Social;

/// <summary>
///     Represents a guild right in the game.
/// </summary>
public class DofusDbGuildRight : DofusDbResource
{
    /// <summary>
    ///     The order in which the guild right appears in the list.
    /// </summary>
    public int? Order { get; init; }

    /// <summary>
    ///     The unique identifier of the group this guild right belongs to.
    /// </summary>
    public int? GroupId { get; init; }

    /// <summary>
    ///     The name of the guild right.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
