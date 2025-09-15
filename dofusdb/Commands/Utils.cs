using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models;

public static class Utils
{
    public static JsonSerializerOptions BuildJsonSerializerOptions(bool prettyPrint) =>
        new(JsonSerializerDefaults.Web)
            { TypeInfoResolver = DofusDbModelsSourceGenerationContext.Default, WriteIndented = prettyPrint, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault };

    public static Stream GetOutputStream(string? outputFile)
    {
        if (string.IsNullOrWhiteSpace(outputFile))
        {
            return Console.OpenStandardOutput();
        }

        string? directory = Path.GetDirectoryName(outputFile);
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return File.Create(outputFile);
    }
}
