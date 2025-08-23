namespace DofusSharp.Dofocus.ApiClients.Responses;

/// <summary>
///     Represents the updated price information for an item on a specific server.
/// </summary>
public class NewItemPrice
{
    /// <summary>
    ///     The unique identifier of the item.
    /// </summary>
    public required long ItemId { get; init; }

    /// <summary>
    ///     The name of the server where the price was updated.
    /// </summary>
    public required string ServerName { get; init; }

    /// <summary>
    ///     The updated price of the item.
    /// </summary>
    public required double Price { get; init; }

    /// <summary>
    ///     The date and time when the price was updated.
    /// </summary>
    public required DateTimeOffset DateUpdated { get; init; }
}
