namespace DofusSharp.DofusDb.ApiClients.Models;

public abstract class DofusDbEntity
{
    /// <summary>
    ///     The unique identifier of the entity.
    /// </summary>
    public int? Id { get; init; }

    /// <summary>
    ///     The creation date of the entity in the database.
    /// </summary>
    public DateTimeOffset? CreatedAt { get; init; }

    /// <summary>
    ///     The last update date of the entity in the database.
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; init; }
}
