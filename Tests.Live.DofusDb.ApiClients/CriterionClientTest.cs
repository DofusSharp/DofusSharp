using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;

namespace Tests.Live.DofusDb.ApiClients;

public class CriterionClientTest
{
    [Fact]
    public async Task CriterionClient_Should_GetCriterion()
    {
        IDofusDbCriterionClient client = DofusDbClient.Beta(Constants.Referrer).Criterion();
        DofusDbCriterion? value = await client.ParseCriterionAsync("(EM>147,0,d)&Wo=0");
        await Verify(value);
    }
}
