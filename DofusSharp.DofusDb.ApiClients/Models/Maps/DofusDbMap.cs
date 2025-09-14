using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     A map in the game.
/// </summary>
public class DofusDbMap : DofusDbResource
{
    /// <summary>
    ///     The unique identifiers of background fixtures for the map.
    /// </summary>
    public IReadOnlyCollection<DofusDbMapFixture>? BackgroundFixtures { get; init; }

    /// <summary>
    ///     The number of background fixtures in the map.
    /// </summary>
    public int? BackgroundsCount { get; init; }

    /// <summary>
    ///     The unique identifiers of foreground fixtures for the map.
    /// </summary>
    public IReadOnlyCollection<DofusDbMapFixture>? ForegroundFixtures { get; init; }

    /// <summary>
    ///     The number of foreground fixtures in the map.
    /// </summary>
    public int? ForegroundsCount { get; init; }

    /// <summary>
    ///     The version of the map.
    /// </summary>
    public int? MapVersion { get; init; }

    /// <summary>
    ///     Whether the map is encrypted.
    /// </summary>
    public bool? Encrypted { get; init; }

    /// <summary>
    ///     The encrypted version of the map.
    /// </summary>
    public int? EncryptedVersion { get; init; }

    /// <summary>
    ///     The relative identifier of the map.
    /// </summary>
    public long? RelativeId { get; init; }

    /// <summary>
    ///     The type of the map.
    /// </summary>
    public int? MapType { get; init; }

    /// <summary>
    ///     The unique identifier of the sub area that the map belongs to.
    /// </summary>
    public long? SubAreaId { get; init; }

    /// <summary>
    ///     The shadow bonus on entities for the map.
    /// </summary>
    public long? ShadowBonusOnEntities { get; init; }

    /// <summary>
    ///     The alpha value of the background.
    /// </summary>
    public long? BackgroundAlpha { get; init; }

    /// <summary>
    ///     The red color value of the background.
    /// </summary>
    public long? BackgroundRed { get; init; }

    /// <summary>
    ///     The green color value of the background.
    /// </summary>
    public long? BackgroundGreen { get; init; }

    /// <summary>
    ///     The blue color value of the background.
    /// </summary>
    public long? BackgroundBlue { get; init; }

    /// <summary>
    ///     The grid color value for the map.
    /// </summary>
    public long? GridColor { get; init; }

    /// <summary>
    ///     The background color value for the map.
    /// </summary>
    public long? BackgroundColor { get; init; }

    /// <summary>
    ///     The zoom scale for the map.
    /// </summary>
    public int? ZoomScale { get; init; }

    /// <summary>
    ///     The zoom offset X value for the map.
    /// </summary>
    public int? ZoomOffsetX { get; init; }

    /// <summary>
    ///     The zoom offset Y value for the map.
    /// </summary>
    public int? ZoomOffsetY { get; init; }

    /// <summary>
    ///     The unique identifier of the tactical mode template for the map.
    /// </summary>
    public long? TacticalModeTemplateId { get; init; }

    /// <summary>
    ///     The ground CRC value for the map.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public int? GroundCRC { get; init; }

    /// <summary>
    ///     The unique identifiers of layers for the map.
    /// </summary>
    public IReadOnlyCollection<DofusDbMapLayer>? Layers { get; init; }

    /// <summary>
    ///     The number of layers in the map.
    /// </summary>
    public int? LayersCount { get; init; }

    /// <summary>
    ///     The cells in the map.
    /// </summary>
    public IReadOnlyCollection<DofusDbMapCell>? Cells { get; init; }

    /// <summary>
    ///     The number of cells in the map.
    /// </summary>
    public int? CellsCount { get; init; }
}

public static class DofusDbMapImagesExtensions
{
    /// <summary>
    ///     Fetches the image of the map.
    /// </summary>
    /// <param name="map">The map to fetch the image for.</param>
    /// <param name="factory">The factory to create the client from.</param>
    /// <param name="scale">The scale of the image to fetch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static Task<Stream> GetImageAsync(
        this DofusDbMap map,
        IDofusDbClientsFactory factory,
        DofusDbImageScale scale = DofusDbImageScale.Full,
        CancellationToken cancellationToken = default
    ) =>
        map.Id.HasValue ? factory.MapImages().GetImageAsync(map.Id.Value, scale, cancellationToken) : throw new ArgumentNullException(nameof(map.Id));
}
