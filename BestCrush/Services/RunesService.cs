using BestCrush.Models;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Runes;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;

namespace BestCrush.Services;

public class RunesService(CharacteristicsService characteristicsService)
{
    IReadOnlyCollection<DofocusRune>? _runes;
    readonly SemaphoreSlim _runesSemaphore = new(1, 1);

    IReadOnlyDictionary<Characteristic, DofocusRune>? _runesByCharacteristics;
    readonly SemaphoreSlim _runesByCharacteristicsSemaphore = new(1, 1);

    public async Task ClearCachesAsync()
    {
        await _runesSemaphore.WaitAsync();
        try
        {
            _runes = null;
        }
        finally
        {
            _runesSemaphore.Release();
        }

        await _runesByCharacteristicsSemaphore.WaitAsync();
        try
        {
            _runesByCharacteristics = null;
        }
        finally
        {
            _runesByCharacteristicsSemaphore.Release();
        }
    }

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

    public async Task<IReadOnlyDictionary<Characteristic, DofocusRune>> GetRunesByCharacteristicAsync(bool forceRefresh = false)
    {
        if (_runesByCharacteristics != null)
        {
            return _runesByCharacteristics;
        }

        await _runesByCharacteristicsSemaphore.WaitAsync();
        try
        {
            if (_runesByCharacteristics != null)
            {
                return _runesByCharacteristics;
            }

            IReadOnlyCollection<DofocusRune> runes = await GetRunesAsync(forceRefresh);
            IReadOnlyDictionary<long, DofusDbCharacteristic> characteristics = await characteristicsService.GetDofusDbCharacteristicsAsync();

            Dictionary<Characteristic, DofocusRune> result = new();
            foreach (DofocusRune rune in runes)
            {
                // for some reason Dofocus uses null for hunting runes instead of the actual characteristic id
                DofusDbCharacteristic? dofusDbCharacteristic = rune.CharacteristicId is null
                    ? characteristics.Values.SingleOrDefault(c => c.Keyword == Characteristic.Hunting.ToDofusDbKeyword())
                    : characteristics.TryGetValue(rune.CharacteristicId.Value, out DofusDbCharacteristic? value)
                        ? value
                        : null;
                if (dofusDbCharacteristic is null)
                {
                    continue;
                }

                Characteristic? characteristic = CharacteristicExtensions.CharacteristicFromDofusDbKeyword(dofusDbCharacteristic.Keyword ?? "");
                if (characteristic is null)
                {
                    continue;
                }

                result.Add(characteristic.Value, rune);
            }

            _runesByCharacteristics = result;

            return result;
        }
        finally
        {
            _runesByCharacteristicsSemaphore.Release();
        }
    }

    public async Task<Dictionary<long, double>> GetRunePricesAsync(string serverName, bool forceRefresh = false)
    {
        IReadOnlyCollection<DofocusRune> runes = await GetRunesAsync(forceRefresh);
        return runes.ToDictionary(r => r.Id, r => r.LatestPrices.Where(c => c.ServerName == serverName).OrderByDescending(c => c.DateUpdated).FirstOrDefault()?.Price ?? 0);
    }
}
