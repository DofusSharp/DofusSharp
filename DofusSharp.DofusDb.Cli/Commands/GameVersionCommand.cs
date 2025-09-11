using System.CommandLine;
using DofusSharp.DofusDb.ApiClients;

namespace DofusSharp.DofusDb.Cli.Commands;

public class GameVersionCommand(Func<Uri, IDofusDbVersionClient> clientFactory, Uri defaultUrl)
{
    readonly Option<string> _baseUrlOption = new("--base")
    {
        Description = "Base URL to use when building the query URL",
        DefaultValueFactory = _ => defaultUrl.ToString()
    };

    public Command CreateCommand()
    {
        Command result = new("game-version", "Get the version of the game corresponding to the data") { Options = { _baseUrlOption } };
        result.SetAction(async (r, cancellationToken) =>
            {
                string? baseUrl = r.GetValue(_baseUrlOption);

                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbVersionClient client = clientFactory(url);
                Version version = await client.GetVersionAsync(cancellationToken);

                Console.WriteLine(version);
            }
        );
        return result;
    }
}
