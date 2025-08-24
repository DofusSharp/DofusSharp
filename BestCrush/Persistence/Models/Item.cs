using System.ComponentModel.DataAnnotations;

namespace BestCrush.Persistence.Models;

class Item
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    // EF ctor
    public Item() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Item(long dofusDbId, string name, IReadOnlyCollection<ItemCharacteristicLine> characteristicLines)
    {
        DofusDbId = dofusDbId;
        Name = name;
        Characteristics = characteristicLines.ToArray();
    }

    public Guid Id { get; private set; }
    public long DofusDbId { get; private set; }

    [MaxLength(256)]
    public string Name { get; set; }

    public ICollection<ItemCharacteristicLine> Characteristics { get; set; }
}
