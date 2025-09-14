using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Npcs;

/// <summary>
///     A non-player character (NPC) in the game.
/// </summary>
public class DofusDbNpc : DofusDbResource
{
    /// <summary>
    ///     The list of messages associated with the NPC.
    ///     Each tuple contains the NPC message identifier and the localized string identifier.
    /// </summary>
    public IReadOnlyList<(long NpcMessageId, long LocalizedStringId)>? DialogMessages { get; init; }

    /// <summary>
    ///     The list of dialog replies available for the NPC.
    ///     Each tuple contains the NPC message identifier and the localized string identifier.
    /// </summary>
    public IReadOnlyList<(long NpcMessageId, long LocalizedStringId)>? DialogReplies { get; init; }

    /// <summary>
    ///     The list of action identifiers that the NPC can perform.
    /// </summary>
    public IReadOnlyList<long>? Actions { get; init; }

    /// <summary>
    ///     The gender of the NPC.
    /// </summary>
    public DofusDbGender? Gender { get; init; }

    /// <summary>
    ///     The visual look string of the NPC.
    /// </summary>
    public string? Look { get; init; }

    /// <summary>
    ///     The list of animation function identifiers for the NPC.
    /// </summary>
    public IReadOnlyList<DofusDbNpcAnimFun>? AnimFunList { get; init; }

    /// <summary>
    ///     Indicates whether the NPC uses fast animation functions.
    /// </summary>
    public bool? FastAnimsFun { get; init; }

    /// <summary>
    ///     Indicates whether the NPC's tooltip is visible.
    /// </summary>
    public bool? TooltipVisible { get; init; }

    /// <summary>
    ///     The list of dialog data entries for the NPC.
    /// </summary>
    public IReadOnlyList<DofusDbNpcDialogData>? DialogData { get; init; }

    /// <summary>
    ///     The default skin identifier for the NPC.
    /// </summary>
    public long? DefaultSkinId { get; init; }

    /// <summary>
    ///     The name of the NPC.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
