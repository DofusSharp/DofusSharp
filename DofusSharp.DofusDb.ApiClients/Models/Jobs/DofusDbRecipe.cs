using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Items;

namespace DofusSharp.DofusDb.ApiClients.Models.Jobs;

public class DofusDbRecipe : DofusDbResource
{
    public long? ResultId { get; init; }
    public long? ResultTypeId { get; init; }
    public int? ResultLevel { get; init; }
    public IReadOnlyList<long>? IngredientIds { get; init; }
    public IReadOnlyList<int>? Quantities { get; init; }
    public long? JobId { get; init; }
    public long? SkillId { get; init; }
    public string? ChangeVersion { get; init; }
    public long? TooltipExpirationDate { get; init; }
    public DofusDbMultiLangString? ResultName { get; init; }
    public DofusDbItem? Result { get; init; }
    public IReadOnlyList<DofusDbItem>? Ingredients { get; init; }
    public DofusDbJob? Job { get; init; }
    public DofusDbItemType? ResultType { get; init; }
}
