using System.CommandLine;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;
using Spectre.Console;

namespace dofusdb.Commands;

class AlmanaxCommand(string command, string description, Func<Uri, IDofusDbAlmanaxCalendarClient> clientFactory, Uri defaultUrl)
{
    readonly Argument<DateOnly> _dateArgument = new("date")
    {
        Description = "The specific day for which to retrieve the Almanax calendar data (format: MM/DD/YYYY)",
        DefaultValueFactory = _ => DateOnly.FromDateTime(DateTime.Today)
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
        Command result = new(command, description) { Arguments = { _dateArgument }, Options = { CommonOptions.OutputFileOption, _prettyPrintOption, _baseUrlOption } };
        result.SetAction(async (r, cancellationToken) =>
            {
                DateOnly date = r.GetRequiredValue(_dateArgument);
                string? outputFile = r.GetValue(CommonOptions.OutputFileOption);
                bool prettyPrint = r.GetValue(_prettyPrintOption);
                string? baseUrl = r.GetValue(_baseUrlOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                JsonSerializerOptions options = Utils.BuildJsonSerializerOptions(prettyPrint);
                JsonTypeInfo jsonTypeInfo = options.GetTypeInfo(typeof(DofusDbAlmanaxCalendar));

                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbAlmanaxCalendarClient client = clientFactory(url);

                DofusDbAlmanaxCalendar almanax = null!;
                if (quiet)
                {
                    almanax = await client.GetAlmanaxAsync(date, cancellationToken);
                }
                else
                {
                    await AnsiConsole
                        .Status()
                        .Spinner(Spinner.Known.Default)
                        .StartAsync($"Executing query: {client.GetAlmanaxQuery(date)}...", async _ => almanax = await client.GetAlmanaxAsync(date, cancellationToken));
                }

                await using Stream stream = Utils.GetOutputStream(outputFile);
                await JsonSerializer.SerializeAsync(stream, almanax, jsonTypeInfo, cancellationToken);
            }
        );
        return result;
    }
}
