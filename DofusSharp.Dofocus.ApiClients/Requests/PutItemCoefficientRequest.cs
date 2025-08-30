namespace DofusSharp.Dofocus.ApiClients.Requests;

/// <summary>
///     Request for updating the coefficient of a item on a specific server.
/// </summary>
public class PutItemCoefficientRequest
{
    /// <summary>
    ///     The new coefficient to set for the item.
    /// </summary>
    public required double Coefficient { get; init; }

    /// <summary>
    ///     The name of the server where the coefficient is being updated.
    /// </summary>
    public required string ServerName { get; init; }
}
