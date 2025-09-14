using System.Text.Json.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Search;

[JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(Eq))]
[JsonDerivedType(typeof(NotEq))]
[JsonDerivedType(typeof(In))]
[JsonDerivedType(typeof(NotIn))]
[JsonDerivedType(typeof(LessThan))]
[JsonDerivedType(typeof(LessThanOrEquals))]
[JsonDerivedType(typeof(GreaterThan))]
[JsonDerivedType(typeof(GreaterThanOrEqual))]
[JsonDerivedType(typeof(And))]
[JsonDerivedType(typeof(Or))]
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
