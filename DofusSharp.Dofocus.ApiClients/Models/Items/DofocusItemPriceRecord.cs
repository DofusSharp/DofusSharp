namespace DofusSharp.Dofocus.ApiClients.Models.Items;

/// <summary>
///     Represents a price record for an item on a specific server.
/// </summary>
public class DofocusItemPriceRecord
{
    /// <summary>
    ///     The name of the server where the price was recorded.
    /// </summary>
    public required string ServerName { get; init; }

    /// <summary>
    ///     The price of the item on the server.
    /// </summary>
    public required double Price { get; init; }

    /// <summary>
    ///     The date and time when the price was last updated.
    /// </summary>
    public required DateTimeOffset LastUpdate { get; init; }
}
