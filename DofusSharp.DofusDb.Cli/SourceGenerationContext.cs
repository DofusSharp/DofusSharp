using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models.Items;

namespace DofusSharp.DofusDb.Cli;

[JsonSerializable(typeof(DofusDbItem))]
[JsonSerializable(typeof(IReadOnlyList<DofusDbItem>))]
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web, WriteIndented = true)]
partial class SourceGenerationContext : JsonSerializerContext
{
}
