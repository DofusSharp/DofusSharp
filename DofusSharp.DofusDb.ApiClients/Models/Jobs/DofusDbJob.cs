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

public static class DofusDbJobImagesExtensions
{
    /// <summary>
    ///     Retrieves the icon image stream for the specified job using the provided factory.
    /// </summary>
    /// <param name="job">The job whose icon is to be retrieved.</param>
    /// <param name="factory">The factory to create the image client.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static Task<Stream> GetIconAsync(this DofusDbJob job, IDofusDbClientsFactory factory, CancellationToken cancellationToken = default) =>
        job.IconId.HasValue ? factory.JobImages().GetImageAsync(job.IconId.Value, cancellationToken) : throw new ArgumentNullException(nameof(job.IconId));
}
