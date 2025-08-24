using System.ComponentModel.DataAnnotations;

namespace BestCrush.Domain.Models;

public class Equipment : IItem
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    // EF ctor
    public Equipment() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Equipment(long dofusDbId)
    {
        DofusDbId = dofusDbId;
    }

    public Guid Id { get; private set; }
    public long DofusDbId { get; private set; }
    public long? DofusDbIconId { get; set; }
    public int Level { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = "???";

    public EquipmentType Type { get; set; }
    public ICollection<ItemCharacteristicLine> Characteristics { get; set; } = [];
    public ICollection<RecipeEntry> Recipe { get; set; } = [];
}
