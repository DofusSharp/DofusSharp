using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Jobs;

/// <summary>
///     A job in the game.
/// </summary>
public class DofusDbJob : DofusDbResource
{
    /// <summary>
    ///     The ID of the icon associated with the job.
    /// </summary>
    public long? IconId { get; init; }

    /// <summary>
    ///     The URL of the icon of the item.
    /// </summary>
    public string? Img { get; init; }

    /// <summary>
    ///     Whether the job has at least one legendary craft.
    /// </summary>
    public bool? HasLegendaryCraft { get; init; }

    /// <summary>
    ///     The name of the job.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }
}
