namespace DofusSharp.DofusDb.ApiClients.Models.Criterion;

public record DofusDbCriterionOperation(DofusDbCriterion Left, DofusDbCriterionOperator Operator, DofusDbCriterion Right) : DofusDbCriterion;
