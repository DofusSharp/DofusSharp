namespace DofusSharp.Dofocus.ApiClients.Models.Runes;

/// <summary>
///     Represents a price record for a rune on a specific server.
/// </summary>
public class DofocusPriceRecord
{
    /// <summary>
    ///     The name of the server where the price was recorded.
    /// </summary>
    public required string ServerName { get; init; }

    /// <summary>
    ///     The price of the rune on the server.
    /// </summary>
    public required double Price { get; init; }

    /// <summary>
    ///     The date and time when the price was last updated.
    /// </summary>
    public required DateTimeOffset DateUpdated { get; init; }
}
