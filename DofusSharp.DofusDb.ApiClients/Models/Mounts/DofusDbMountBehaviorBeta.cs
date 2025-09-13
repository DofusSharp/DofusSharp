namespace DofusSharp.DofusDb.ApiClients.Models.Mounts;

/// <inheritdoc cref="DofusDbMountBehavior" />
/// <remarks>
///     The BETA version of DofusDB uses a different type discriminator for weapons: the className fields is MountBehaviorData instead of MountBehaviors for the prod environment.
///     This model is an exact copy of <see cref="DofusDbMountBehavior" /> that is mapped to the new discriminator.
/// </remarks>
public class DofusDbMountBehaviorBeta : DofusDbMountBehavior;
