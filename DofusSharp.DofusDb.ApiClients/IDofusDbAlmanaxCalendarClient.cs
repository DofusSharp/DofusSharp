using DofusSharp.DofusDb.ApiClients.Models.Almanax;

namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     A client for interacting with the `almanax` API.
/// </summary>
public interface IDofusDbAlmanaxCalendarClient : IDofusDbClient
{
    /// <summary>
    ///     Gets the almanax of the given day.
    /// </summary>
    /// <param name="date">The specific day for which to retrieve the Almanax calendar data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<DofusDbAlmanaxCalendar> GetAlmanaxCalendarAsync(DateOnly date, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get the URL to fetch the current almanax.
    ///     This URL is the one used by <see cref="GetAlmanaxCalendarAsync(CancellationToken)" />.
    /// </summary>
    /// <param name="date">The specific day for which to retrieve the Almanax calendar data.</param>
    Uri GetAlmanaxCalendarQuery(DateOnly date);
}
