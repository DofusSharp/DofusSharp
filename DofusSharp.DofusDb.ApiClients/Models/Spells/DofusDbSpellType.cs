using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Spells;

/// <summary>
///     A type of spell in the game.
/// </summary>
public class DofusDbSpellType : DofusDbEntity
{
    /// <summary>
    ///     The localized long name of the spell type.
    /// </summary>
    public DofusDbMultiLangString? LongName { get; init; }

    /// <summary>
    ///     The localized short name of the spell type.
    /// </summary>
    public DofusDbMultiLangString? ShortName { get; init; }
}
