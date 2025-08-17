using DofusSharp.DofusDb.ApiClients.Models.Common;

namespace DofusSharp.DofusDb.ApiClients.Models.Maps;

public class SuperArea : DofusDbEntity
{
    public int? WorldMapId { get; init; }
    public bool? HasWorldMap { get; init; }
    public MultiLangString? Name { get; init; }
}

public class Area : DofusDbEntity
{
    public int? SuperAreaId { get; init; }
    public bool? ContainsHouses { get; init; }
    public bool? ContainsPaddocks { get; init; }
    public Bounds? Bounds { get; init; }
    public int? WorldMapId { get; init; }
    public bool? HasWorldMap { get; init; }
    public bool? HasSuggestions { get; init; }
    public MultiLangString? Name { get; init; }
}

public class SubArea : DofusDbEntity
{
    public int? AreaId { get; init; }
    public IReadOnlyCollection<long>? MapIds { get; init; }
    public Bounds? Bounds { get; init; }
    public IReadOnlyCollection<int>? Shape { get; init; }
    public IReadOnlyCollection<int>? CustomWorldMap { get; init; }
    public int? PackId { get; init; }
    public int? Level { get; init; }
    public bool? IsConquestVillage { get; init; }
    public bool? BasicAccountAllowed { get; init; }
    public bool? DisplayOnWorldMap { get; init; }
    public bool? MountAutoTripAllowed { get; init; }
    public bool? PsiAllowed { get; init; }
    public IReadOnlyCollection<int>? Monsters { get; init; }
    public IReadOnlyCollection<int>? EntranceMapIds { get; init; }
    public IReadOnlyCollection<int>? ExitMapIds { get; init; }
    public bool? Capturable { get; init; }
    public IReadOnlyCollection<int>? Achievements { get; init; }
    public int? ExploreAchievementId { get; init; }
    public IReadOnlyCollection<int>? Harvestables { get; init; }
    public int? AssociatedZaapMapId { get; init; }
    public IReadOnlyCollection<int>? Neighbors { get; init; }
    public int? DungeonId { get; init; }
    public MultiLangString? Name { get; init; }
    public IReadOnlyCollection<int>? QuestsIds { get; init; }
    public IReadOnlyCollection<int>? NpcsIds { get; init; }
}

public class Map : DofusDbEntity
{
    public IReadOnlyCollection<int>? BackgroundFixtures { get; init; }
    public int? BackgroundsCount { get; init; }
    public IReadOnlyCollection<int>? ForegroundFixtures { get; init; }
    public int? ForegroundsCount { get; init; }
    public int? MapVersion { get; init; }
    public bool? Encrypted { get; init; }
    public int? EncryptedVersion { get; init; }
    public int? RelativeId { get; init; }
    public int? MapType { get; init; }
    public int? SubAreaId { get; init; }
    public long? ShadowBonusOnEntities { get; init; }
    public long? BackgroundAlpha { get; init; }
    public long? BackgroundRed { get; init; }
    public long? BackgroundGreen { get; init; }
    public long? BackgroundBlue { get; init; }
    public long? GridColor { get; init; }
    public long? BackgroundColor { get; init; }
    public int? ZoomScale { get; init; }
    public int? ZoomOffsetX { get; init; }
    public int? ZoomOffsetY { get; init; }
    public int? TacticalModeTemplateId { get; init; }

    // ReSharper disable once InconsistentNaming
    public int? GroundCRC { get; init; }
    public IReadOnlyCollection<int>? Layers { get; init; }
    public int? LayersCount { get; init; }
    public IReadOnlyCollection<MapCell>? Cells { get; init; }
    public int? CellsCount { get; init; }
}

public class MapCell
{
    public int? Nb { get; init; }
    public int? Floor { get; init; }
    public bool? Mov { get; init; }
    public bool? NonWalkableDuringFight { get; init; }

    // ReSharper disable once InconsistentNaming
    public bool? NonWalkableDuringRP { get; init; }
    public bool? Los { get; init; }
    public bool? Blue { get; init; }
    public bool? Red { get; init; }
    public bool? Visible { get; init; }
    public bool? HavenBagCell { get; init; }
    public double? Speed { get; init; }
    public int? MapChangeData { get; init; }
    public int? MoveZone { get; init; }
}
