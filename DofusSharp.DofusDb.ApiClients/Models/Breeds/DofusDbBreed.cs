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

public static class DofusDbBreedImagesExtensions
{
    /// <summary>
    ///     Fetches the symbol image for the specified breed using the provided factory to create the image client.
    /// </summary>
    /// <param name="breed">The breed for which to fetch the symbol image.</param>
    /// <param name="factory">The factory used to create the image client.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static async Task<Stream?> GetSymbolAsync(this DofusDbBreed breed, IDofusDbClientsFactory factory, CancellationToken cancellationToken = default) =>
        breed.Id.HasValue ? await factory.BreedImages().GetSymbolAsync(breed.Id.Value, cancellationToken) : null;

    /// <summary>
    ///     Fetches the logo image for the specified breed using the provided factory to create the image client.
    /// </summary>
    /// <param name="breed">The breed for which to fetch the logo image.</param>
    /// <param name="factory">The factory used to create the image client.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static async Task<Stream?> GetLogoAsync(this DofusDbBreed breed, IDofusDbClientsFactory factory, CancellationToken cancellationToken = default) =>
        breed.Id.HasValue ? await factory.BreedImages().GetLogoAsync(breed.Id.Value, cancellationToken) : null;

    /// <summary>
    ///     Fetches the head image for the specified breed using the provided factory to create the image client.
    /// </summary>
    /// <param name="breed">The breed for which to fetch the head image.</param>
    /// <param name="factory">The factory used to create the image client.</param>
    /// <param name="gender">The gender of the head image to fetch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static async Task<Stream?> GetHeadAsync(this DofusDbBreed breed, IDofusDbClientsFactory factory, DofusDbGender gender, CancellationToken cancellationToken = default) =>
        breed.Id.HasValue
            ? await factory
                .BreedImages()
                .GetHeadAsync(
                    gender switch
                    {
                        DofusDbGender.Masculine => breed.Id.Value * 10,
                        DofusDbGender.Feminine => breed.Id.Value * 10 + 1,
                        _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
                    },
                    cancellationToken
                )
            : null;
}
