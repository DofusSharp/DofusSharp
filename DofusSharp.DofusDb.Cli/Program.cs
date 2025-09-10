// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using System.CommandLine.Parsing;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.Cli.Commands;

Uri referrer = new("https://github.com/ismailbennani/DofusSharp/tree/main/DofusSharp.DofusDb.Cli");
#if DEBUG
IDofusDbClientsFactory factory = DofusDbClient.Beta(referrer);
#else
IDofusDbClientsFactory factory = DofusDbClient.Production(referrer);
#endif

RootCommand rootCommand = new("A command line interface for DofusDB.")
{
    new TableClientCommand<DofusDbItem>("items", "Items", factory.Items()).CreateCommand()
};

ParseResult parseResult = rootCommand.Parse(args);

if (parseResult.Errors.Count != 0)
{
    foreach (ParseError parseError in parseResult.Errors)
    {
        Console.Error.WriteLine(parseError.Message);
    }
    return 1;
}

return await parseResult.InvokeAsync();
