using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;

namespace Tests.Live.DofusDb.ApiClients;

public class AlmanaxCalendarClientTest
{
    [Fact]
    public async Task AlmanaxCalendarClient_Should_GetAlmanaxCalendar()
    {
        IDofusDbAlmanaxCalendarClient client = DofusDbClient.Beta(Constants.Referrer).Almanax();
        DofusDbAlmanaxCalendar value = await client.GetAlmanaxCalendarAsync(new DateOnly(1995, 06, 03));
        await Verify(value);
    }
}
