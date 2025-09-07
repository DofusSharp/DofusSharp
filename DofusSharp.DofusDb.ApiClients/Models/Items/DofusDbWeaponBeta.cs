namespace DofusSharp.DofusDb.ApiClients.Models.Items;

/// <inheritdoc cref="DofusDbWeapon" />
/// <remarks>
///     The BETA version of DofusDB uses a different type discriminator for weapons: the className fields is WeaponData instead of Weapons for the prod environment.
///     This model is an exact copy of <see cref="DofusDbWeapon" /> that is mapped to the new discriminator.
/// </remarks>
public class DofusDbWeaponBeta : DofusDbWeapon;
