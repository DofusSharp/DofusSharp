namespace DofusSharp.DofusDb.ApiClients.Models.Npcs;

/// <summary>
///     Dialog data for an NPC.
/// </summary>
public class DofusDbNpcDialogData
{
    /// <summary>
    ///     The unique identifier of the dialog data entry.
    /// </summary>
    public long? Id { get; init; }

    /// <summary>
    ///     The message identifier associated with the dialog data.
    /// </summary>
    public long? MessageId { get; init; }

    /// <summary>
    ///     The mood identifier for the dialog data.
    /// </summary>
    public long? MoodId { get; init; }

    /// <summary>
    ///     The position of the dialog bubble.
    /// </summary>
    public long? BubblePosition { get; init; }
}
