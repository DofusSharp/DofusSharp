namespace DofusSharp.Dofocus.ApiClients.Requests;

/// <summary>
///     Request for updating the price of a rune on a specific server.
/// </summary>
public class PutRunePriceRequest
{
    /// <summary>
    ///     The new price to set for the rune.
    /// </summary>
    public required double Price { get; init; }

    /// <summary>
    ///     The name of the server where the price is being updated.
    /// </summary>
    public required string ServerName { get; init; }
}
