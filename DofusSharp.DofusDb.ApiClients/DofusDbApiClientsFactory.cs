using DofusSharp.DofusDb.ApiClients.Models.Items;

namespace DofusSharp.DofusDb.ApiClients;

/// <summary>
///     Factory for creating DofusDb API clients.
/// </summary>
/// <param name="baseAddress">The base URL of the API to query.</param>
/// <param name="referrer">The referer header to include in requests to the API.</param>
public class DofusDbApiClientsFactory(Uri baseAddress, Uri? referrer = null)
{
    public IDofusDbApiClient<Item> Items() => new DofusDbApiClient<Item>(new Uri(baseAddress, "items/"), referrer);
    public IDofusDbApiClient<ItemType> ItemTypes() => new DofusDbApiClient<ItemType>(new Uri(baseAddress, "item-types/"), referrer);
    public IDofusDbApiClient<ItemSuperType> ItemSuperTypes() => new DofusDbApiClient<ItemSuperType>(new Uri(baseAddress, "item-super-types/"), referrer);
    public IDofusDbApiClient<ItemSet> ItemSets() => new DofusDbApiClient<ItemSet>(new Uri(baseAddress, "item-sets/"), referrer);
}
