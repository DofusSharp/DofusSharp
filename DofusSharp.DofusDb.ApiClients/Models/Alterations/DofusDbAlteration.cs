using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Effects;

namespace DofusSharp.DofusDb.ApiClients.Models.Alterations;

/// <summary>
///     An alteration in the game.
/// </summary>
public class DofusDbAlteration : DofusDbResource
{
    /// <summary>
    ///     The unique identifier of the category of the alteration.
    /// </summary>
    public long? CategoryId { get; init; }

    /// <summary>
    ///     The unique identifier of the icon of the alteration.
    /// </summary>
    public long? IconId { get; init; }

    /// <summary>
    ///     Whether the alteration is visible in the game.
    /// </summary>
    public bool? IsVisible { get; init; }

    /// <summary>
    ///     The condition that must be met by a player to use the alteration.
    /// </summary>
    public string? Criteria { get; init; }

    /// <summary>
    ///     Whether the alteration is displayed on the website's encyclopedia.
    /// </summary>
    public bool? IsWebDisplay { get; init; }

    /// <summary>
    ///     The possible effects of the alteration.
    /// </summary>
    public IReadOnlyCollection<DofusDbEffectInstance>? PossibleEffects { get; init; }

    /// <summary>
    ///     The name of the alteration.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The description of the alteration.
    /// </summary>
    public DofusDbMultiLangString? Description { get; init; }

    /// <summary>
    ///     The URL of the icon of the alteration.
    /// </summary>
    public string? Img { get; init; }
}

public static class DofusDbAlterationImagesExtensions
{
    /// <summary>
    ///     Gets the icon image stream for the specified alteration using the provided factory to create the image client.
    /// </summary>
    /// <param name="alteration">The alteration whose icon to fetch.</param>
    /// <param name="factory">The factory to create the image client.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static async Task<Stream?> GetIconAsync(this DofusDbAlteration alteration, IDofusDbClientsFactory factory, CancellationToken cancellationToken = default) =>
        alteration.IconId.HasValue ? await factory.ItemImages().GetImageAsync(alteration.IconId.Value, cancellationToken) : null;
}
