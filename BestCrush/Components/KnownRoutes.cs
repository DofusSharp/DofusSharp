using DofusSharp.Dofocus.ApiClients.Models.Servers;

namespace BestCrush.Components;

static class KnownRoutes
{
    public static string Splash() => "/";

    public static string Servers() => "/servers";
    public static string Server(string server) => $"/servers/{server}";
    public static string Server(DofocusServer server) => Server(server.Name);
}
