namespace DofusSharp.Dofocus.ApiClients.Responses;

/// <summary>
///     Represents the response after updating the coefficient of a item on a specific server.
/// </summary>
public class PutItemCoefficientResponse
{
    /// <summary>
    ///     A message describing the result of the coefficient update operation.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    ///     The updated coefficient information for the item.
    /// </summary>
    public required NewItemCoefficient Coefficient { get; init; }
}
