using System.Text.Json.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Models.Common;

[JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(StringCase))]
[JsonDerivedType(typeof(ResourceCase))]
public abstract class DofusDbStringOrResource
{
    public class StringCase : DofusDbStringOrResource
    {
        public string? Value { get; init; }
    }

    public class ResourceCase : DofusDbStringOrResource
    {
        public DofusDbResource? Value { get; init; }
    }
}
