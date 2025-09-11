using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Text.RegularExpressions;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;

namespace DofusSharp.DofusDb.Cli.Commands;

public partial class TableClientCommand<TResource>(string command, string name, Func<Uri, IDofusDbTableClient<TResource>> clientFactory, Uri defaultUrl)
    where TResource: DofusDbResource
{
    readonly Argument<long> _idArgument = new("id")
    {
        Description = "Unique identifier of the resource",
        Arity = ArgumentArity.ExactlyOne
    };

    readonly Option<int?> _limitOption = new("--limit")
    {
        Description = "Number of results to get. This might lead to multiple requests if the limit exceeds the API's maximum page size"
    };

    readonly Option<int?> _skipOption = new("--skip")
    {
        Description = "Number of results to skip"
    };

    readonly Option<string[]> _selectOption = new("--select")
    {
        Description = "Comma separated list of fields to include in the results. If not specified, all fields are included. Example: --select \"id,name.fr,level\"",
        Arity = ArgumentArity.ZeroOrMore,
        CustomParser = r => r.Tokens.SelectMany(t => t.Value.Split(',')).ToArray()
    };

    readonly Option<Dictionary<string, DofusDbSearchQuerySortOrder>> _sortOption = new("--sort")
    {
        Description = "Comma separated list of fields to sorts the results by. Prefix with '-' for descending order. Example: --sort \"-level,name.fr\"",
        Arity = ArgumentArity.ZeroOrMore,
        CustomParser = ParseSortOption
    };

    readonly Option<IReadOnlyList<DofusDbSearchPredicate>> _filterOption = new("--filter")
    {
        Description = "Comma separated list of predicates to filter the results by. "
                      + "Each predicate is made of the name of the field, an operator (=, !=, <, <=, >, >=) and the value. "
                      + "Multiple values can be separated by '|' for '=' operator (in) and for '!=' operator (not in) to match any of the values. "
                      + "Example: --filter \"level>=10,name.fr=Razielle|Goultard\"",
        Arity = ArgumentArity.ZeroOrMore,
        CustomParser = ParseFilterOption
    };

    readonly Option<string> _outputFileOption = new("--output", "-o")
    {
        Description = "File to write the JSON output to. If not specified, the output will be written to the console"
    };

    readonly Option<bool> _prettyPrintOption = new("--pretty-print")
    {
        Description = "Pretty print the JSON output",
        DefaultValueFactory = _ => false
    };

    readonly Option<string> _baseUrlOption = new("--base")
    {
        Description = "Base URL to use when building the query URL",
        DefaultValueFactory = _ => defaultUrl.ToString()
    };

    public Command CreateCommand() =>
        new(command, $"{name} client")
        {
            CreateListCommand(),
            CreateGetCommand(),
            CreateBuildQueryCommand(),
            CreateCountCommand()
        };

    Command CreateListCommand()
    {
        Command result = new("list", $"List all {name.ToLowerInvariant()}")
            { Options = { _limitOption, _skipOption, _selectOption, _sortOption, _filterOption, _outputFileOption, _prettyPrintOption, _baseUrlOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                int? limit = r.GetValue(_limitOption);
                int? skip = r.GetValue(_skipOption);
                string[]? select = r.GetValue(_selectOption);
                Dictionary<string, DofusDbSearchQuerySortOrder>? sort = r.GetValue(_sortOption);
                IReadOnlyList<DofusDbSearchPredicate>? filter = r.GetValue(_filterOption);
                string? outputFile = r.GetValue(_outputFileOption);
                bool prettyPrint = r.GetValue(_prettyPrintOption);
                string? baseUrl = r.GetValue(_baseUrlOption);

                JsonSerializerOptions options = BuildJsonSerializerOptions(prettyPrint);
                JsonTypeInfo jsonTypeInfo = options.GetTypeInfo(typeof(IReadOnlyList<TResource>));

                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbTableClient<TResource> client = clientFactory(url);
                IReadOnlyList<TResource> results = await client
                    .MultiQuerySearchAsync(
                        new DofusDbSearchQuery { Limit = limit, Skip = skip, Select = select ?? [], Sort = sort ?? [], Predicates = filter ?? [] },
                        cancellationToken
                    )
                    .ToListAsync(cancellationToken);

                await using Stream stream = GetOutputStream(outputFile);
                await JsonSerializer.SerializeAsync(stream, results, jsonTypeInfo, cancellationToken);
            }
        );

        return result;
    }

    Command CreateGetCommand()
    {
        Command result = new("get", $"Get {name.ToLowerInvariant()} by id") { Arguments = { _idArgument }, Options = { _outputFileOption, _prettyPrintOption, _baseUrlOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                long id = r.GetRequiredValue(_idArgument);
                string? outputFile = r.GetValue(_outputFileOption);
                bool prettyPrint = r.GetValue(_prettyPrintOption);
                string? baseUrl = r.GetValue(_baseUrlOption);

                JsonSerializerOptions options = BuildJsonSerializerOptions(prettyPrint);
                JsonTypeInfo jsonTypeInfo = options.GetTypeInfo(typeof(TResource));

                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbTableClient<TResource> client = clientFactory(url);
                TResource resource = await client.GetAsync(id, cancellationToken);

                await using Stream stream = GetOutputStream(outputFile);
                await JsonSerializer.SerializeAsync(stream, resource, jsonTypeInfo, cancellationToken);
            }
        );

        return result;
    }

    Command CreateBuildQueryCommand()
    {
        Command result = new("build-query", $"Build the search query for {name.ToLowerInvariant()}")
            { Options = { _limitOption, _skipOption, _selectOption, _sortOption, _filterOption, _baseUrlOption } };

        result.SetAction(r =>
            {
                int? limit = r.GetValue(_limitOption);
                int? skip = r.GetValue(_skipOption);
                string[]? select = r.GetValue(_selectOption);
                Dictionary<string, DofusDbSearchQuerySortOrder>? sort = r.GetValue(_sortOption);
                IReadOnlyList<DofusDbSearchPredicate>? filter = r.GetValue(_filterOption);
                string? baseUrl = r.GetValue(_baseUrlOption);

                DofusDbSearchQuery query = new() { Limit = limit, Skip = skip, Select = select ?? [], Sort = sort ?? [], Predicates = filter ?? [] };
                string queryString = query.ToQueryString();

                if (string.IsNullOrWhiteSpace(queryString))
                {
                    Console.WriteLine(baseUrl);
                }
                else
                {
                    Console.WriteLine("{0}?{1}", baseUrl, queryString);
                }
            }
        );

        return result;
    }

    Command CreateCountCommand()
    {
        Command result = new("count", $"Count {name.ToLowerInvariant()}") { Options = { _filterOption, _baseUrlOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                IReadOnlyList<DofusDbSearchPredicate>? filter = r.GetValue(_filterOption);
                string? baseUrl = r.GetValue(_baseUrlOption);

                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbTableClient<TResource> client = clientFactory(url);
                int results = await client.CountAsync(filter ?? [], cancellationToken);

                Console.WriteLine(results);
            }
        );

        return result;
    }

    static Dictionary<string, DofusDbSearchQuerySortOrder> ParseSortOption(ArgumentResult r) =>
        r
            .Tokens.SelectMany(t => t.Value.Split(','))
            .ToDictionary(s => s.TrimStart('-', '+'), s => s.StartsWith('-') ? DofusDbSearchQuerySortOrder.Descending : DofusDbSearchQuerySortOrder.Ascending);

    static List<DofusDbSearchPredicate> ParseFilterOption(ArgumentResult r)
    {
        List<DofusDbSearchPredicate> result = [];

        foreach (Token token in r.Tokens)
        {
            DofusDbSearchPredicate? predicate = ParseFilter(r, token.Value);
            if (predicate is null)
            {
                continue;
            }

            result.Add(predicate);
        }

        return result;
    }

    static DofusDbSearchPredicate? ParseFilter(ArgumentResult result, string filterStr)
    {
        Regex regex = FilterRegex();
        Match math = regex.Match(filterStr);
        if (!math.Success)
        {
            result.AddError("Invalid filter format. Expected format: {field}{operator}{value}, where operator is one of =, !=, <, <=, >, >=.");
        }

        string field = math.Groups["field"].Value;
        string op = math.Groups["operator"].Value;
        string value = math.Groups["value"].Value;

        switch (op)
        {
            case "=":
            {
                string[] values = value.Split('|');
                if (values.Length == 1)
                {
                    return new DofusDbSearchPredicate.Eq(field, value);
                }
                return new DofusDbSearchPredicate.In(field, values);
            }
            case "!=":
            {
                string[] values = value.Split('|');
                if (values.Length == 1)
                {
                    return new DofusDbSearchPredicate.NotEq(field, value);
                }
                return new DofusDbSearchPredicate.NotIn(field, values);
            }
            case "<":
                return new DofusDbSearchPredicate.LessThan(field, value);
            case "<=":
                return new DofusDbSearchPredicate.LessThanOrEquals(field, value);
            case ">":
                return new DofusDbSearchPredicate.GreaterThan(field, value);
            case ">=":
                return new DofusDbSearchPredicate.GreaterThanOrEqual(field, value);
            default:
                result.AddError($"Invalid operator '{op}' in filter. Expected one of =, !=, <, <=, >, >=.");
                return null;
        }
    }

    static JsonSerializerOptions BuildJsonSerializerOptions(bool prettyPrint) =>
        new(JsonSerializerDefaults.Web)
            { TypeInfoResolver = DofusDbModelsSourceGenerationContext.Default, WriteIndented = prettyPrint, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault };

    static Stream GetOutputStream(string? outputFile)
    {
        if (string.IsNullOrWhiteSpace(outputFile))
        {
            return Console.OpenStandardOutput();
        }

        string? directory = Path.GetDirectoryName(outputFile);
        if (directory is not null && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return File.Create(outputFile);
    }

    [GeneratedRegex(
        "^(?<field>.+)(?<operator>=|!=|<|<=|>|>=)(?<value>.+)$",
        RegexOptions.Compiled
        | RegexOptions.CultureInvariant
        | RegexOptions.IgnoreCase
        | RegexOptions.Singleline
        | RegexOptions.ExplicitCapture
        | RegexOptions.IgnorePatternWhitespace
    )]
    private static partial Regex FilterRegex();
}
