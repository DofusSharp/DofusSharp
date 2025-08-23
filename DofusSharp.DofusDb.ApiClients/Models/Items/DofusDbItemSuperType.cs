using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Items;

/// <summary>
///     A super type of item, used for categorization.
/// </summary>
public class DofusDbItemSuperType : DofusDbResource
{
    /// <summary>
    ///     The name of the item super type.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The possible slots where items of this super type can be equipped.
    /// </summary>
    public IReadOnlyCollection<int>? Positions { get; init; }
}
