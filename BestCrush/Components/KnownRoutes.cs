using DofusSharp.Dofocus.ApiClients.Models.Servers;

namespace BestCrush.Components;

static class KnownRoutes
{
    public static string Splash() => "/";

    public static string Servers() => "/servers";
    public static string Server(string server) => $"/servers/{server}";
    public static string Server(DofocusServer server) => Server(server.Name);

    public static string Crush(string server) => $"/servers/{server}/crush";
    public static string Crush(DofocusServer server) => Crush(server.Name);

    public static string SalesHouse(string server) => $"/servers/{server}/sales";
    public static string SalesHouse(DofocusServer server) => SalesHouse(server.Name);
}
