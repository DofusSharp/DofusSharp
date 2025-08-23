namespace DofusSharp.Dofocus.ApiClients;

public static class DofocusClient
{
    public static DofocusServersClient Servers() => new(new Uri("https://dofocus.fr/api/servers/"));
    public static DofocusRunesClient Runes() => new(new Uri("https://dofocus.fr/api/runes/"));
    public static DofocusItemsClient Items() => new(new Uri("https://dofocus.fr/api/items/"));
}