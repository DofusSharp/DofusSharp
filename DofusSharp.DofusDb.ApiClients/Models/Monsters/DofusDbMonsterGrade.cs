namespace DofusSharp.DofusDb.ApiClients.Models.Monsters;

/// <summary>
///     A grade of a monster, including its stats and characteristics.
/// </summary>
public class DofusDbMonsterGrade
{
    /// <summary>
    ///     The unique identifier of the monster.
    /// </summary>
    public long? MonsterId { get; init; }

    /// <summary>
    ///     The grade number of the monster.
    /// </summary>
    public int? Grade { get; init; }

    /// <summary>
    ///     The level of the monster at this grade.
    /// </summary>
    public int? Level { get; init; }

    /// <summary>
    ///     The life points of the monster at this grade.
    /// </summary>
    public int? LifePoints { get; init; }

    /// <summary>
    ///     The action points of the monster at this grade.
    /// </summary>
    public int? ActionPoints { get; init; }

    /// <summary>
    ///     The movement points of the monster at this grade.
    /// </summary>
    public int? MovementPoints { get; init; }

    /// <summary>
    ///     The vitality of the monster at this grade.
    /// </summary>
    public int? Vitality { get; init; }

    /// <summary>
    ///     The PA dodge value of the monster at this grade.
    /// </summary>
    public int? PaDodge { get; init; }

    /// <summary>
    ///     The PM dodge value of the monster at this grade.
    /// </summary>
    public int? PmDodge { get; init; }

    /// <summary>
    ///     The strength of the monster at this grade.
    /// </summary>
    public int? Strength { get; init; }

    /// <summary>
    ///     The wisdom of the monster at this grade.
    /// </summary>
    public int? Wisdom { get; init; }

    /// <summary>
    ///     The chance of the monster at this grade.
    /// </summary>
    public int? Chance { get; init; }

    /// <summary>
    ///     The agility of the monster at this grade.
    /// </summary>
    public int? Agility { get; init; }

    /// <summary>
    ///     The intelligence of the monster at this grade.
    /// </summary>
    public int? Intelligence { get; init; }

    /// <summary>
    ///     The earth resistance of the monster at this grade.
    /// </summary>
    public int? EarthResistance { get; init; }

    /// <summary>
    ///     The air resistance of the monster at this grade.
    /// </summary>
    public int? AirResistance { get; init; }

    /// <summary>
    ///     The fire resistance of the monster at this grade.
    /// </summary>
    public int? FireResistance { get; init; }

    /// <summary>
    ///     The water resistance of the monster at this grade.
    /// </summary>
    public int? WaterResistance { get; init; }

    /// <summary>
    ///     The neutral resistance of the monster at this grade.
    /// </summary>
    public int? NeutralResitance { get; init; }

    /// <summary>
    ///     The bonus characteristics for the monster at this grade.
    /// </summary>
    public DofusDbBonusCharacteristics? BonusCharacteristics { get; init; }

    /// <summary>
    ///     The experience points awarded for defeating the monster at this grade.
    /// </summary>
    public int? GradeXp { get; init; }

    /// <summary>
    ///     The damage reflect value for the monster at this grade.
    /// </summary>
    public int? DamageReflect { get; init; }

    /// <summary>
    ///     The hidden level of the monster at this grade.
    /// </summary>
    public int? HiddenLevel { get; init; }

    /// <summary>
    ///     The unique identifier of the starting spell for the monster at this grade.
    /// </summary>
    public int? StartingSpellId { get; init; }

    /// <summary>
    ///     The bonus range for the monster at this grade.
    /// </summary>
    public int? BonusRange { get; init; }
}
