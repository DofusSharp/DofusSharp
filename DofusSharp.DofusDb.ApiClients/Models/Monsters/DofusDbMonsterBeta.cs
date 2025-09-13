namespace DofusSharp.DofusDb.ApiClients.Models.Monsters;

/// <inheritdoc cref="DofusDbMonster" />
/// <remarks>
///     The BETA version of DofusDB uses a different type discriminator for weapons: the className fields is MonsterData instead of Monsters for the prod environment.
///     This model is an exact copy of <see cref="DofusDbMonster" /> that is mapped to the new discriminator.
/// </remarks>
public class DofusDbMonsterBeta : DofusDbMonster;
