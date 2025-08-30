namespace DofusSharp.DofusDb.ApiClients.Models.Monsters;

/// <summary>
///     Represents bonus characteristics for a monster grade.
/// </summary>
public class DofusDbBonusCharacteristics
{
    /// <summary>
    ///     The bonus life points.
    /// </summary>
    public int? LifePoints { get; init; }

    /// <summary>
    ///     The bonus strength.
    /// </summary>
    public int? Strength { get; init; }

    /// <summary>
    ///     The bonus wisdom.
    /// </summary>
    public int? Wisdom { get; init; }

    /// <summary>
    ///     The bonus chance.
    /// </summary>
    public int? Chance { get; init; }

    /// <summary>
    ///     The bonus agility.
    /// </summary>
    public int? Agility { get; init; }

    /// <summary>
    ///     The bonus intelligence.
    /// </summary>
    public int? Intelligence { get; init; }

    /// <summary>
    ///     The bonus earth resistance.
    /// </summary>
    public int? EarthResistance { get; init; }

    /// <summary>
    ///     The bonus fire resistance.
    /// </summary>
    public int? FireResistance { get; init; }

    /// <summary>
    ///     The bonus water resistance.
    /// </summary>
    public int? WaterResistance { get; init; }

    /// <summary>
    ///     The bonus air resistance.
    /// </summary>
    public int? AirResistance { get; init; }

    /// <summary>
    ///     The bonus neutral resistance.
    /// </summary>
    public int? NeutralResistance { get; init; }

    /// <summary>
    ///     The bonus tackle evade value.
    /// </summary>
    public int? TackleEvade { get; init; }

    /// <summary>
    ///     The bonus tackle block value.
    /// </summary>
    public int? TackleBlock { get; init; }

    /// <summary>
    ///     The bonus earth damage.
    /// </summary>
    public int? BonusEarthDamage { get; init; }

    /// <summary>
    ///     The bonus fire damage.
    /// </summary>
    public int? BonusFireDamage { get; init; }

    /// <summary>
    ///     The bonus water damage.
    /// </summary>
    public int? BonusWaterDamage { get; init; }

    /// <summary>
    ///     The bonus air damage.
    /// </summary>
    public int? BonusAirDamage { get; init; }

    /// <summary>
    ///     The AP removal value.
    /// </summary>
    public int? ApRemoval { get; init; }
}
