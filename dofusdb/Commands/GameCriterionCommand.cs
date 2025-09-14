using System.CommandLine;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using Spectre.Console;

namespace dofusdb.Commands;

class GameCriterionCommand(string command, string description, Func<Uri, IDofusDbCriterionClient> clientFactory, Uri defaultUrl)
{
    readonly Argument<string> _criterionArgument = new("criterion")
    {
        Description = "Criterion to parse",
        Arity = ArgumentArity.ExactlyOne
    };

    readonly Option<DofusDbLanguage> _langOption = new("--lang")
    {
        Description = "Language to request"
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

    public Command CreateCommand()
    {
        Command result = new(command, description)
        {
            Arguments = { _criterionArgument },
            Options =
            {
                _langOption,
                CommonOptions.OutputFileOption,
                _prettyPrintOption, _baseUrlOption
            }
        };
        result.SetAction(async (r, cancellationToken) =>
            {
                string criterion = r.GetRequiredValue(_criterionArgument);
                DofusDbLanguage lang = r.GetValue(_langOption);
                string? outputFile = r.GetValue(CommonOptions.OutputFileOption);
                bool prettyPrint = r.GetValue(_prettyPrintOption);
                string? baseUrl = r.GetValue(_baseUrlOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                JsonSerializerOptions options = Utils.BuildJsonSerializerOptions(prettyPrint);
                JsonTypeInfo jsonTypeInfo = options.GetTypeInfo(typeof(DofusDbCriterion));

                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbCriterionClient client = clientFactory(url);

                DofusDbCriterion? parsedCriterion = null!;
                if (quiet)
                {
                    parsedCriterion = await client.ParseCriterionAsync(criterion, lang, cancellationToken);
                }
                else
                {
                    await AnsiConsole
                        .Status()
                        .Spinner(Spinner.Known.Default)
                        .StartAsync(
                            $"Executing query: {client.ParseCriterionQuery(criterion, lang)}...",
                            async _ => parsedCriterion = await client.ParseCriterionAsync(criterion, lang, cancellationToken)
                        );
                }

                await using Stream stream = Utils.GetOutputStream(outputFile);
                await JsonSerializer.SerializeAsync(stream, parsedCriterion, jsonTypeInfo, cancellationToken);
            }
        );
        return result;
    }
}
