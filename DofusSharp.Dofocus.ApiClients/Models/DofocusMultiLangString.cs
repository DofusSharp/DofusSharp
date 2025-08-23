namespace DofusSharp.Dofocus.ApiClients.Models;

/// <summary>
///     Localized string for multiple languages.
/// </summary>
public class DofocusMultiLangString
{
    /// <summary>
    ///     The French translation.
    /// </summary>
    public required string Fr { get; init; }

    /// <summary>
    ///     The English translation.
    /// </summary>
    public required string En { get; init; }

    /// <summary>
    ///     The Spanish translation.
    /// </summary>
    public required string Es { get; init; }
}
