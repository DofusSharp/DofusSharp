using DofusSharp.DofusDb.ApiClients.Models.Criterion;
using DofusSharp.DofusDb.ApiClients.Models.Servers;

namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     A client for interacting with the `criterion` API.
/// </summary>
public interface IDofusDbCriterionClient : IDofusDbClient
{
    /// <summary>
    ///     Gets the parsed criterion from the DofusDb API.
    /// </summary>
    /// <param name="criterion">The criterion to parse.</param>
    /// <param name="language">The language to use for the textual parts of the result.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<DofusDbCriterion?> ParseCriterionAsync(string criterion, DofusDbLanguage? language = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get the URL to fetch the parsed criterion.
    ///     This URL is the one used by <see cref="ParseCriterionAsync(string, CancellationToken)" />.
    /// </summary>
    /// <param name="criterion">The criterion to parse.</param>
    /// <param name="language">The language to use for the textual parts of the result.</param>
    Uri GetCriterionRequestUri(string criterion, DofusDbLanguage? language = null);
}
