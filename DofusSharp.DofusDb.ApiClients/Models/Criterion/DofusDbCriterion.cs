using System.Text.Json.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Models.Criterion;

/// <summary>
///     A condition in the game.
///     Criteria are used to describe all kind of conditions in the game, such as the condition to equip an item or the condition to receive an achievement.
///     They are usually encoded as a string (e.g. <c>PO=666</c>) in the resources that use them, and can be parsed into an instance of this class by using
///     the <see cref="IDofusDbCriterionClient" />.
/// </summary>
[JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(DofusDbCriterionText))]
[JsonDerivedType(typeof(DofusDbCriterionResource))]
[JsonDerivedType(typeof(DofusDbCriterionSequence))]
[JsonDerivedType(typeof(DofusDbCriterionOperation))]
public abstract record DofusDbCriterion
{
}
