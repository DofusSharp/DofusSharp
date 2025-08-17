using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

/// <summary>
///     Position information about a map in the game.
/// </summary>
public class MapPosition : DofusDbEntity
{
    /// <summary>
    ///     The X coordinate of the map.
    /// </summary>
    public int? PosX { get; init; }

    /// <summary>
    ///     The Y coordinate of the map.
    /// </summary>
    public int? PosY { get; init; }

    /// <summary>
    ///     The unique identifier of the sub area for the map.
    /// </summary>
    public int? SubAreaId { get; init; }

    /// <summary>
    ///     The unique identifier of the world map for the map.
    /// </summary>
    public int? WorldMap { get; init; }

    /// <summary>
    ///     The unique identifier of the tactical mode template for the map.
    /// </summary>
    public int? TacticalModeTemplateId { get; init; }

    /// <summary>
    ///     The name of the map.
    /// </summary>
    public MultiLangString? Name { get; init; }

    /// <summary>
    ///     Whether the map allows challenges.
    /// </summary>
    public bool? CapabilityAllowChallenge { get; init; }

    /// <summary>
    ///     Whether the map allows aggression.
    /// </summary>
    public bool? CapabilityAllowAggression { get; init; }

    /// <summary>
    ///     Whether the map allows teleporting to it.
    /// </summary>
    public bool? CapabilityAllowTeleportTo { get; init; }

    /// <summary>
    ///     Whether the map allows teleporting from it.
    /// </summary>
    public bool? CapabilityAllowTeleportFrom { get; init; }

    /// <summary>
    ///     Whether the map allows exchange between players.
    /// </summary>
    public bool? CapabilityAllowExchangeBetweenPlayers { get; init; }

    /// <summary>
    ///     Whether the map allows human vendors.
    /// </summary>
    public bool? CapabilityAllowHumanVendor { get; init; }

    /// <summary>
    ///     Whether the map allows collectors.
    /// </summary>
    public bool? CapabilityAllowCollector { get; init; }

    /// <summary>
    ///     Whether the map allows soul capture.
    /// </summary>
    public bool? CapabilityAllowSoulCapture { get; init; }

    /// <summary>
    ///     Whether the map allows soul summon.
    /// </summary>
    public bool? CapabilityAllowSoulSummon { get; init; }

    /// <summary>
    ///     Whether the map allows tavern regeneration.
    /// </summary>
    public bool? CapabilityAllowTavernRegen { get; init; }

    /// <summary>
    ///     Whether the map allows tomb mode.
    /// </summary>
    public bool? CapabilityAllowTombMode { get; init; }

    /// <summary>
    ///     Whether the map allows teleporting everywhere.
    /// </summary>
    public bool? CapabilityAllowTeleportEverywhere { get; init; }

    /// <summary>
    ///     Whether the map allows fight challenges.
    /// </summary>
    public bool? CapabilityAllowFightChallenge { get; init; }

    /// <summary>
    ///     Whether the map allows monster respawn.
    /// </summary>
    public bool? CapabilityAllowMonsterRespawn { get; init; }

    /// <summary>
    ///     Whether the map allows monster fights.
    /// </summary>
    public bool? CapabilityAllowMonsterFight { get; init; }

    /// <summary>
    ///     Whether the map allows mounts.
    /// </summary>
    public bool? CapabilityAllowMount { get; init; }

    /// <summary>
    ///     Whether the map allows object disposal.
    /// </summary>
    public bool? CapabilityAllowObjectDisposal { get; init; }

    /// <summary>
    ///     Whether the map allows underwater activities.
    /// </summary>
    public bool? CapabilityAllowUnderwater { get; init; }

    /// <summary>
    ///     Whether the map allows 1v1 PvP.
    /// </summary>
    public bool? CapabilityAllowPvp1V1 { get; init; }

    /// <summary>
    ///     Whether the map allows 3v3 PvP.
    /// </summary>
    public bool? CapabilityAllowPvp3V3 { get; init; }

    /// <summary>
    ///     Whether the map allows monster aggression.
    /// </summary>
    public bool? CapabilityAllowMonsterAggression { get; init; }

    /// <summary>
    ///     Whether all capabilities are allowed for the map.
    /// </summary>
    public bool? AllCapabilitiesMask { get; init; }

    /// <summary>
    ///     Whether the map is outdoors.
    /// </summary>
    public bool? Outdoor { get; init; }

    /// <summary>
    ///     Whether the map name is shown on fingerposts.
    /// </summary>
    public bool? ShowNameOnFingerpost { get; init; }

    /// <summary>
    ///     Whether the map has priority on the world map.
    /// </summary>
    public bool? HasPriorityOnWorldMap { get; init; }

    /// <summary>
    ///     Whether the map allows prisms.
    /// </summary>
    public bool? AllowPrism { get; init; }

    /// <summary>
    ///     Whether the map is a transition.
    /// </summary>
    public bool? IsTransition { get; init; }

    /// <summary>
    ///     Whether the map has a template.
    /// </summary>
    public bool? MapHasTemplate { get; init; }

    /// <summary>
    ///     Whether the map has a public paddock.
    /// </summary>
    public bool? HasPublicPaddock { get; init; }

    /// <summary>
    ///     The images associated with the map, indexed by zoom level.
    /// </summary>
    public Dictionary<double, string>? Img { get; init; }
}
