using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Servers;

/// <summary>
///     A server in the game.
/// </summary>
public class DofusDbServer : DofusDbResource
{
    /// <summary>
    ///     The date when the server was opened.
    /// </summary>
    public DateOnly? OpeningDate { get; init; }

    /// <summary>
    ///     The main language of the server.
    /// </summary>
    public DofusDbLanguage? Language { get; init; }

    /// <summary>
    ///     The population level.
    /// </summary>
    public int? PopulationId { get; init; }

    /// <summary>
    ///     The type of the server.
    /// </summary>
    public int? GameTypeId { get; init; }

    /// <summary>
    ///     The main community to which the server is targeted.
    /// </summary>
    public int? CommunityId { get; init; }

    /// <summary>
    ///     The languages that must be spoken in the server.
    /// </summary>
    public IReadOnlyCollection<DofusDbLanguage>? RestrictedToLanguages { get; init; }

    /// <summary>
    ///     Whether the server allows a single person to log into multiple characters simultaneously.
    /// </summary>
    public bool? MonoAccount { get; init; }

    /// <summary>
    ///     The name of the illustration of the server.
    /// </summary>
    public string? Illus { get; init; }

    /// <summary>
    ///     The localized name of the server.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     The localized description of the server.
    /// </summary>
    public DofusDbMultiLangString? Comment { get; init; }
}
