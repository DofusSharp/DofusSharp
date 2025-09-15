using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Challenges;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;

namespace Tests.Live.DofusDb.ApiClients;

public class ChallengesClientTest
{
    [Fact]
    public async Task ChallengesClient_Should_GetChallenge()
    {
        IDofusDbTableClient<DofusDbChallenge> client = DofusDbClient.Beta(Constants.Referrer).Challenges();
        DofusDbChallenge value = await client.GetAsync(1); // Use a valid ID for a challenge
        await Verify(value);
    }

    [Fact]
    public async Task ChallengesClient_Should_SearchChallenges()
    {
        IDofusDbTableClient<DofusDbChallenge> client = DofusDbClient.Beta(Constants.Referrer).Challenges();
        DofusDbSearchQuery query = new();
        DofusDbChallenge[] results = await client.MultiQuerySearchAsync(query).ToArrayAsync();
        int count = await client.CountAsync(query.Predicates);
        results.Length.Should().Be(count);
    }
}
