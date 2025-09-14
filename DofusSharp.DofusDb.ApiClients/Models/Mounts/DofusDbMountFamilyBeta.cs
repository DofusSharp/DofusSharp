namespace DofusSharp.DofusDb.ApiClients.Models.Mounts;

/// <inheritdoc cref="DofusDbMountFamily" />
/// <remarks>
///     The BETA version of DofusDB uses a different type discriminator for mount families: the className fields is MountFamilyData instead of MountFamilys for the prod environment.
///     This model is an exact copy of <see cref="DofusDbMountFamily" /> that is mapped to the new discriminator.
/// </remarks>
public class DofusDbMountFamilyBeta : DofusDbMountFamily;
