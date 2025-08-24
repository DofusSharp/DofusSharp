namespace BestCrush.Domain.Models;

public class ItemCoefficientRecord
{
    public ItemCoefficientRecord(long itemId, string serverName, int coefficient)
    {
        ItemId = itemId;
        ServerName = serverName;
        Coefficient = coefficient;
        Date = DateTimeOffset.Now;
    }

    public Guid Id { get; private set; }
    public long ItemId { get; private set; }
    public string ServerName { get; private set; }
    public int Coefficient { get; private set; }
    public DateTimeOffset Date { get; private set; }
}
