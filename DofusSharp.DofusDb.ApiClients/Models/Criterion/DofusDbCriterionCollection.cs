namespace DofusSharp.DofusDb.ApiClients.Models.Criterion;

public record DofusDbCriterionCollection(IReadOnlyList<DofusDbCriterion> Value) : DofusDbCriterion
{
    public virtual bool Equals(DofusDbCriterionCollection? other)
    {
        if (other is null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        return base.Equals(other) && Value.SequenceEqual(other.Value);
    }

    public override int GetHashCode()
    {
        int result = base.GetHashCode();

        foreach (DofusDbCriterion value in Value)
        {
            result = HashCode.Combine(result, value);
        }

        return result;
    }
}
