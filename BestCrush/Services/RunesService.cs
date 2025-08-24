using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Runes;

namespace BestCrush.Services;

public class RunesService
{
    IReadOnlyCollection<DofocusRune>? _runes;
    readonly SemaphoreSlim _runesSemaphore = new(1, 1);

    public async Task<IReadOnlyCollection<DofocusRune>> GetRunesAsync(bool forceRefresh = false)
    {
        if (!forceRefresh && _runes != null)
        {
            return _runes;
        }

        await _runesSemaphore.WaitAsync();
        try
        {
            if (!forceRefresh && _runes != null)
            {
                return _runes;
            }

            DofocusRunesClient client = DofocusClient.Runes();
            _runes = await client.GetRunesAsync();
            return _runes;
        }
        finally
        {
            _runesSemaphore.Release();
        }
    }

    public async Task<Dictionary<long, double>> GetRunePricesAsync(string serverName, bool forceRefresh = false)
    {
        IReadOnlyCollection<DofocusRune> runes = await GetRunesAsync(forceRefresh);
        return runes.ToDictionary(
            r => r.CharacteristicId ?? -1,
            r => r.LatestPrices.Where(c => c.ServerName == serverName).OrderByDescending(c => c.DateUpdated).FirstOrDefault()?.Price ?? 0
        );
    }
}
