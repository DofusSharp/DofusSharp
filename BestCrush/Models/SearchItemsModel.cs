namespace BestCrush.Models;

public class SearchItemsModel
{
    public int? LevelMin { get; set; }
    public int? LevelMax { get; set; }
    public EquipmentTypesFilter EquipmentType { get; set; } = new();
}

public class EquipmentTypesFilter
{
    public bool Amulet { get; set; }
    public bool Ring { get; set; }
    public bool Belt { get; set; }
    public bool Boots { get; set; }
    public bool Hat { get; set; }
    public bool Cloak { get; set; }
    public bool Trophy { get; set; }

    public bool Bow { get; set; }
    public bool Lance { get; set; }
    public bool MagicWeapon { get; set; }
    public bool Scythe { get; set; }
    public bool Axe { get; set; }
    public bool Tool { get; set; }
    public bool Pickaxe { get; set; }
    public bool Wand { get; set; }
    public bool Staff { get; set; }
    public bool Dagger { get; set; }
    public bool Sword { get; set; }
    public bool Hammer { get; set; }
    public bool Shovel { get; set; }

    public bool Pet { get; set; }
    public bool Petsmount { get; set; }
    public bool Mount { get; set; }
}
