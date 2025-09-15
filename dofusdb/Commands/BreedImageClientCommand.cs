using System.CommandLine;
using DofusSharp.DofusDb.ApiClients;
using Spectre.Console;

namespace dofusdb.Commands;

class BreedImageClientCommand(string command, Func<Uri, IDofusDbBreedImagesClient> clientFactory)
{
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

        Command result = new("symbol", "Retrieve the symbol image for a breed using its ID")
            { Arguments = { symbolIdArgument }, Options = { CommonOptions.OutputImageOption, CommonOptions.BaseUrlOption, CommonOptions.RequestOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                long id = r.GetRequiredValue(symbolIdArgument);
                string? outputFile = r.GetValue(CommonOptions.OutputImageOption);
                string baseUrl = r.GetRequiredValue(CommonOptions.BaseUrlOption);
                bool request = r.GetValue(CommonOptions.RequestOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                IDofusDbBreedImagesClient client = clientFactory(new Uri(baseUrl));
                return request ? WriteSymbolRequest(client, id, outputFile) : await ExecuteSymbolRequestAsync(client, id, outputFile, quiet, cancellationToken);
            }
        );

        return result;
    }

    static int WriteSymbolRequest(IDofusDbBreedImagesClient client, long id, string? outputFile)
    {
        Uri query = client.GetSymbolRequestUri(id);
        using Stream stream = Utils.GetOutputStream(outputFile);
        using StreamWriter textWriter = new(stream);
        textWriter.WriteLine(query.ToString());
        return 0;
    }

    async Task<int> ExecuteSymbolRequestAsync(IDofusDbBreedImagesClient client, long id, string? outputFile, bool quiet, CancellationToken cancellationToken)
    {
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
                .StartAsync($"Executing query: {client.GetSymbolRequestUri(id)}...", async _ => image = await client.GetSymbolAsync(id, cancellationToken));
        }

        await using Stream stream = GetOutputStream(id, "symbol", outputFile);
        await image.CopyToAsync(stream, cancellationToken);

        return 0;
    }

    Command CreateLogoCommand()
    {
        Argument<long> logoIdArgument = new("logo-id")
        {
            Description = "The unique identifier of the breed logo to retrieve. Logo ID = Breed ID",
            Arity = ArgumentArity.ExactlyOne
        };

        Command result = new("logo", "Retrieve the logo image for a breed using its ID")
            { Arguments = { logoIdArgument }, Options = { CommonOptions.OutputImageOption, CommonOptions.BaseUrlOption, CommonOptions.RequestOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                long id = r.GetRequiredValue(logoIdArgument);
                string? outputFile = r.GetValue(CommonOptions.OutputImageOption);
                string baseUrl = r.GetRequiredValue(CommonOptions.BaseUrlOption);
                bool request = r.GetValue(CommonOptions.RequestOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                IDofusDbBreedImagesClient client = clientFactory(new Uri(baseUrl));
                return request ? WriteLogoRequest(client, id, outputFile) : await ExecuteLogoRequestAsync(client, id, outputFile, quiet, cancellationToken);
            }
        );

        return result;
    }

    static int WriteLogoRequest(IDofusDbBreedImagesClient client, long id, string? outputFile)
    {
        Uri query = client.GetLogoRequestUri(id);
        using Stream stream = Utils.GetOutputStream(outputFile);
        using StreamWriter textWriter = new(stream);
        textWriter.WriteLine(query.ToString());
        return 0;
    }

    async Task<int> ExecuteLogoRequestAsync(IDofusDbBreedImagesClient client, long id, string? outputFile, bool quiet, CancellationToken cancellationToken)
    {
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
                .StartAsync($"Executing query: {client.GetLogoRequestUri(id)}...", async _ => image = await client.GetLogoAsync(id, cancellationToken));
        }

        await using Stream stream = GetOutputStream(id, "logo", outputFile);
        await image.CopyToAsync(stream, cancellationToken);

        return 0;
    }

    Command CreateHeadCommand()
    {
        Argument<long> headIdArgument = new("head-id")
        {
            Description = "The unique identifier of the breed head to retrieve. Male head ID = breed ID × 10, female head ID = breed ID × 10 + 1",
            Arity = ArgumentArity.ExactlyOne
        };

        Command result = new("head", "Retrieve the head image for a breed using its ID")
            { Arguments = { headIdArgument }, Options = { CommonOptions.OutputImageOption, CommonOptions.BaseUrlOption, CommonOptions.RequestOption } };

        result.SetAction(async (r, cancellationToken) =>
            {
                long id = r.GetRequiredValue(headIdArgument);
                string? outputFile = r.GetValue(CommonOptions.OutputImageOption);
                string baseUrl = r.GetRequiredValue(CommonOptions.BaseUrlOption);
                bool request = r.GetValue(CommonOptions.RequestOption);
                bool quiet = r.GetValue(CommonOptions.QuietOption);

                IDofusDbBreedImagesClient client = clientFactory(new Uri(baseUrl));
                return request ? WriteHeadRequest(client, id, outputFile) : await ExecuteHeadRequestAsync(client, id, outputFile, quiet, cancellationToken);
            }
        );

        return result;
    }

    static int WriteHeadRequest(IDofusDbBreedImagesClient client, long id, string? outputFile)
    {
        Uri query = client.GetHeadRequestUri(id);
        using Stream stream = Utils.GetOutputStream(outputFile);
        using StreamWriter textWriter = new(stream);
        textWriter.WriteLine(query.ToString());
        return 0;
    }

    async Task<int> ExecuteHeadRequestAsync(IDofusDbBreedImagesClient client, long id, string? outputFile, bool quiet, CancellationToken cancellationToken)
    {
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
                .StartAsync($"Executing query: {client.GetHeadRequestUri(id)}...", async _ => image = await client.GetHeadAsync(id, cancellationToken));
        }

        await using Stream stream = GetOutputStream(id, "head", outputFile);
        await image.CopyToAsync(stream, cancellationToken);

        return 0;
    }

    Stream GetOutputStream(long id, string prefix, string? outputFile)
    {
        if (outputFile == null)
        {
            outputFile = $"{command}-{prefix}-{id}.png";
        }

        return Utils.GetOutputStream(outputFile);
    }
}
