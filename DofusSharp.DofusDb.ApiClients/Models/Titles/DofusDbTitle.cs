using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Titles;

/// <summary>
///     A title rewarded for achievements.
/// </summary>
public class DofusDbTitle : DofusDbResource
{
    /// <summary>
    ///     Whether the title is visible.
    /// </summary>
    public bool? Visible { get; init; }

    /// <summary>
    ///     The unique identifier of the category this title belongs to.
    /// </summary>
    public long? CategoryId { get; init; }

    /// <summary>
    ///     The male name of the title.
    /// </summary>
    public DofusDbMultiLangString? NameMale { get; init; }

    /// <summary>
    ///     The female name of the title.
    /// </summary>
    public DofusDbMultiLangString? NameFemale { get; init; }
}
