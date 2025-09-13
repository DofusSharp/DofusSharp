using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Mounts;

/// <summary>
///     A mount behavior in the game.
/// </summary>
public class DofusDbMountBehavior : DofusDbResource
{
    /// <summary>
    ///     The name of the mount behavior, in multiple languages.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The description of the mount behavior, in multiple languages.
    /// </summary>
    public DofusDbMultiLangString? Description { get; init; }
}
