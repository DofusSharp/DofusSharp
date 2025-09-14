using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Text.RegularExpressions;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Search;
using Spectre.Console;

namespace dofusdb.Commands;

partial class TableClientCommand<TResource>(string command, string name, Func<Uri, IDofusDbTableClient<TResource>> clientFactory, Uri defaultUrl) where TResource: DofusDbResource
{
    readonly Argument<long> _idArgument = new("id")
    {
        Description = "Unique identifier of the resource",
        Arity = ArgumentArity.ExactlyOne
    };

    readonly Option<int> _limitOption = new("--limit")
    {
        Description = "Maximum number of results to retrieve. If the value exceeds the API’s maximum page size, multiple requests will be performed",
        DefaultValueFactory = _ => 10
    };

    readonly Option<bool> _allOption = new("--all", "-a")
    {
        Description = "Fetch all available results, ignoring --limit. The --skip option is still honored when this option is set"
    };

    readonly Option<int> _skipOption = new("--skip")
    {
        Description = "Number of results to skip",
        DefaultValueFactory = _ => 0
    };

    readonly Option<string[]> _selectOption = new("--select")
    {
        Description = "Comma separated list of fields to include in the results. If not specified, all fields are included [example: --select \"id,name.fr,level\"]",
        Arity = ArgumentArity.ZeroOrMore,
        CustomParser = r => r.Tokens.SelectMany(t => t.Value.Split(',')).ToArray()
    };

    readonly Option<Dictionary<string, DofusDbSearchQuerySortOrder>> _sortOption = new("--sort")
    {
        Description = "Comma separated list of fields to sorts the results by. Prefix with '-' for descending order [example: --sort \"-level,name.fr\"]",
        Arity = ArgumentArity.ZeroOrMore,
        CustomParser = ParseSortOption
    };

