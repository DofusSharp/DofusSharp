namespace DofusSharp.DofusDb.ApiClients.Models;

public abstract class DofusDbResource : IEquatable<DofusDbResource>
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

    public bool Equals(DofusDbResource? other)
    {
        if (other?.Id is null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        if (obj.GetType() != GetType())
        {
            return false;
        }
        return Equals((DofusDbResource)obj);
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(DofusDbResource? left, DofusDbResource? right) => Equals(left, right);

    public static bool operator !=(DofusDbResource? left, DofusDbResource? right) => !Equals(left, right);
}
