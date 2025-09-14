using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Npcs;

/// <summary>
///     A message associated with a non-player character (NPC) in the game.
/// </summary>
public class DofusDbNpcMessage : DofusDbResource
{
    /// <summary>
    ///     The parameters of the NPC message, represented as a list of unique identifiers.
    /// </summary>
    public IReadOnlyList<string>? MessageParams { get; init; }

    /// <summary>
    ///     The unique identifier of the skin used for the NPC message.
    /// </summary>
    public long? MessageSkinId { get; init; }

    /// <summary>
    ///     The position of the message bubble for the NPC message.
    /// </summary>
    public long? MessageBubblePosition { get; init; }

    /// <summary>
    ///     The mood identifier of the NPC for this message.
    /// </summary>
    public long? MessageNpcMoodId { get; init; }

    /// <summary>
    ///     The content of the NPC message, as a multi-language string.
    /// </summary>
    public DofusDbMultiLangString? Message { get; init; }
}
