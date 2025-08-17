using DofusSharp.DofusDb.ApiClients.Models.Items;

namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     Factory for creating DofusDb API clients.
/// </summary>
/// <param name="baseAddress">The base URL of the API to query.</param>
/// <param name="referrer">The referer header to include in requests to the API.</param>
public class DofusDbClientsFactory(Uri baseAddress, Uri? referrer = null)
{
    public IDofusDbTableClient<Item> Items() => new DofusDbTableClient<Item>(new Uri(baseAddress, "items/"), referrer);
    public IDofusDbTableClient<ItemType> ItemTypes() => new DofusDbTableClient<ItemType>(new Uri(baseAddress, "item-types/"), referrer);
    public IDofusDbTableClient<ItemSuperType> ItemSuperTypes() => new DofusDbTableClient<ItemSuperType>(new Uri(baseAddress, "item-super-types/"), referrer);
    public IDofusDbTableClient<ItemSet> ItemSets() => new DofusDbTableClient<ItemSet>(new Uri(baseAddress, "item-sets/"), referrer);
}
