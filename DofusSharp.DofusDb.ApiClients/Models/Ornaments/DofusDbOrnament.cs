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

public static class DofusDbOrnamentImagesExtensions
{
    /// <summary>
    ///     Gets the icon image stream for the specified ornament using the provided factory to create the image client.
    /// </summary>
    /// <param name="ornament">The ornament whose icon to fetch.</param>
    /// <param name="factory">The factory to create the image client.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static async Task<Stream?> GetIconAsync(this DofusDbOrnament ornament, IDofusDbClientsFactory factory, CancellationToken cancellationToken = default) =>
        ornament.IconId.HasValue ? await factory.OrnamentImages().GetImageAsync(ornament.IconId.Value, cancellationToken) : null;
}
