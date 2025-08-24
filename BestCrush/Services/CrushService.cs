using DofusSharp.Dofocus.ApiClients.Models.Items;

namespace BestCrush.Services;

public class CrushService(CharacteristicsService characteristicsService)
{
    public IReadOnlyDictionary<long, (DofocusItemCharacteristic Characteristic, double Min, double Max)> GetRunesWithoutFocus(DofocusItem item, double coefficient) =>
        item.Characteristics.ToDictionary(c => c.Id, c => (c, (double)c.From, (double)(c.To == 0 ? c.From : c.To)));

    public (double Min, double Max) GetRunesWithFocus(DofocusItem item, long focusedCharacteristic, double coefficient) =>
        (item.Characteristics.Sum(c => c.From), item.Characteristics.Sum(c => c.To == 0 ? c.From : c.To));
}
