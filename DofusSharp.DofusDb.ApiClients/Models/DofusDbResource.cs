namespace DofusSharp.DofusDb.ApiClients.Models;

public abstract class DofusDbResource
{
    /// <summary>
    ///     The unique identifier of the resource.
    /// </summary>
    public long? Id { get; init; }

    /// <summary>
    ///     The creation date of the resource in the database.
    /// </summary>
    public DateTimeOffset? CreatedAt { get; init; }

    /// <summary>
    ///     The last update date of the resource in the database.
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; init; }
}
