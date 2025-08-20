using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Items;

namespace DofusSharp.DofusDb.ApiClients.Models.Jobs;

public class Recipe : DofusDbEntity
{
    public long? ResultId { get; init; }
    public long? ResultTypeId { get; init; }
    public int? ResultLevel { get; init; }
    public IReadOnlyCollection<long>? IngredientIds { get; init; }
    public IReadOnlyCollection<int>? Quantities { get; init; }
    public long? JobId { get; init; }
    public long? SkillId { get; init; }
    public string? ChangeVersion { get; init; }
    public long? TooltipExpirationDate { get; init; }
    public MultiLangString? ResultName { get; init; }
    public Item? Result { get; init; }
    public IReadOnlyCollection<Item>? Ingredients { get; init; }
    public Job? Job { get; init; }
    public ItemType? ResultType { get; init; }
}
