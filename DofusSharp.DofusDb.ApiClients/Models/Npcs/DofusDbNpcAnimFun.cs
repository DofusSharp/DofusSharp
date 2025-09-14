namespace DofusSharp.DofusDb.ApiClients.Models.Npcs;

/// <summary>
///     An animation function for a non-player character (NPC).
/// </summary>
public class DofusDbNpcAnimFun
{
    /// <summary>
    ///     The unique identifier of the animation.
    /// </summary>
    public long? AnimId { get; init; }

    /// <summary>
    ///     The unique identifier of the entity associated with the animation.
    /// </summary>
    public long? EntityId { get; init; }

    /// <summary>
    ///     The name of the animation.
    /// </summary>
    public string? AnimName { get; init; }

    /// <summary>
    ///     The weight of the animation, used for selection probability.
    /// </summary>
    public double? AnimWeight { get; init; }

    /// <summary>
    ///     The list of sub-animation functions associated with this animation.
    /// </summary>
    public IReadOnlyList<DofusDbNpcAnimFun>? SubAnimFunData { get; init; }
}
