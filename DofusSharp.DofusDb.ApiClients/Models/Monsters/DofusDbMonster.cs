using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Monsters;

/// <summary>
///     A monster in the game.
/// </summary>
public class DofusDbMonster : DofusDbResource
{
    /// <summary>
    ///     The unique identifier of the monster's graphics.
    /// </summary>
    public long? GfxId { get; init; }

    /// <summary>
    ///     The unique identifier of the monster's race.
    /// </summary>
    public long? Race { get; init; }

    /// <summary>
    ///     The grades of the monster, each representing a different level and stats.
    /// </summary>
    public IReadOnlyList<DofusDbMonsterGrade>? Grades { get; init; }

    /// <summary>
    ///     The look string representing the monster's appearance.
    /// </summary>
    public string? Look { get; init; }

    /// <summary>
    ///     The items that can be dropped by the monster.
    /// </summary>
    public IReadOnlyCollection<DofusDbMonsterDrop>? Drops { get; init; }

    /// <summary>
    ///     The items that can be dropped by the monster in Temporis mode.
    /// </summary>
    public IReadOnlyCollection<DofusDbMonsterDrop>? TemporisDrops { get; init; }

    /// <summary>
    ///     The unique identifiers of the subareas where the monster can be found.
    /// </summary>
    public IReadOnlyCollection<long>? Subareas { get; init; }

    /// <summary>
    ///     The unique identifiers of the spells used by the monster.
    /// </summary>
    public IReadOnlyCollection<long>? Spells { get; init; }

    /// <summary>
    ///     The grades of the spells used by the monster.
    /// </summary>
    public IReadOnlyCollection<string>? SpellGrades { get; init; }

    /// <summary>
    ///     The unique identifier of the monster's favorite subarea.
    /// </summary>
    public long? FavoriteSubareaId { get; init; }

    /// <summary>
    ///     The unique identifier of the corresponding mini-boss, if any.
    /// </summary>
    public long? CorrespondingMiniBossId { get; init; }

    /// <summary>
    ///     The speed adjustment value for the monster.
    /// </summary>
    public long? SpeedAdjust { get; init; }

    /// <summary>
    ///     The unique identifier of the monster's creature bone.
    /// </summary>
    public long? CreatureBoneId { get; init; }

    /// <summary>
    ///     The cost to summon the monster.
    /// </summary>
    public int? SummonCost { get; init; }

    /// <summary>
    ///     The unique identifiers of idols incompatible with the monster.
    /// </summary>
    public IReadOnlyCollection<long>? IncompatibleIdols { get; init; }

    /// <summary>
    ///     The unique identifiers of challenges incompatible with the monster.
    /// </summary>
    public IReadOnlyCollection<long>? IncompatibleChallenges { get; init; }

    /// <summary>
    ///     The size of the monster's aggressive zone.
    /// </summary>
    public int? AgressiveZoneSize { get; init; }

    /// <summary>
    ///     The level difference for aggression.
    /// </summary>
    public int? AgressiveLevelDiff { get; init; }

    /// <summary>
    ///     The criterion for immunity to aggression.
    /// </summary>
    public string? AgressiveImmunityCriterion { get; init; }

    /// <summary>
    ///     The delay before the monster attacks aggressively.
    /// </summary>
    public long? AgressiveAttackDelay { get; init; }

    /// <summary>
    ///     The reference for scaling grades.
    /// </summary>
    public int? ScaleGradeRef { get; init; }

    /// <summary>
    ///     The ratios for monster characteristics.
    /// </summary>
    public IReadOnlyList<(int, double)>? CharacRatios { get; init; }

    /// <summary>
    ///     The name of the monster.
    /// </summary>
    public DofusDbMultiLangString? Name { get; init; }

    /// <summary>
    ///     Whether the monster uses a summon slot.
    /// </summary>
    public bool? UseSummonSlot { get; init; }

    /// <summary>
    ///     Whether the monster uses a bomb slot.
    /// </summary>
    public bool? UseBombSlot { get; init; }

    /// <summary>
    ///     Whether the monster is a boss.
    /// </summary>
    public bool? IsBoss { get; init; }

    /// <summary>
    ///     Whether the monster is a mini-boss.
    /// </summary>
    public bool? IsMiniBoss { get; init; }

    /// <summary>
    ///     Whether the monster is a quest monster.
    /// </summary>
    public bool? IsQuestMonster { get; init; }

    /// <summary>
    ///     Whether the monster uses fast animations.
    /// </summary>
    public bool? FastAnimsFun { get; init; }

    /// <summary>
    ///     Whether the monster can play.
    /// </summary>
    public bool? CanPlay { get; init; }

    /// <summary>
    ///     Whether the monster can tackle.
    /// </summary>
    public bool? CanTackle { get; init; }

    /// <summary>
    ///     Whether the monster can be pushed.
    /// </summary>
    public bool? CanBePushed { get; init; }

    /// <summary>
    ///     Whether the monster can switch position.
    /// </summary>
    public bool? CanSwitchPos { get; init; }

    /// <summary>
    ///     Whether the monster can switch position on target.
    /// </summary>
    public bool? CanSwitchPosOnTarget { get; init; }

    /// <summary>
    ///     Whether the monster can be carried.
    /// </summary>
    public bool? CanBeCarried { get; init; }

    /// <summary>
    ///     Whether the monster can use portals.
    /// </summary>
    public bool? CanUsePortal { get; init; }

    /// <summary>
    ///     Whether all idols are disabled for the monster.
    /// </summary>
    public bool? AllIdolsDisabled { get; init; }

    /// <summary>
    ///     Whether the monster uses race values.
    /// </summary>
    public bool? UseRaceValues { get; init; }

    /// <summary>
    ///     Whether soul capture is forbidden for the monster.
    /// </summary>
    public bool? SoulCaptureForbidden { get; init; }

    /// <summary>
    ///     The slugified name of the monster.
    /// </summary>
    public DofusDbMultiLangString? Slug { get; init; }

    /// <summary>
    ///     The tags associated with the monster.
    /// </summary>
    public IReadOnlyCollection<string>? Tags { get; init; }

    /// <summary>
    ///     The URL of the monster's image.
    /// </summary>
    public string? Img { get; init; }

    /// <summary>
    ///     The corresponding mini-boss monster, if any.
    /// </summary>
    public DofusDbMonster? CorrespondingMiniBoss { get; init; }
}

public static class DofusDbMonsterImagesExtensions
{
    /// <summary>
    ///     Fetches the graphics image associated with the monster using the provided factory to create the image client.
    /// </summary>
    /// <param name="monster">The monster for which to fetch the graphics image.</param>
    /// <param name="factory">The factory used to create the image client.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public static async Task<Stream?> GetGfxAsync(this DofusDbMonster monster, IDofusDbClientsFactory factory, CancellationToken cancellationToken = default) =>
        monster.GfxId.HasValue ? await factory.MonsterImages().GetImageAsync(monster.GfxId.Value, cancellationToken) : null;
}
