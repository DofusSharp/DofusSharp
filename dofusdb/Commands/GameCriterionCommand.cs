using System.CommandLine;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using Spectre.Console;

namespace dofusdb.Commands;

class GameCriterionCommand(string command, string description, Func<Uri, IDofusDbCriterionClient> clientFactory)
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

    public Command CreateCommand()
    {
        Command result = new(command, description)
        {
            Arguments = { _criterionArgument },
            Options =
            {
                _langOption,
                CommonOptions.OutputFileOption,
                CommonOptions.PrettyPrintOption, CommonOptions.BaseUrlOption
            }
        };
        result.SetAction(async (r, cancellationToken) =>
            {
                string criterion = r.GetRequiredValue(_criterionArgument);
                DofusDbLanguage lang = r.GetValue(_langOption);
                string? outputFile = r.GetValue(CommonOptions.OutputFileOption);
                bool prettyPrint = r.GetValue(CommonOptions.PrettyPrintOption);
                string baseUrl = r.GetRequiredValue(CommonOptions.BaseUrlOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                JsonSerializerOptions options = Utils.BuildJsonSerializerOptions(prettyPrint);
                JsonTypeInfo jsonTypeInfo = options.GetTypeInfo(typeof(DofusDbCriterion));

                IDofusDbCriterionClient client = clientFactory(new Uri(baseUrl));

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
