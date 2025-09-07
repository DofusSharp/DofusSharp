using DofusSharp.Dofocus.ApiClients.Models.Items;
using DofusSharp.Dofocus.ApiClients.Requests;
using DofusSharp.Dofocus.ApiClients.Responses;

namespace DofusSharp.Dofocus.ApiClients;

public interface IDofocusItemsClient
{
    Task<IReadOnlyCollection<DofocusItemMinimal>> GetItemsAsync(CancellationToken cancellationToken = default);
    Task<DofocusItem> GetItemAsync(long itemId, CancellationToken cancellationToken = default);
    Task<PutItemPriceResponse> PutItemPriceAsync(long itemId, PutItemPriceRequest request, CancellationToken cancellationToken = default);
    Task<PutItemCoefficientResponse> PutItemCoefficientAsync(long itemId, PutItemCoefficientRequest request, CancellationToken cancellationToken = default);
}
