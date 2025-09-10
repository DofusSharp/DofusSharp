using System.CommandLine;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models;

namespace DofusSharp.DofusDb.Cli.Commands;

public class TableClientCommand<TResource>(string command, string name, IDofusDbTableClient<TResource> client) where TResource: DofusDbResource
{
    public Command CreateCommand() =>
        new(command, $"Query {name.ToLowerInvariant()} resources from DofusDB.")
        {
            CreateSearchCommand(),
            CreateGetCommand(),
            CreateCountCommand()
        };

    Command CreateSearchCommand() => new("search", $"Search for {name.ToLowerInvariant()}.");

    Command CreateGetCommand()
    {
        Argument<long> idArgument = new("id")
        {
            Description = "The unique identifier of the resource.",
            Arity = ArgumentArity.ExactlyOne,
            HelpName = "ID"
        };

        Command result = new("get", $"Get {name.ToLowerInvariant()} by id.") { Arguments = { idArgument } };

        result.SetAction(async (r, cancellationToken) =>
            {
                JsonSerializerOptions options = new(JsonSerializerDefaults.Web) { TypeInfoResolver = SourceGenerationContext.Default };
                JsonTypeInfo jsonTypeInfo = options.GetTypeInfo(typeof(TResource));

                long id = r.GetValue(idArgument);
                TResource resource = await client.GetAsync(id, cancellationToken);
                string serialized = JsonSerializer.Serialize(resource, jsonTypeInfo);
                Console.WriteLine(serialized);
            }
        );

        return result;
    }

    Command CreateCountCommand() => new("count", $"Count {name.ToLowerInvariant()}.");
}
