using System.CommandLine;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Common;
using Spectre.Console;

namespace dofusdb.Commands;

class ImageClientCommand<TId>(string command, string name, Func<Uri, IDofusDbImageClient<TId>> clientFactory, Uri defaultUrl)
{
    readonly Argument<TId> _idArgument = new("id")
    {
        Description = "Unique identifier of the resource",
        Arity = ArgumentArity.ExactlyOne
    };

    readonly Option<string> _outputFileOption = new("--output", "-o")
    {
        Description = "File to write the JSON output to. If not specified, the output will be written to the console"
    };

    readonly Option<string> _baseUrlOption = new("--base")
    {
        Description = "Base URL to use when building the query URL",
        DefaultValueFactory = _ => defaultUrl.ToString()
    };

    public Command CreateCommand() =>
        new(command, $"{name} client")
        {
            CreateGetCommand()
        };

    Command CreateGetCommand()
    {
        Command result = new("get", $"Get {name.ToLowerInvariant()} by id") { Arguments = { _idArgument }, Options = { _outputFileOption, _baseUrlOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                TId id = r.GetRequiredValue(_idArgument);
                string? outputFile = r.GetValue(_outputFileOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);
                string? baseUrl = r.GetValue(_baseUrlOption);
                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbImageClient<TId> client = clientFactory(url);

                Stream image = null!;
                if (quiet)
                {
                    image = await client.GetImageAsync(id, cancellationToken);
                }
                else
                {
                    await AnsiConsole
                        .Status()
                        .Spinner(Spinner.Known.Default)
                        .StartAsync($"Executing query: {client.GetImageQuery(id)}...", async _ => image = await client.GetImageAsync(id, cancellationToken));
                }

                await using Stream stream = GetOutputStream(client, id, outputFile);
                await image.CopyToAsync(stream, cancellationToken);
            }
        );

        return result;
    }

    FileStream GetOutputStream(IDofusDbImageClient<TId> client, TId id, string? outputFile)
    {
        if (outputFile == null)
        {
            string extension = client.ImageFormat switch
            {
                ImageFormat.Jpeg => ".jpg",
                ImageFormat.Png => ".png",
                _ => ""
            };
            outputFile = $"{command}-{id}{extension}";
        }

        string? directory = Path.GetDirectoryName(outputFile);
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return File.Create(outputFile);
    }
}
