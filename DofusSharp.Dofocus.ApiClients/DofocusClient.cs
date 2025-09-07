namespace DofusSharp.Dofocus.ApiClients;

public static class DofocusClient
{
    public static IDofocusClientFactory Create(Uri baseUri) => new DofocusClientFactory(baseUri);
    public static IDofocusClientFactory Production() => new DofocusClientFactory(new Uri("https://dofocus.fr/api/servers/"));
}
