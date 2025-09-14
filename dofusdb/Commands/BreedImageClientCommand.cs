using System.CommandLine;
using DofusSharp.DofusDb.ApiClients;
using Spectre.Console;

namespace dofusdb.Commands;

class BreedImageClientCommand(string command, Func<Uri, IDofusDbBreedImagesClient> clientFactory, Uri defaultUrl)
{
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
        new(command, "Breed images client")
        {
            CreateSymbolCommand(),
            CreateLogoCommand(),
            CreateHeadCommand()
        };

    Command CreateSymbolCommand()
    {
        Argument<long> symbolIdArgument = new("symbol-id")
        {
            Description = "The unique identifier of the breed symbol to retrieve. Symbol ID = Breed ID)",
            Arity = ArgumentArity.ExactlyOne
        };

        Command result = new("symbol", "Retrieve the symbol image for a breed using its ID") { Arguments = { symbolIdArgument }, Options = { _outputFileOption, _baseUrlOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                long id = r.GetRequiredValue(symbolIdArgument);
                string? outputFile = r.GetValue(_outputFileOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);
                string? baseUrl = r.GetValue(_baseUrlOption);
                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbBreedImagesClient client = clientFactory(url);

                Stream image = null!;
                if (quiet)
                {
                    image = await client.GetSymbolAsync(id, cancellationToken);
                }
                else
                {
                    await AnsiConsole
                        .Status()
                        .Spinner(Spinner.Known.Default)
                        .StartAsync($"Executing query: {client.GetSymbolQuery(id)}...", async _ => image = await client.GetSymbolAsync(id, cancellationToken));
                }

                await using Stream stream = GetOutputStream(id, "symbol", outputFile);
                await image.CopyToAsync(stream, cancellationToken);
            }
        );

        return result;
    }

    Command CreateLogoCommand()
    {
        Argument<long> logoIdArgument = new("logo-id")
        {
            Description = "The unique identifier of the breed logo to retrieve. Logo ID = Breed ID",
            Arity = ArgumentArity.ExactlyOne
        };

        Command result = new("logo", "Retrieve the logo image for a breed using its ID") { Arguments = { logoIdArgument }, Options = { _outputFileOption, _baseUrlOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                long id = r.GetRequiredValue(logoIdArgument);
                string? outputFile = r.GetValue(_outputFileOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);
                string? baseUrl = r.GetValue(_baseUrlOption);
                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbBreedImagesClient client = clientFactory(url);

                Stream image = null!;
                if (quiet)
                {
                    image = await client.GetLogoAsync(id, cancellationToken);
                }
                else
                {
                    await AnsiConsole
                        .Status()
                        .Spinner(Spinner.Known.Default)
                        .StartAsync($"Executing query: {client.GetLogoQuery(id)}...", async _ => image = await client.GetLogoAsync(id, cancellationToken));
                }

                await using Stream stream = GetOutputStream(id, "logo", outputFile);
                await image.CopyToAsync(stream, cancellationToken);
            }
        );

        return result;
    }

    Command CreateHeadCommand()
    {
        Argument<long> headIdArgument = new("head-id")
        {
            Description = "The unique identifier of the breed head to retrieve. Male head ID = breed ID × 10, female head ID = breed ID × 10 + 1",
            Arity = ArgumentArity.ExactlyOne
        };

        Command result = new("head", "Retrieve the head image for a breed using its ID") { Arguments = { headIdArgument }, Options = { _outputFileOption, _baseUrlOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                long id = r.GetRequiredValue(headIdArgument);
                string? outputFile = r.GetValue(_outputFileOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);
                string? baseUrl = r.GetValue(_baseUrlOption);
                Uri url = baseUrl is not null ? new Uri(baseUrl) : defaultUrl;
                IDofusDbBreedImagesClient client = clientFactory(url);

                Stream image = null!;
                if (quiet)
                {
                    image = await client.GetHeadAsync(id, cancellationToken);
                }
                else
                {
                    await AnsiConsole
                        .Status()
                        .Spinner(Spinner.Known.Default)
                        .StartAsync($"Executing query: {client.GetHeadQuery(id)}...", async _ => image = await client.GetHeadAsync(id, cancellationToken));
                }

                await using Stream stream = GetOutputStream(id, "head", outputFile);
                await image.CopyToAsync(stream, cancellationToken);
            }
        );

        return result;
    }

    FileStream GetOutputStream(long id, string prefix, string? outputFile)
    {
        if (outputFile == null)
        {
            outputFile = $"{command}-{prefix}-{id}.png";
        }

        string? directory = Path.GetDirectoryName(outputFile);
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return File.Create(outputFile);
    }
}
