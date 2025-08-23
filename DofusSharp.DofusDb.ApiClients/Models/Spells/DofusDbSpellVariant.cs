namespace DofusSharp.DofusDb.ApiClients.Models.Spells;

/// <summary>
///     A pair of spells that are variants of each other for a given breed.
/// </summary>
public class DofusDbSpellVariant : DofusDbEntity
{
    /// <summary>
    ///     The unique identifier of the breed associated with this spell variants.
    /// </summary>
    public long? BreedId { get; init; }

    /// <summary>
    ///     The collection of spell identifiers that are variants of each other.
    /// </summary>
    public IReadOnlyCollection<long>? SpellIds { get; init; }
}