    readonly Option<IReadOnlyList<DofusDbSearchPredicate>> _filterOption = new("--filter")
    {
        Description = "Comma separated list of predicates to filter the results by. "
                      + "Each predicate is made of the name of the field, an operator (=, !=, <, <=, >, >=) and the value. "
                      + "Multiple values can be separated by '|' for '=' operator (in) and for '!=' operator (not in) to match any of the values "
                      + "[example: --filter \"level>=10,name.fr=Razielle|Goultard\"]",
        Arity = ArgumentArity.ZeroOrMore,
        CustomParser = ParseFilterOption
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
        {
            Options =
            {
                _allOption, _limitOption, _skipOption, _selectOption, _sortOption, _filterOption, CommonOptions.OutputFileOption,
                CommonOptions.PrettyPrintOption, _baseUrlOption
            }
        };

        result.SetAction(async (r, cancellationToken) =>
            {
                bool all = r.GetValue(_allOption);
                int? limit = r.GetValue(_limitOption);
                int? skip = r.GetValue(_skipOption);
                string[]? select = r.GetValue(_selectOption);
                Dictionary<string, DofusDbSearchQuerySortOrder>? sort = r.GetValue(_sortOption);
                IReadOnlyList<DofusDbSearchPredicate>? filter = r.GetValue(_filterOption);
                string? outputFile = r.GetValue(CommonOptions.OutputFileOption);
                bool prettyPrint = r.GetValue(CommonOptions.PrettyPrintOption);
                string? baseUrl = r.GetValue(_baseUrlOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                JsonSerializerOptions options = Utils.BuildJsonSerializerOptions(prettyPrint);
                JsonTypeInfo jsonTypeInfo = options.GetTypeInfo(typeof(IReadOnlyList<TResource>));

                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbTableClient<TResource> client = clientFactory(url);

                DofusDbSearchQuery query = new() { Limit = all ? null : limit, Skip = skip, Select = select ?? [], Sort = sort ?? [], Predicates = filter ?? [] };
                IReadOnlyList<TResource> results = null!;
                if (quiet)
                {
                    results = await client.MultiQuerySearchAsync(query, cancellationToken).ToListAsync(cancellationToken);
                }
                else
                {
                    await AnsiConsole
                        .Progress()
                        .AutoRefresh(false)
                        .Columns(new SpinnerColumn(), new TaskDescriptionColumn(), new ProgressBarColumn(), new PercentageColumn(), new RemainingTimeColumn())
                        .StartAsync(async ctx =>
                            {
                                ProgressTask tsk = ctx.AddTask("Fetching resources").IsIndeterminate();

                                ProgressSync<DofusDbTableClientExtensions.MultiSearchQueryProgress>? progress = quiet
                                    ? null
                                    : new ProgressSync<DofusDbTableClientExtensions.MultiSearchQueryProgress>(p =>
                                        {
                                            switch (p)
                                            {
                                                case DofusDbTableClientExtensions.MultiSearchCurrentCount currentCount:
                                                {
                                                    if (currentCount.AlreadyFetched == currentCount.TotalToFetch)
                                                    {
                                                        tsk
                                                            .IsIndeterminate(false)
                                                            .Value(currentCount.AlreadyFetched)
                                                            .MaxValue(currentCount.TotalToFetch)
                                                            .Description($"[{currentCount.AlreadyFetched}/{currentCount.TotalToFetch}] Done fetching resources".EscapeMarkup());
                                                        tsk.StopTask();
                                                    }
                                                    else
                                                    {
                                                        tsk.IsIndeterminate(false).Value(currentCount.AlreadyFetched).MaxValue(currentCount.TotalToFetch);
                                                    }
                                                    ctx.Refresh();
                                                    break;
                                                }

                                                case DofusDbTableClientExtensions.MultiSearchNextQuery nextQuery:
                                                    tsk.Description($"[{tsk.Value}/{tsk.MaxValue}] {client.SearchQuery(nextQuery.NextQuery)}...".EscapeMarkup());
                                                    ctx.Refresh();
                                                    break;
                                            }
                                        }
                                    );

                                results = await client.MultiQuerySearchAsync(query, progress, cancellationToken).ToListAsync(cancellationToken);
                            }
                        );
                }

                await using Stream stream = Utils.GetOutputStream(outputFile);
                await JsonSerializer.SerializeAsync(stream, results, jsonTypeInfo, cancellationToken);
            }
        );

        return result;
    }

    Command CreateGetCommand()
    {
        Command result = new("get", $"Get {name.ToLowerInvariant()} by id")
            { Arguments = { _idArgument }, Options = { CommonOptions.OutputFileOption, CommonOptions.PrettyPrintOption, _baseUrlOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                long id = r.GetRequiredValue(_idArgument);
                string? outputFile = r.GetValue(CommonOptions.OutputFileOption);
                bool prettyPrint = r.GetValue(CommonOptions.PrettyPrintOption);
                string? baseUrl = r.GetValue(_baseUrlOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                JsonSerializerOptions options = Utils.BuildJsonSerializerOptions(prettyPrint);
                JsonTypeInfo jsonTypeInfo = options.GetTypeInfo(typeof(TResource));

                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbTableClient<TResource> client = clientFactory(url);

                TResource resource = null!;
                if (quiet)
                {
                    resource = await client.GetAsync(id, cancellationToken);
                }
                else
                {
                    await AnsiConsole
                        .Status()
                        .Spinner(Spinner.Known.Default)
                        .StartAsync($"Executing query: {client.GetQuery(id)}...", async _ => resource = await client.GetAsync(id, cancellationToken));
                }


                await using Stream stream = Utils.GetOutputStream(outputFile);
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
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbTableClient<TResource> client = clientFactory(url);

                if (!quiet)
                {
                    await Console.Error.WriteLineAsync($"Executing query: {client.CountQuery(filter ?? [])}...");
                }

                int results = 0;
                if (quiet)
                {
                    results = await client.CountAsync(filter ?? [], cancellationToken);
                }
                else
                {
                    await AnsiConsole
                        .Status()
                        .Spinner(Spinner.Known.Default)
                        .StartAsync($"Executing query: {client.CountQuery(filter ?? [])}...", async _ => results = await client.CountAsync(filter ?? [], cancellationToken));
                }

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
