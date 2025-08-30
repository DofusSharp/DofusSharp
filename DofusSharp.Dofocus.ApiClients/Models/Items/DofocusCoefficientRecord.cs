namespace DofusSharp.Dofocus.ApiClients.Models.Items;

/// <summary>
///     A record of the crusher coefficient for an item on a specific server.
/// </summary>
public class DofocusCoefficientRecord
{
    /// <summary>
    ///     The name of the server where the coefficient was recorded.
    /// </summary>
    public required string ServerName { get; init; }

    /// <summary>
    ///     The coefficient recorded for the item.
    /// </summary>
    public required double Coefficient { get; init; }

    /// <summary>
    ///     The date and time of the last update for this coefficient.
    /// </summary>
    public required DateTimeOffset LastUpdate { get; init; }
}
