using System.CommandLine;

namespace DofusSharp.DofusDb.Cli.Commands;

public static class CommonOptions
{
    public static readonly Option<bool> Verbose = new("--debug", "-d")
    {
        Description = "Show debugging output.",
        Recursive = true
    };
}
