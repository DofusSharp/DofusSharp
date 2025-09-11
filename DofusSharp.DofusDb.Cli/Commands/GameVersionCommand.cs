using System.CommandLine;
using DofusSharp.DofusDb.ApiClients;

namespace DofusSharp.DofusDb.Cli.Commands;

public class GameVersionCommand(IDofusDbVersionClient client)
{
    public Command CreateCommand()
    {
        Command result = new("game-version", "Get the version of the game corresponding to the data.");
        result.SetAction(async (r, cancellationToken) =>
            {
                Version version = await client.GetVersionAsync(cancellationToken);
                Console.WriteLine(version);
            }
        );
        return result;
    }
}
