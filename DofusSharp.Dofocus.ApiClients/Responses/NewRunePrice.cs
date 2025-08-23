namespace DofusSharp.Dofocus.ApiClients.Responses;

/// <summary>
///     Represents the updated price information for a rune on a specific server.
/// </summary>
public class NewRunePrice
{
    /// <summary>
    ///     The unique identifier of the rune.
    /// </summary>
    public required long RuneId { get; init; }

    /// <summary>
    ///     The name of the server where the price was updated.
    /// </summary>
    public required string ServerName { get; init; }

    /// <summary>
    ///     The updated price of the rune.
    /// </summary>
    public required double Price { get; init; }

    /// <summary>
    ///     The date and time when the price was updated.
    /// </summary>
    public required DateTimeOffset DateUpdated { get; init; }
}
