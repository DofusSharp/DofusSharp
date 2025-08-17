namespace DofusSharp.DofusDb.ApiClients.Models.Common;

/// <summary>
///     Represents the scale of an image that can be fetched from the DofusDB API.
/// </summary>
public enum ImageScale
{
    /// <summary>
    ///     The image will be returned at full size.
    /// </summary>
    Full,

    /// <summary>
    ///     The image will be returned at three-quarters of its full size.
    /// </summary>
    ThreeQuarters,

    /// <summary>
    ///     The image will be returned at half of its full size.
    /// </summary>
    Half,

    /// <summary>
    ///     The image will be returned at a quarter of its full size.
    /// </summary>
    Quarter
}
