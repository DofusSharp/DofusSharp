using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Breeds;

/// <summary>
///     A character breed in the game.
/// </summary>
public class DofusDbBreed : DofusDbResource
{
    /// <summary>
    ///     The unique identifier of the guide item associated with this breed.
    /// </summary>
    public long? GuideItemId { get; init; }

    /// <summary>
    ///     The appearance configuration for male characters of this breed.
    /// </summary>
    public string? MaleLook { get; init; }

    /// <summary>
    ///     The appearance configuration for female characters of this breed.
    /// </summary>
    public string? FemaleLook { get; init; }

    /// <summary>
    ///     The unique identifier of the creature bones used for this breed.
    /// </summary>
    public long? CreatureBonesId { get; init; }

    /// <summary>
    ///     The unique identifier of the artwork for male characters of this breed.
    /// </summary>
    public long? MaleArtwork { get; init; }

    /// <summary>
    ///     The unique identifier of the artwork for female characters of this breed.
    /// </summary>
    public long? FemaleArtwork { get; init; }

    /// <summary>
    ///     The stat point thresholds and costs for Strength.
    /// </summary>
    public IReadOnlyList<(int Threshold, int Cost)>? StatsPointsForStrength { get; init; }

    /// <summary>
    ///     The stat point thresholds and costs for Intelligence.
    /// </summary>
    public IReadOnlyList<(int Threshold, int Cost)>? StatsPointsForIntelligence { get; init; }

    /// <summary>
    ///     The stat point thresholds and costs for Chance.
    /// </summary>
    public IReadOnlyList<(int Threshold, int Cost)>? StatsPointsForChance { get; init; }

    /// <summary>
    ///     The stat point thresholds and costs for Agility.
    /// </summary>
    public IReadOnlyList<(int Threshold, int Cost)>? StatsPointsForAgility { get; init; }

    /// <summary>
    ///     The stat point thresholds and costs for Vitality.
    /// </summary>
    public IReadOnlyList<(int Threshold, int Cost)>? StatsPointsForVitality { get; init; }

    /// <summary>
    ///     The stat point thresholds and costs for Wisdom.
    /// </summary>
    public IReadOnlyList<(int Threshold, int Cost)>? StatsPointsForWisdom { get; init; }

    /// <summary>
    ///     The unique identifiers of spells available to this breed.
    /// </summary>
    public IReadOnlyList<long>? BreedSpellsId { get; init; }

    /// <summary>
    ///     The roles associated with this breed.
    /// </summary>
    public IReadOnlyList<DofusDbBreedRole>? BreedRoles { get; init; }

    /// <summary>
    ///     The available color identifiers for male characters of this breed.
    /// </summary>
    public IReadOnlyList<long>? MaleColors { get; init; }

    /// <summary>
    ///     The available color identifiers for female characters of this breed.
    /// </summary>
    public IReadOnlyList<long>? FemaleColors { get; init; }

    /// <summary>
    ///     The unique identifier of the map where characters of this breed spawn.
    /// </summary>
    public long? SpawnMap { get; init; }

    /// <summary>
    ///     The complexity rating of this breed for new players.
    /// </summary>
    public int? Complexity { get; init; }

    /// <summary>
    ///     The sort index used for ordering breeds in lists.
    /// </summary>
    public int? SortIndex { get; init; }

    /// <summary>
    ///     The short name of the breed.
    /// </summary>
    public DofusDbMultiLangString? ShortName { get; init; }

    /// <summary>
    ///     The long name of the breed.
    /// </summary>
    public DofusDbMultiLangString? LongName { get; init; }

    /// <summary>
    ///     The description of the breed.
    /// </summary>
    public DofusDbMultiLangString? Description { get; init; }

    /// <summary>
    ///     The gameplay description of the breed.
    /// </summary>
    public DofusDbMultiLangString? GameplayDescription { get; init; }

    /// <summary>
    ///     The gameplay class description of the breed.
    /// </summary>
    public DofusDbMultiLangString? GameplayClassDescription { get; init; }

    /// <summary>
    ///     The URL of the breed's image.
    /// </summary>
    public string? Img { get; init; }

    /// <summary>
    ///     The URL of the breed's transparent image.
    /// </summary>
    public string? ImgTransparent { get; init; }

    /// <summary>
    ///     The heads available for this breed.
    /// </summary>
    public DofusDbBreedHeads? Heads { get; init; }
}
