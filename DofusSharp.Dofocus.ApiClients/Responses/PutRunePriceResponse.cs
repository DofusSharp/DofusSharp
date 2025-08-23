namespace DofusSharp.Dofocus.ApiClients.Responses;

/// <summary>
///     Represents the response after updating the price of a rune on a specific server.
/// </summary>
public class PutRunePriceResponse
{
    /// <summary>
    ///     A message describing the result of the price update operation.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    ///     The updated price information for the rune.
    /// </summary>
    public required NewRunePrice NewRunePrice { get; init; }
}
