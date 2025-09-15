using System.CommandLine;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Common;
using Spectre.Console;

namespace dofusdb.Commands;

class ImageClientCommand<TId>(string command, string name, Func<Uri, IDofusDbImagesClient<TId>> clientFactory)
{
    readonly Argument<TId> _idArgument = new("id")
    {
        Description = "Unique identifier of the resource",
        Arity = ArgumentArity.ExactlyOne
    };

    public Command CreateCommand() =>
        new(command, $"{name} client")
        {
            CreateGetCommand()
        };

    Command CreateGetCommand()
    {
        Command result = new("get", $"Get {name.ToLowerInvariant()} by id")
            { Arguments = { _idArgument }, Options = { CommonOptions.OutputImageOption, CommonOptions.BaseUrlOption, CommonOptions.RequestOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                TId id = r.GetRequiredValue(_idArgument);
                string? outputFile = r.GetValue(CommonOptions.OutputImageOption);
                string baseUrl = r.GetRequiredValue(CommonOptions.BaseUrlOption);
                bool request = r.GetValue(CommonOptions.RequestOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                IDofusDbImagesClient<TId> client = clientFactory(new Uri(baseUrl));
                return request ? WriteImageRequest(client, id, outputFile) : await ExecuteImageRequestAsync(client, id, outputFile, quiet, cancellationToken);
            }
        );

        return result;
    }

    static int WriteImageRequest(IDofusDbImagesClient<TId> client, TId id, string? outputFile)
    {
        Uri query = client.GetImageRequestUri(id);
        using Stream stream = Utils.GetOutputStream(outputFile);
        using StreamWriter textWriter = new(stream);
        textWriter.WriteLine(query.ToString());
        return 0;
    }

    async Task<int> ExecuteImageRequestAsync(IDofusDbImagesClient<TId> client, TId id, string? outputFile, bool quiet, CancellationToken cancellationToken)
    {
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
                .StartAsync($"Executing query: {client.GetImageRequestUri(id)}...", async _ => image = await client.GetImageAsync(id, cancellationToken));
        }

        await using Stream stream = GetOutputStream(client, id, outputFile);
        await image.CopyToAsync(stream, cancellationToken);

        return 0;
    }

    Stream GetOutputStream(IDofusDbImagesClient<TId> client, TId id, string? outputFile)
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

        return Utils.GetOutputStream(outputFile);
    }
}
