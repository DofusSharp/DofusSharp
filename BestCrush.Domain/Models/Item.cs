using System.ComponentModel.DataAnnotations;

namespace BestCrush.Domain.Models;

public class Item
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    // EF ctor
    public Item() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Item(long dofusDbId)
    {
        DofusDbId = dofusDbId;
    }

    public Guid Id { get; private set; }
    public long DofusDbId { get; private set; }
    public long? DofusDbIconId { get; set; }
    public int Level { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = "???";

    public ICollection<ItemCharacteristicLine> Characteristics { get; set; } = [];
}
