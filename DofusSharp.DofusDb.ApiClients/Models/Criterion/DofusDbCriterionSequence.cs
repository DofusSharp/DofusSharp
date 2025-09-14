namespace DofusSharp.DofusDb.ApiClients.Models.Criterion;

/// <summary>
///     A sequence of criteria.
///     This is usually used to chain <see cref="DofusDbCriterionText" /> and <see cref="DofusDbCriterionResource" /> together to form a sentence.
/// </summary>
/// <param name="Value"></param>
public record DofusDbCriterionSequence(IReadOnlyList<DofusDbCriterion> Value) : DofusDbCriterion
{
    public virtual bool Equals(DofusDbCriterionSequence? other)
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
