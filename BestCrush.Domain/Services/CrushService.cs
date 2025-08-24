using BestCrush.Domain.Models;

namespace BestCrush.Domain.Services;

public class CrushService
{
    public IReadOnlyDictionary<Characteristic, double> GetCrushResult(Dictionary<Characteristic, double> itemLines, int itemLevel, double coefficient)
    {
        Dictionary<Characteristic, double> result = new();

        foreach ((Characteristic characteristic, double value) in itemLines)
        {
            if (value <= 0)
            {
                continue;
            }

            double crushWeight = GetCrushWeight(characteristic, value, itemLevel);
            double boostedCrushWeight = crushWeight * coefficient;
            double runeYield = boostedCrushWeight / characteristic.GetBasicRuneWeight();
            result.Add(characteristic, runeYield);
        }

        return result;
    }

    public double GetFocusedCrushResult(Dictionary<Characteristic, double> itemLines, Characteristic focus, int itemLevel, double coefficient)
    {
        double totalCrushWeight = 0;
        foreach ((Characteristic characteristic, double value) in itemLines)
        {
            if (value <= 0)
            {
                continue;
            }

            double crushWeight = GetCrushWeight(characteristic, value, itemLevel);
            if (characteristic == focus)
            {
                totalCrushWeight += crushWeight;
            }
            else
            {
                totalCrushWeight += crushWeight / 2;
            }
        }

        double boostedCrushWeight = totalCrushWeight * coefficient;
        double runeYield = boostedCrushWeight / focus.GetBasicRuneWeight();

        return runeYield;
    }

    static double GetCrushWeight(Characteristic characteristic, double lineValue, int itemLevel) => 3 * lineValue * characteristic.GetWeight() * itemLevel / 200 + 1;
}
