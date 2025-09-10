using System.CommandLine;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Models.Items;

namespace DofusSharp.DofusDb.Cli.Commands;

public abstract class TableClientCommand<TResource> where TResource: DofusDbResource
{
    public abstract string Command { get; }
    public abstract string Name { get; }

    public Command CreateCommand() =>
        new(Name, $"Query {Name.ToLowerInvariant()} resources from DofusDB.")
        {
            CreateSearchCommand(),
            CreateGetCommand(),
            CreateCountCommand()
        };

    Command CreateSearchCommand() => new("search", $"Search for {Name.ToLowerInvariant()}.");

    Command CreateGetCommand() =>
        new("get", $"Get {Name.ToLowerInvariant()} by id.")
        {
            Arguments =
            {
                new Argument<long>("id")
                {
                    Description = "The unique identifier of the resource.",
                    Arity = ArgumentArity.ExactlyOne,
                    HelpName = "ID"
                }
            }
        };

    Command CreateCountCommand() => new("count", $"Count {Name.ToLowerInvariant()}.");
}

public class ItemsCommand : TableClientCommand<DofusDbItem>
{
    public override string Command => "items";
    public override string Name => "Items";
}
