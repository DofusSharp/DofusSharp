using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Npcs;

namespace DofusSharp.DofusDb.ApiClients.Models.Almanax;

public class DofusDbAlmanaxCalendar : DofusDbResource
{
    public long? NpcId { get; init; }
    public long? CategoryId { get; init; }
    public IReadOnlyCollection<string>? Dates { get; init; }
    public IReadOnlyCollection<long>? BonusIds { get; init; }
    public IReadOnlyCollection<long>? ItemIds { get; init; }
    public IReadOnlyCollection<DofusDbItem>? Items { get; init; }
    public IReadOnlyCollection<long>? Quantities { get; init; }
    public IReadOnlyCollection<long>? QuestIds { get; init; }
    public IReadOnlyCollection<long>? Types { get; init; }
    public DofusDbNpc? Npc { get; init; }
    public long? ObjectiveId { get; init; }
    public long? MeridiaIllustrationId { get; init; }
    public DofusDbMultiLangString? Name { get; init; }
    public DofusDbMultiLangString? Desc { get; init; }
    public DofusDbMultiLangString? MeriaDescription { get; init; }
    public DofusDbMultiLangString? MeriaEffect { get; init; }
    public DofusDbMultiLangString? Rubrikabrax { get; init; }
    public DofusDbMultiLangString? CelebrationName { get; init; }
    public DofusDbMultiLangString? CelebrationDescription { get; init; }
}
