using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Items;

/// <summary>
///     A set of items that can be equipped together to gain bonuses.
/// </summary>
public class DofusDbItemSet : DofusDbEntity
{
    /// <summary>
    ///     The name of the item set.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The name of the item set as lower-case and without diacritics.
    /// </summary>
    public DofusDbMultiLangString? Slug { get; init; }

    /// <summary>
    ///     The level of the item set. This is the level of the highest-level item in the set.
    /// </summary>
    public int? Level { get; init; }

    /// <summary>
    ///     The unique identifiers of the items that belong to the item set.
    /// </summary>
    public IReadOnlyCollection<DofusDbItem>? Items { get; init; }

    /// <summary>
    ///     The unique identifiers of the item types of the items that belong to the item set.
    /// </summary>
    public IReadOnlyCollection<long>? TypeIds { get; init; }

    /// <summary>
    ///     Whether the bonus of the item set is a secret bonus.
    /// </summary>
    public bool? BonusIsSecret { get; init; }

    /// <summary>
    ///     The effects of the item set, applied when multiple items from the set are equipped together.
    ///     The collection of effects at index N is applied when N items from the set are equipped.
    /// </summary>
    public IReadOnlyList<IReadOnlyCollection<DofusDbItemEffect>>? Effects { get; init; }

    /// <summary>
    ///     Whether the items in the item set are cosmetic only.
    /// </summary>
    public bool? IsCosmetic { get; init; }
}
