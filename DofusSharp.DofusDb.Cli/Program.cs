// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using System.CommandLine.Parsing;
using DofusSharp.DofusDb.Cli.Commands;

RootCommand rootCommand = new("A command line interface for DofusDB.")
{
    new ItemsCommand().CreateCommand()
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
