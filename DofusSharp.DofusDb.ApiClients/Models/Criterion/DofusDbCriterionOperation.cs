namespace DofusSharp.DofusDb.ApiClients.Models.Criterion;

/// <summary>
///     A logical formula involving criteria.
/// </summary>
public record DofusDbCriterionOperation(DofusDbCriterion Left, DofusDbCriterionOperator Operator, DofusDbCriterion Right) : DofusDbCriterion;
