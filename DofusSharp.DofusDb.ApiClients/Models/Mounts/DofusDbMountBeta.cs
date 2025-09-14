namespace DofusSharp.DofusDb.ApiClients.Models.Mounts;

/// <inheritdoc cref="DofusDbMount" />
/// <remarks>
///     The BETA version of DofusDB uses a different type discriminator for mounts: the className field is MountData instead of Mounts for the prod environment.
///     This model is an exact copy of <see cref="DofusDbMount" /> that is mapped to the new discriminator.
/// </remarks>
public class DofusDbMountBeta : DofusDbMount;
