namespace DofusSharp.DofusDb.ApiClients.Models.Common;

/// <summary>
///     Image formats supported by the DofusDB API.
/// </summary>
public enum ImageFormat
{
    Jpeg,
    Png
}

public static class ImageFormatExtensions
{
    public static string ToExtension(this ImageFormat format) =>
        format switch
        {
            ImageFormat.Jpeg => "jpg",
            ImageFormat.Png => "png",
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
}
