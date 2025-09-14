using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class AlmanaxCalendarClientTest
{
    [Fact]
    public async Task AlmanaxClient_Should_GetAlmanaxCalendar()
    {
        IDofusDbAlmanaxCalendarClient client = DofusDbClient.Beta(Constants.Referrer).Almanax();
        DofusDbAlmanaxCalendar value = await client.GetAlmanaxAsync(new DateOnly(1995, 06, 03));
        await Verify(value);
    }

    [Fact]
    public async Task AlmanaxCalendarsClient_Should_GetAlmanaxCalendar()
    {
        IDofusDbTableClient<DofusDbAlmanaxCalendar> client = DofusDbClient.Beta(Constants.Referrer).AlmanaxCalendars();
        DofusDbAlmanaxCalendar value = await client.GetAsync(11);
        await Verify(value);
    }

    [Fact]
    public async Task AlmanaxCalendarsClient_Should_SearchAlmanaxCalendars()
    {
        IDofusDbTableClient<DofusDbAlmanaxCalendar> client = DofusDbClient.Beta(Constants.Referrer).AlmanaxCalendars();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofusDbSearchQuery query = new();
        DofusDbAlmanaxCalendar[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);

        results.Length.Should().Be(count);
    }
}
