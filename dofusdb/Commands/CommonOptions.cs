using System.CommandLine;
using DofusSharp.DofusDb.ApiClients;

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
        Description = "File to write the JSON output to. If not specified, the output will be written to stdout"
    };

    public static readonly Option<string> OutputImageOption = new("--output", "-o")
    {
        Description = "File to write the JSON output to. If not specified, an arbitrary file name will be used"
    };

    public static readonly Option<bool> PrettyPrintOption = new("--pretty-print")
    {
        Description = "Pretty print the JSON output",
        DefaultValueFactory = _ => false
    };

    public static readonly Option<string> BaseUrlOption = new("--base")
    {
        Description = "Base URL to use when building the query URL",
#if DEBUG
        DefaultValueFactory = _ => DofusDbClient.BetaUri.ToString()
#else
        DefaultValueFactory = _ => DofusDbClient.ProductionUri.ToString()
#endif
    };

    public static readonly Option<bool> RequestOption = new("--request-only")
    {
        Description = "Output the HTTP request URL that would be executed, without actually executing it",
        DefaultValueFactory = _ => false
    };
}
