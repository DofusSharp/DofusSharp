using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Monsters;

/// <summary>
///     A super race of monsters, used for categorization.
/// </summary>
public class DofusDbMonsterSuperRace : DofusDbResource
{
    /// <summary>
    ///     The localized name of the super race.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
