using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Breeds;

/// <summary>
///     A role associated with a breed in the game.
/// </summary>
public class DofusDbBreedRole
{
    /// <summary>
    ///     The unique identifier of the breed this role is associated with.
    /// </summary>
    public long? BreedId { get; init; }

    /// <summary>
    ///     The unique identifier of the role.
    /// </summary>
    public long? RoleId { get; init; }

    /// <summary>
    ///     The value associated with the role.
    /// </summary>
    public int? Value { get; init; }

    /// <summary>
    ///     The order in which the role appears in the list.
    /// </summary>
    public int? Order { get; init; }

    /// <summary>
    ///     The name of the role.
    /// </summary>
    public DofusDbMultiLangString? Description { get; init; }
}
