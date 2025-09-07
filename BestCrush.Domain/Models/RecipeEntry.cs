namespace BestCrush.Domain.Models;

public class RecipeEntry
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    // EF ctor
    public RecipeEntry() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public RecipeEntry(Equipment equipment, Resource resource, int count)
    {
        Equipment = equipment;
        Resource = resource;
        Count = count;
    }

    public Guid Id { get; private set; }
    public Equipment Equipment { get; private set; }
    public Resource Resource { get; private set; }
    public int Count { get; set; }
}
