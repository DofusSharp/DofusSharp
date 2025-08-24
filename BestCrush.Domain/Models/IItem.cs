namespace BestCrush.Domain.Models;

public interface IItem
{
    long DofusDbId { get; }
    long? DofusDbIconId { get; }
    int Level { get; }
    string Name { get; }
}
