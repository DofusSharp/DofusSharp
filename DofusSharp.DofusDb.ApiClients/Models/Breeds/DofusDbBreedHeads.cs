namespace DofusSharp.DofusDb.ApiClients.Models.Breeds;

/// <summary>
///     Heads available for a character breed.
/// </summary>
public class DofusDbBreedHeads
{
    /// <summary>
    ///     The URL for the male's head image.
    /// </summary>
    public string? Male { get; init; }

    /// <summary>
    ///     The URL for the female's head image.
    /// </summary>
    public string? Female { get; init; }
}
