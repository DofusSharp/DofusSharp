namespace BestCrush.Persistence.Models;

class ItemPriceRecord
{
    public ItemPriceRecord(long itemId, string serverName, double price)
    {
        ItemId = itemId;
        ServerName = serverName;
        Price = price;
        Date = DateTimeOffset.Now;
    }

    public Guid Id { get; private set; }
    public long ItemId { get; private set; }
    public string ServerName { get; private set; }
    public double Price { get; private set; }
    public DateTimeOffset Date { get; private set; }
}
