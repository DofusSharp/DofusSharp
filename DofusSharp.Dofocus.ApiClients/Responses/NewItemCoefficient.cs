namespace DofusSharp.Dofocus.ApiClients.Responses;

/// <summary>
///     Represents the updated coefficient information for an item on a specific server.
/// </summary>
public class NewItemCoefficient
{
    /// <summary>
    ///     The unique identifier of the item.
    /// </summary>
    public required long ItemId { get; init; }

    /// <summary>
    ///     The name of the server where the coefficient was updated.
    /// </summary>
    public required string ServerName { get; init; }

    /// <summary>
    ///     The updated coefficient of the item.
    /// </summary>
    public required double Coefficient { get; init; }

    /// <summary>
    ///     The date and time when the coefficient was updated.
    /// </summary>
    public required DateTimeOffset DateUpdated { get; init; }
}
