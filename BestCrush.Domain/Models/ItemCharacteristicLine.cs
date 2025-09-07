namespace BestCrush.Domain.Models;

public class ItemCharacteristicLine
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    // EF ctor
    public ItemCharacteristicLine() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public ItemCharacteristicLine(Equipment equipment, Characteristic characteristic, int from, int to)
    {
        Equipment = equipment;
        Characteristic = characteristic;
        From = from;
        To = to;
    }

    public Guid Id { get; private set; }
    public Equipment Equipment { get; private set; }
    public Characteristic Characteristic { get; private set; }
    public int From { get; set; }
    public int To { get; set; }
}
