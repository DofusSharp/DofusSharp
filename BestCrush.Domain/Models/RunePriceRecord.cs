namespace BestCrush.EfCore.Models;

public class RunePriceRecord
{
    public RunePriceRecord(long runeId, string serverName, double price)
    {
        RuneId = runeId;
        ServerName = serverName;
        Price = price;
        Date = DateTimeOffset.Now;
    }

    public Guid Id { get; private set; }
    public long RuneId { get; private set; }
    public string ServerName { get; private set; }
    public double Price { get; private set; }
    public DateTimeOffset Date { get; private set; }
}
