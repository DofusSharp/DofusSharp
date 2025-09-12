using System.CommandLine;
using DofusSharp.DofusDb.ApiClients;
using Spectre.Console;

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
                bool quiet = r.GetValue(CommonOptions.Quiet);

                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbVersionClient client = clientFactory(url);

                Version version = null!;
                if (quiet)
                {
                    version = await client.GetVersionAsync(cancellationToken);
                }
                else
                {
                    await AnsiConsole
                        .Status()
                        .Spinner(Spinner.Known.Default)
                        .StartAsync($"Executing query: {client.GetVersionQuery()}...", async _ => version = await client.GetVersionAsync(cancellationToken));
                }

                Console.WriteLine(version);
            }
        );
        return result;
    }
}
