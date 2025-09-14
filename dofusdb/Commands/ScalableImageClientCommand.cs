using System.CommandLine;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Common;
using Spectre.Console;

namespace dofusdb.Commands;

class ScalableImageClientCommand<TId>(string command, string name, Func<Uri, IDofusDbScalableImagesClient<TId>> clientFactory)
{
    readonly Argument<TId> _idArgument = new("id")
    {
        Description = "Unique identifier of the resource",
        Arity = ArgumentArity.ExactlyOne
    };

    readonly Option<DofusDbImageScale> _scaleOption = new("--scale")
    {
        Description = "Scale of the image to fetch",
        DefaultValueFactory = _ => DofusDbImageScale.Full
    };

    public Command CreateCommand() =>
        new(command, $"{name} client")
        {
            CreateGetCommand()
        };

    Command CreateGetCommand()
    {
        Command result = new("get", $"Get {name.ToLowerInvariant()} by id")
            { Arguments = { _idArgument }, Options = { _scaleOption, CommonOptions.OutputImageOption, CommonOptions.BaseUrlOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                TId id = r.GetRequiredValue(_idArgument);
                DofusDbImageScale scale = r.GetValue(_scaleOption);
                string? outputFile = r.GetValue(CommonOptions.OutputImageOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);
                string baseUrl = r.GetRequiredValue(CommonOptions.BaseUrlOption);

                IDofusDbScalableImagesClient<TId> client = clientFactory(new Uri(baseUrl));

                Stream image = null!;
                if (quiet)
                {
                    image = await client.GetImageAsync(id, scale, cancellationToken);
                }
                else
                {
                    await AnsiConsole
                        .Status()
                        .Spinner(Spinner.Known.Default)
                        .StartAsync($"Executing query: {client.GetImageQuery(id, scale)}...", async _ => image = await client.GetImageAsync(id, scale, cancellationToken));
                }

                await using Stream stream = GetOutputStream(client, id, scale, outputFile);
                await image.CopyToAsync(stream, cancellationToken);
            }
        );

        return result;
    }

    FileStream GetOutputStream(IDofusDbImagesClient<TId> client, TId id, DofusDbImageScale scale, string? outputFile)
    {
        if (outputFile == null)
        {
            string extension = client.ImageFormat switch
            {
                ImageFormat.Jpeg => ".jpg",
                ImageFormat.Png => ".png",
                _ => ""
            };
            outputFile = $"{command}-{id}-{scale.ToString().ToLowerInvariant()}{extension}";
        }

        string? directory = Path.GetDirectoryName(outputFile);
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return File.Create(outputFile);
    }
}
