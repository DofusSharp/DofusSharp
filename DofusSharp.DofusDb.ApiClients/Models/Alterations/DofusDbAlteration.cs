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
