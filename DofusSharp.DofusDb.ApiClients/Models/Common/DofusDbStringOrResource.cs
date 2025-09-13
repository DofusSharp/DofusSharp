namespace DofusSharp.DofusDb.ApiClients.Models.Common;

public abstract class DofusDbStringOrResource
{
    public class StringCase : DofusDbStringOrResource
    {
        public string? String { get; init; }
    }

    public class ResourceCase : DofusDbStringOrResource
    {
        public DofusDbResource? Resource { get; init; }
    }
}
