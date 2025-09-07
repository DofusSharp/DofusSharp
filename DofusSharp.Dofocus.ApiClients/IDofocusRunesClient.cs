using DofusSharp.Dofocus.ApiClients.Models.Runes;
using DofusSharp.Dofocus.ApiClients.Requests;
using DofusSharp.Dofocus.ApiClients.Responses;

namespace DofusSharp.Dofocus.ApiClients;

public interface IDofocusRunesClient
{
    Task<IReadOnlyCollection<DofocusRune>> GetRunesAsync(CancellationToken cancellationToken = default);
    Task<PutRunePriceResponse> PutRunePriceAsync(long runeId, PutRunePriceRequest request, CancellationToken cancellationToken = default);
}
