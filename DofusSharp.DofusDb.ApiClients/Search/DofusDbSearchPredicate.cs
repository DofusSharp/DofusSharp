namespace DofusSharp.DofusDb.ApiClients.Search;

public abstract record DofusDbSearchPredicate
{
    public record Eq(string Field, string Value) : DofusDbSearchPredicate;

    public record NotEq(string Field, string Value) : DofusDbSearchPredicate;

    public record In(string Field, params IReadOnlyCollection<string> Value) : DofusDbSearchPredicate;

    public record NotIn(string Field, params IReadOnlyCollection<string> Value) : DofusDbSearchPredicate;

    public record LessThan(string Field, string Value) : DofusDbSearchPredicate;

    public record LessThanOrEquals(string Field, string Value) : DofusDbSearchPredicate;

    public record GreaterThan(string Field, string Value) : DofusDbSearchPredicate;

    public record GreaterThanOrEqual(string Field, string Value) : DofusDbSearchPredicate;

    public record And(params IReadOnlyList<DofusDbSearchPredicate> Predicates) : DofusDbSearchPredicate;

    public record Or(params IReadOnlyList<DofusDbSearchPredicate> Predicates) : DofusDbSearchPredicate;
}
