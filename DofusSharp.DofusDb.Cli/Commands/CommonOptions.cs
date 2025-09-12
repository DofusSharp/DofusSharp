using System.CommandLine;

namespace DofusSharp.DofusDb.Cli.Commands;

public static class CommonOptions
{
    public static readonly Option<bool> Quiet = new("--quiet", "-q")
    {
        Description = "Display less output.",
        Recursive = true
    };

    public static readonly Option<bool> Debug = new("--debug", "-d")
    {
        Description = "Show debugging output.",
        Recursive = true
    };
}
