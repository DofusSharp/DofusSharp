using System.CommandLine;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;
using Spectre.Console;

namespace dofusdb.Commands;

class AlmanaxCommand(string command, string description, Func<Uri, IDofusDbAlmanaxCalendarClient> clientFactory)
{
    readonly Argument<DateOnly> _dateArgument = new("date")
    {
        Description = "The specific day for which to retrieve the Almanax calendar data (format: MM/DD/YYYY)",
        DefaultValueFactory = _ => DateOnly.FromDateTime(DateTime.Today)
    };

    public Command CreateCommand()
    {
        Command result = new(command, description)
        {
            Arguments = { _dateArgument }, Options = { CommonOptions.OutputFileOption, CommonOptions.PrettyPrintOption, CommonOptions.BaseUrlOption, CommonOptions.RequestOption }
        };

        result.SetAction(async (r, token) =>
            {
                DateOnly date = r.GetRequiredValue(_dateArgument);
                string? outputFile = r.GetValue(CommonOptions.OutputFileOption);
                bool prettyPrint = r.GetValue(CommonOptions.PrettyPrintOption);
                string baseUrl = r.GetRequiredValue(CommonOptions.BaseUrlOption);
                bool request = r.GetValue(CommonOptions.RequestOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                IDofusDbAlmanaxCalendarClient client = clientFactory(new Uri(baseUrl));
                return request ? Query(client, date, outputFile) : await ExecuteAsync(client, date, outputFile, prettyPrint, quiet, token);
            }
        );

        return result;
    }

    static int Query(IDofusDbAlmanaxCalendarClient client, DateOnly date, string? outputFile)
    {
        Uri query = client.GetAlmanaxRequestUri(date);
        using Stream stream = Utils.GetOutputStream(outputFile);
        using StreamWriter textWriter = new(stream);
        textWriter.WriteLine(query.ToString());
        return 0;
    }

    static async Task<int> ExecuteAsync(IDofusDbAlmanaxCalendarClient client, DateOnly date, string? outputFile, bool prettyPrint, bool quiet, CancellationToken cancellationToken)
    {
        JsonSerializerOptions options = Utils.BuildJsonSerializerOptions(prettyPrint);
        JsonTypeInfo jsonTypeInfo = options.GetTypeInfo(typeof(DofusDbAlmanaxCalendar));

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
                .StartAsync($"Executing query: {client.GetAlmanaxRequestUri(date)}...", async _ => almanax = await client.GetAlmanaxAsync(date, cancellationToken));
        }

        await using Stream stream = Utils.GetOutputStream(outputFile);
        await JsonSerializer.SerializeAsync(stream, almanax, jsonTypeInfo, cancellationToken);

        return 0;
    }
}
