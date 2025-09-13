using System.Text.Json.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Models.Criterion;

[JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(DofusDbCriterionText))]
[JsonDerivedType(typeof(DofusDbCriterionResource))]
[JsonDerivedType(typeof(DofusDbCriterionCollection))]
[JsonDerivedType(typeof(DofusDbCriterionOperation))]
public abstract record DofusDbCriterion
{
}
