namespace DofusSharp.Dofocus.ApiClients;

public interface IDofocusClientFactory
{
    IDofocusServersClient Servers();
    IDofocusRunesClient Runes();
    IDofocusItemsClient Items();
}
