using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Mounts;

/// <summary>
///     A mount family in the game.
/// </summary>
public class DofusDbMountFamily : DofusDbResource
{
    /// <summary>
    ///     The URI of the head icon for the mount family.
    /// </summary>
    public string? HeadUri { get; init; }

    /// <summary>
    ///     The name of the mount family, in multiple languages.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
