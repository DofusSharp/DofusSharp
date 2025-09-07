namespace DofusSharp.Dofocus.ApiClients;

class DofocusClientFactory(Uri baseUri) : IDofocusClientFactory
{
    public IDofocusServersClient Servers() => new DofocusServersClient(baseUri);
    public IDofocusRunesClient Runes() => new DofocusRunesClient(baseUri);
    public IDofocusItemsClient Items() => new DofocusItemsClient(baseUri);
}
