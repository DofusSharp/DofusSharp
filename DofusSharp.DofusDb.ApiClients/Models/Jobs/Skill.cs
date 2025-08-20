using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Items;

namespace DofusSharp.DofusDb.ApiClients.Models.Jobs;

/// <summary>
///     A skill associated with a job in the game.
/// </summary>
public class Skill : DofusDbEntity
{
    /// <summary>
    ///     The unique identifier of the parent job for this skill.
    /// </summary>
    public long? ParentJobId { get; init; }

    /// <summary>
    ///     Indicates whether this skill is a Forgemagus skill.
    /// </summary>
    public bool? IsForgemagus { get; init; }

    /// <summary>
    ///     The collection of modifiable item type identifiers for this skill.
    /// </summary>
    public IReadOnlyCollection<long>? ModifiableItemTypeIds { get; init; }

    /// <summary>
    ///     The unique identifier of the gathered resource item associated with this skill.
    /// </summary>
    public long? GatheredResourceItem { get; init; }

    /// <summary>
    ///     The item associated with this skill.
    /// </summary>
    public Item? Item { get; init; }

    /// <summary>
    ///     The unique identifier of the interactive element for this skill.
    /// </summary>
    public long? InteractiveId { get; init; }

    /// <summary>
    ///     The range of the skill.
    /// </summary>
    public int? Range { get; init; }

    /// <summary>
    ///     Indicates whether the range is used in the client.
    /// </summary>
    public bool? UseRangeInClient { get; init; }

    /// <summary>
    ///     The animation used when performing this skill.
    /// </summary>
    public string? UseAnimation { get; init; }

    /// <summary>
    ///     The cursor type for this skill.
    /// </summary>
    public int? Cursor { get; init; }

    /// <summary>
    ///     The unique identifier of the element action for this skill.
    /// </summary>
    public long? ElementActionId { get; init; }

    /// <summary>
    ///     Indicates whether this skill is available in houses.
    /// </summary>
    public bool? AvailableInHouse { get; init; }

    /// <summary>
    ///     Indicates whether this skill should be displayed in the client.
    /// </summary>
    public bool? ClientDisplay { get; init; }

    /// <summary>
    ///     The minimum level required to use this skill.
    /// </summary>
    public int? LevelMin { get; init; }

    /// <summary>
    ///     The localized name of the skill.
    /// </summary>
    public MultiLangString? Name { get; init; }
}
