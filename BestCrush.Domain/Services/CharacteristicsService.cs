using BestCrush.Domain.Models;
using DofusSharp.Dofocus.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;

namespace BestCrush.Domain.Services;

public class CharacteristicsService
{
    IReadOnlyDictionary<long, DofusDbCharacteristic>? _dofusDbCharacteristics;
    readonly SemaphoreSlim _dofusDbCharacteristicsSemaphore = new(1, 1);

    public async Task<IReadOnlyDictionary<long, DofusDbCharacteristic>> GetDofusDbCharacteristicsAsync()
    {
        if (_dofusDbCharacteristics != null)
        {
            return _dofusDbCharacteristics;
        }

        await _dofusDbCharacteristicsSemaphore.WaitAsync();
        try
        {
            if (_dofusDbCharacteristics != null)
            {
                return _dofusDbCharacteristics;
            }

            DofusDbQuery<DofusDbCharacteristic> query = DofusDbQuery.Production().Characteristics();
            DofusDbCharacteristic[] characteristics = await query.ExecuteAsync().ToArrayAsync();
            _dofusDbCharacteristics = characteristics.Where(c => c.Id.HasValue).ToDictionary(c => c.Id!.Value, c => c);
            return _dofusDbCharacteristics;
        }
        finally
        {
            _dofusDbCharacteristicsSemaphore.Release();
        }
    }

    public Characteristic? GetCharacteristicFromDofusDb(DofusDbCharacteristic characteristic)
    {
        if (characteristic.Keyword is null)
        {
            return null;
        }

        return CharacteristicExtensions.CharacteristicFromDofusDbKeyword(characteristic.Keyword);
    }

    public async Task<Characteristic?> GetCharacteristicFromDofocusAsync(DofocusItemCharacteristicMinimal characteristic)
    {
        IReadOnlyDictionary<long, DofusDbCharacteristic> dofusDbCharacteristics = await GetDofusDbCharacteristicsAsync();
        DofusDbCharacteristic? dofusDbCharacteristic = dofusDbCharacteristics.GetValueOrDefault(characteristic.Id);
        if (dofusDbCharacteristic is null)
        {
            return null;
        }

        return GetCharacteristicFromDofusDb(dofusDbCharacteristic);
    }
}
