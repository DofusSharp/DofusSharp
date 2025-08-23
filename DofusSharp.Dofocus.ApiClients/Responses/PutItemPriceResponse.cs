namespace DofusSharp.Dofocus.ApiClients.Responses;

/// <summary>
///     Represents the response after updating the price of a item on a specific server.
/// </summary>
public class PutItemPriceResponse
{
    /// <summary>
    ///     A message describing the result of the price update operation.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    ///     The updated price information for the item.
    /// </summary>
    public required NewItemPrice NewItemPrice { get; init; }
}
