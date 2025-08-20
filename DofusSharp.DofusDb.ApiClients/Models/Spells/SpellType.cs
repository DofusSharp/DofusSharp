using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Spells;

/// <summary>
///     A type of spell in the game.
/// </summary>
public class SpellType : DofusDbEntity
{
    /// <summary>
    ///     The localized long name of the spell type.
    /// </summary>
    public MultiLangString? LongName { get; init; }

    /// <summary>
    ///     The localized short name of the spell type.
    /// </summary>
    public MultiLangString? ShortName { get; init; }
}
