using System.CommandLine;
using DofusSharp.DofusDb.ApiClients;
using Spectre.Console;

namespace dofusdb.Commands;

class GameVersionCommand(string command, string description, Func<Uri, IDofusDbVersionClient> clientFactory)
{
    public Command CreateCommand()
    {
        Command result = new(command, description) { Options = { CommonOptions.BaseUrlOption, CommonOptions.RequestOption } };
        
        result.SetAction(async (r, cancellationToken) =>
            {
                string baseUrl = r.GetRequiredValue(CommonOptions.BaseUrlOption);
                bool request = r.GetValue(CommonOptions.RequestOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                IDofusDbVersionClient client = clientFactory(new Uri(baseUrl));
                return request ? Query(client) : await ExecuteAsync(client, quiet, cancellationToken);
            }
        );
        
        return result;
    }

    static int Query(IDofusDbVersionClient client)
    {
        Uri query = client.GetVersionRequestUri();
        Console.WriteLine(query.ToString());
        return 0;
    }

    static async Task<int> ExecuteAsync(IDofusDbVersionClient client, bool quiet, CancellationToken cancellationToken)
    {
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
                .StartAsync($"Executing query: {client.GetVersionRequestUri()}...", async _ => version = await client.GetVersionAsync(cancellationToken));
        }

        Console.WriteLine(version);
        return 0;
    }
}
