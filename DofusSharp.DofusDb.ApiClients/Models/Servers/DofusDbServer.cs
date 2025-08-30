using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Servers;

public class DofusDbServer : DofusDbResource
{
    public DateOnly? OpeningDate { get; init; }
    public DofusDbLanguage? Language { get; init; }
    public int? PopulationId { get; init; }
    public int? GameTypeId { get; init; }
    public int? CommunityId { get; init; }
    public IReadOnlyCollection<DofusDbLanguage>? RestrictedToLanguages { get; init; }
    public bool? MonoAccount { get; init; }
    public string? Illus { get; init; }
    public DofusDbMultiLangString? Name { get; init; }
    public DofusDbMultiLangString? Comment { get; init; }
}
