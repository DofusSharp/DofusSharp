using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Effects;

namespace DofusSharp.DofusDb.ApiClients.Models.Mounts;

/// <summary>
///     A mount in the game.
/// </summary>
public class DofusDbMount : DofusDbResource
{
    /// <summary>
    ///     The unique identifier of the family to which the mount belongs.
    /// </summary>
    public long? FamilyId { get; init; }

    /// <summary>
    ///     The visual look string of the mount.
    /// </summary>
    public string? Look { get; init; }

    /// <summary>
    ///     The unique identifier of the certificate associated with the mount.
    /// </summary>
    public long? CertificateId { get; init; }

    /// <summary>
    ///     The list of effects that the mount provides.
    /// </summary>
    public IReadOnlyList<DofusDbEffectInstance>? Effects { get; init; }

    /// <summary>
    ///     The name of the mount, in multiple languages.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
