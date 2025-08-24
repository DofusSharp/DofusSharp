using DofusSharp.Dofocus.ApiClients.Models.Items;

namespace BestCrush.Services;

public class CrushService
{
    public Dictionary<long, double> GetRunesWithoutFocus(DofocusItem item, double coefficient) => item.Characteristics.ToDictionary(c => c.Id, c => 0.5);

    public double GetRunesWithFocus(DofocusItem item, long focusedCharacteristic, double coefficient) => 1;
}
