using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Ornaments;

/// <summary>
///     An ornament rewarded for achievements.
/// </summary>
public class DofusDbOrnament : DofusDbResource
{
    /// <summary>
    ///     Whether the ornament is visible.
    /// </summary>
    public bool? Visible { get; init; }

    /// <summary>
    ///     The unique identifier of the asset for the ornament.
    /// </summary>
    public long? AssetId { get; init; }

    /// <summary>
    ///     The unique identifier of the icon for the ornament.
    /// </summary>
    public long? IconId { get; init; }

    /// <summary>
    ///     The order in which the ornament is displayed.
    /// </summary>
    public int? Order { get; init; }

    /// <summary>
    ///     The name of the ornament.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The image URL for the ornament.
    /// </summary>
    public string? Img { get; init; }
}
