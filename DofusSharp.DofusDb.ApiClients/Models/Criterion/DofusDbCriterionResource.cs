using DofusSharp.DofusDb.ApiClients.Models.Items;

namespace DofusSharp.DofusDb.ApiClients.Models.Criterion;

/// <summary>
///     A resource involved in a <see cref="DofusDbCriterion" />.
/// </summary>
/// <remarks>
///     The DofusDB API returns all data for the resource, but this type only stores the resource's identifier and type.
///     The <c>type</c> field in the criterion replaces any <c>type</c> field in the resource data, such as <see cref="DofusDbItem.Type" />.
///     This avoids parsing errors due to type field conflicts.
///     The resource data can then be retrieved by querying the API again, see the <see cref="DofusDbCriterionResourceExtensions.GetAsync" /> extension method.
/// </remarks>
/// <param name="Id">The unique identifier of the resource.</param>
/// <param name="Type">The type of the resource.</param>
public record DofusDbCriterionResource(long Id, DofusDbCriterionResourceType Type) : DofusDbCriterion;

/// <summary>
///     Extension methods for <see cref="DofusDbCriterionResource" />.
/// </summary>
public static class DofusDbCriterionResourceExtensions
{
    /// <summary>
    ///     Retrieve detailed data about the specified resource from the DofusDB API.
    /// </summary>
    /// <param name="resource">The resource to fetch data for.</param>
    /// <param name="clientsFactory">The client used to create the appropriate client.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A <see cref="Task{DofusDbResource}" /> representing the asynchronous operation, with the resource data as the result.
    /// </returns>
    public static Task<DofusDbResource> GetAsync(this DofusDbCriterionResource resource, IDofusDbClientsFactory clientsFactory, CancellationToken cancellationToken = default)
    {
        IDofusDbTableClient client = clientsFactory.CreateClientForCriterionResourceOfType(resource.Type);
        return client.GetAsync(resource.Id, cancellationToken);
    }
}
