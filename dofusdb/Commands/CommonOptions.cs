using System.CommandLine;

namespace dofusdb.Commands;

static class CommonOptions
{
    public static readonly Option<bool> QuietOption = new("--quiet", "-q")
    {
        Description = "Display less output",
        Recursive = true
    };

    public static readonly Option<bool> DebugOption = new("--debug", "-d")
    {
        Description = "Show debugging output",
        Recursive = true
    };

    public static readonly Option<string> OutputFileOption = new("--output", "-o")
    {
        Description = "File to write the JSON output to. If not specified, the output will be written to the console"
    };
}
