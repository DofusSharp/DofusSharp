using System.CommandLine;

namespace DofusSharp.DofusDb.Cli.Commands;

public static class CommonOptions
{
    public static readonly Option<bool> Verbose = new("--verbose", "-v")
    {
        Description = "Enables verbose output.",
        Recursive = true
    };
}
