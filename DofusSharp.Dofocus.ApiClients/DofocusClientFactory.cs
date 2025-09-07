namespace DofusSharp.Dofocus.ApiClients;

class DofocusClientFactory(Uri baseUri) : IDofocusClientFactory
{
    public IDofocusServersClient Servers() => new DofocusServersClient(new Uri(baseUri, "servers/"));
    public IDofocusRunesClient Runes() => new DofocusRunesClient(new Uri(baseUri, "runes/"));
    public IDofocusItemsClient Items() => new DofocusItemsClient(new Uri(baseUri, "items/"));
}
