using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Achievements;

public class DofusDbAchievementCategory : DofusDbResource
{
    public long? ParentId { get; init; }
    public string? Icon { get; init; }
    public int? Order { get; init; }
    public string? Color { get; init; }
    public IReadOnlyCollection<long>? AchievementIds { get; init; }
    public string? VisibilityCriterion { get; init; }
    public DofusDbMultiLangString? Name { get; init; }
}

public class DofusDbAchievementObjective : DofusDbResource
{
    public long? AchievementId { get; init; }
    public int? Order { get; init; }
    public string? Criterion { get; init; }
    public DofusDbMultiLangString? Name { get; init; }
    public IReadOnlyList<DofusDbStringOrResource>? ReadableCriterion { get; init; }
}

public class DofusDbAchievementReward : DofusDbResource
{
    public long? AchievementId { get; init; }
    public string? Criteria { get; init; }
    public int? KamasRatio { get; init; }
    public int? ExperienceRatio { get; init; }
    public bool? KamasScaleWithPlayerLevel { get; init; }
    public IReadOnlyCollection<long>? ItemsReward { get; init; }
    public IReadOnlyCollection<int>? ItemsQuantityReward { get; init; }
    public IReadOnlyCollection<long>? EmotesReward { get; init; }
    public IReadOnlyCollection<long>? SpellsReward { get; init; }
    public IReadOnlyCollection<long>? TitlesReward { get; init; }
    public IReadOnlyCollection<DofusDbTitle>? Titles { get; init; }
    public IReadOnlyCollection<long>? OrnamentsReward { get; init; }
    public IReadOnlyCollection<DofusDbOrnament>? Ornaments { get; init; }
    public int? GuildPoints { get; init; }
}

public class DofusDbAchievement : DofusDbResource
{
    public long? CategoryId { get; init; }
    public long? IconId { get; init; }
    public IReadOnlyCollection<long>? ObjectiveIds { get; init; }
    public IReadOnlyCollection<long>? RewardIds { get; init; }
    public DofusDbMultiLangString? Name { get; init; }
    public DofusDbMultiLangString? Description { get; init; }
    public DofusDbMultiLangString? Slug { get; init; }
    public DofusDbAchievementNeeds? Need { get; init; }
    public string? Img { get; init; }
}

public class DofusDbAchievementNeeds
{
    public IReadOnlyCollection<long>? Items { get; init; }
    public IReadOnlyCollection<long>? Quantities { get; init; }
    public IReadOnlyCollection<long>? Quests { get; init; }
    public IReadOnlyCollection<long>? Achievements { get; init; }
}

public class DofusDbTitle : DofusDbResource
{
    public bool? Visible { get; init; }
    public long? CategoryId { get; init; }
    public DofusDbMultiLangString? NameMale { get; init; }
    public DofusDbMultiLangString? NameFemale { get; init; }
}

public class DofusDbOrnament : DofusDbResource
{
    public bool? Visible { get; init; }
    public long? AssetId { get; init; }
    public long? IconId { get; init; }
    public int? Order { get; init; }
    public DofusDbMultiLangString? Name { get; init; }
    public string? Img { get; init; }
}
