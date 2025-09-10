using System.CommandLine;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;

namespace DofusSharp.DofusDb.Cli.Commands;

public class TableClientCommand<TResource>(string command, string name, IDofusDbTableClient<TResource> client) where TResource: DofusDbResource
{
    readonly Argument<long> _idArgument = new("id")
    {
        Description = "Unique identifier of the resource.",
        Arity = ArgumentArity.ExactlyOne
    };

    readonly Option<int> _limitOption = new("--limit")
    {
        Description = "Number of results to get. This might lead to multiple requests if the limit exceeds the API's maximum page size."
    };

    readonly Option<int> _skipOption = new("--skip")
    {
        Description = "Number of results to skip."
    };

    readonly Option<string[]> _selectOption = new("--select")
    {
        Description = "Comma separated list of fields to include in the results. If not specified, all fields are included.",
        Arity = ArgumentArity.ZeroOrMore,
        CustomParser = r => r.Tokens.SelectMany(t => t.Value.Split(',')).ToArray()
    };

    readonly Option<string[]> _sortOption = new("--sort")
    {
        Description = "Comma separated list of fields to sorts the results by. Prefix with '-' for descending order.",
        Arity = ArgumentArity.ZeroOrMore,
        CustomParser = r => r.Tokens.SelectMany(t => t.Value.Split(',')).ToArray()
    };

    readonly Option<string> _outputFileOption = new("--output", "-o")
    {
        Description = "File to write the JSON output to. If not specified, the output will be written to the console."
    };

    readonly Option<bool> _prettyPrintOption = new("--pretty-print")
    {
        Description = "Pretty print the JSON output.",
        DefaultValueFactory = _ => false
    };

    readonly Option<string> _baseUrlOption = new("--base")
    {
        Description = "Base URL to use when building the query URL.",
        DefaultValueFactory = _ => client.BaseAddress.ToString()
    };

    public Command CreateCommand() =>
        new(command, $"Query {name.ToLowerInvariant()} resources from DofusDB.")
        {
            CreateSearchCommand(),
            CreateSearchQueryCommand(),
            CreateGetCommand(),
            CreateCountCommand()
        };

    Command CreateSearchCommand()
    {
        Command result = new("search", $"Search for {name.ToLowerInvariant()}.")
            { Options = { _limitOption, _skipOption, _selectOption, _sortOption, _outputFileOption, _prettyPrintOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                int limit = r.GetValue(_limitOption);
                int skip = r.GetValue(_skipOption);
                string[]? select = r.GetValue(_selectOption);
                string[]? sort = r.GetValue(_sortOption);
                string? outputFile = r.GetValue(_outputFileOption);
                bool prettyPrint = r.GetValue(_prettyPrintOption);

                JsonSerializerOptions options = BuildJsonSerializerOptions(prettyPrint);
                JsonTypeInfo jsonTypeInfo = options.GetTypeInfo(typeof(IReadOnlyList<TResource>));

                IReadOnlyList<TResource> results = await client
                    .MultiQuerySearchAsync(new DofusDbSearchQuery { Limit = limit, Skip = skip, Select = select ?? [], Sort = BuildSortQuery(sort) }, cancellationToken)
                    .ToListAsync(cancellationToken);
                string serialized = JsonSerializer.Serialize(results, jsonTypeInfo);

                await WriteToStdoutOrFile(outputFile, serialized, cancellationToken);
            }
        );

        return result;
    }

    Command CreateSearchQueryCommand()
    {
        Command result = new("search-query", $"Build the search query for {name.ToLowerInvariant()}.")
            { Options = { _limitOption, _skipOption, _selectOption, _sortOption, _baseUrlOption } };

        result.SetAction(r =>
            {
                int limit = r.GetValue(_limitOption);
                int skip = r.GetValue(_skipOption);
                string[]? select = r.GetValue(_selectOption);
                string[]? sort = r.GetValue(_sortOption);
                string baseUrl = r.GetRequiredValue(_baseUrlOption);

                DofusDbSearchQuery query = new() { Limit = limit, Skip = skip, Select = select ?? [], Sort = BuildSortQuery(sort) };
                query.ToQueryString();

                Console.WriteLine("{0}?{1}", baseUrl, query.ToQueryString());
            }
        );

        return result;
    }

    Command CreateGetCommand()
    {
        Command result = new("get", $"Get {name.ToLowerInvariant()} by id.") { Arguments = { _idArgument }, Options = { _outputFileOption, _prettyPrintOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                long id = r.GetRequiredValue(_idArgument);
                string? outputFile = r.GetValue(_outputFileOption);
                bool prettyPrint = r.GetValue(_prettyPrintOption);

                JsonSerializerOptions options = BuildJsonSerializerOptions(prettyPrint);
                JsonTypeInfo jsonTypeInfo = options.GetTypeInfo(typeof(TResource));

                TResource resource = await client.GetAsync(id, cancellationToken);
                string serialized = JsonSerializer.Serialize(resource, jsonTypeInfo);

                await WriteToStdoutOrFile(outputFile, serialized, cancellationToken);
            }
        );

        return result;
    }

    Command CreateCountCommand()
    {
        Command result = new("count", $"Count {name.ToLowerInvariant()}.");

        result.SetAction(async (r, cancellationToken) =>
            {
                int results = await client.CountAsync(cancellationToken);
                Console.WriteLine(results);
            }
        );

        return result;
    }

    static JsonSerializerOptions BuildJsonSerializerOptions(bool prettyPrint) =>
        new(JsonSerializerDefaults.Web)
            { TypeInfoResolver = SourceGenerationContext.Default, WriteIndented = prettyPrint, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault };

    static Dictionary<string, DofusDbSearchQuerySortOrder> BuildSortQuery(string[]? sort) =>
        sort?.ToDictionary(s => s, s => s.StartsWith('-') ? DofusDbSearchQuerySortOrder.Descending : DofusDbSearchQuerySortOrder.Ascending) ?? [];

    static async Task WriteToStdoutOrFile(string? outputFile, string content, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(outputFile))
        {
            Console.WriteLine(content);
        }
        else
        {
            string? directory = Path.GetDirectoryName(outputFile);
            if (directory is not null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await File.WriteAllTextAsync(outputFile, content, cancellationToken);
        }
    }
}
