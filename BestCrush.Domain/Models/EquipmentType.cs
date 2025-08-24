namespace BestCrush.Domain.Models;

public enum EquipmentType
{
    Amulet,
    Bow,
    Wand,
    Staff,
    Dagger,
    Sword,
    Hammer,
    Shovel,
    Ring,
    Belt,
    Boots,
    Hat,
    Cloak,
    Pet,
    Petsmount,
    Mount,
    Axe,
    Tool,
    Pickaxe,
    Scythe,
    MagicWeapon,
    Trophy,
    Lance
}

public static class EquipmentTypeExtensions
{
    public static string ToDisplayName(this EquipmentType type) =>
        type switch
        {
            EquipmentType.Amulet => "Amulet",
            EquipmentType.Bow => "Bow",
            EquipmentType.Wand => "Wand",
            EquipmentType.Staff => "Staff",
            EquipmentType.Dagger => "Dagger",
            EquipmentType.Sword => "Sword",
            EquipmentType.Hammer => "Hammer",
            EquipmentType.Shovel => "Shovel",
            EquipmentType.Ring => "Ring",
            EquipmentType.Belt => "Belt",
            EquipmentType.Boots => "Boots",
            EquipmentType.Hat => "Hat",
            EquipmentType.Cloak => "Cloak",
            EquipmentType.Pet => "Pet",
            EquipmentType.Petsmount => "Petsmount",
            EquipmentType.Mount => "Mount",
            EquipmentType.Axe => "Axe",
            EquipmentType.Tool => "Tool",
            EquipmentType.Pickaxe => "Pickaxe",
            EquipmentType.Scythe => "Scythe",
            EquipmentType.MagicWeapon => "Magic Weapon",
            EquipmentType.Trophy => "Trophy",
            EquipmentType.Lance => "Lance",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    public static long ToDofusDbItemTypeId(this EquipmentType type) =>
        type switch
        {
            EquipmentType.Amulet => 1,
            EquipmentType.Bow => 2,
            EquipmentType.Wand => 3,
            EquipmentType.Staff => 4,
            EquipmentType.Dagger => 5,
            EquipmentType.Sword => 6,
            EquipmentType.Hammer => 7,
            EquipmentType.Shovel => 8,
            EquipmentType.Ring => 9,
            EquipmentType.Belt => 10,
            EquipmentType.Boots => 11,
            EquipmentType.Hat => 16,
            EquipmentType.Cloak => 17,
            EquipmentType.Pet => 18,
            EquipmentType.Petsmount => 121,
            EquipmentType.Mount => 311,
            EquipmentType.Axe => 19,
            EquipmentType.Tool => 20,
            EquipmentType.Pickaxe => 21,
            EquipmentType.Scythe => 22,
            EquipmentType.MagicWeapon => 114,
            EquipmentType.Trophy => 151,
            EquipmentType.Lance => 271,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    public static EquipmentType? EquipmentTypeFromDofusDbTypeId(long type) =>
        type switch
        {
            1 => EquipmentType.Amulet,
            2 => EquipmentType.Bow,
            3 => EquipmentType.Wand,
            4 => EquipmentType.Staff,
            5 => EquipmentType.Dagger,
            6 => EquipmentType.Sword,
            7 => EquipmentType.Hammer,
            8 => EquipmentType.Shovel,
            9 => EquipmentType.Ring,
            10 => EquipmentType.Belt,
            11 => EquipmentType.Boots,
            16 => EquipmentType.Hat,
            17 => EquipmentType.Cloak,
            18 => EquipmentType.Pet,
            121 => EquipmentType.Petsmount,
            311 => EquipmentType.Mount,
            19 => EquipmentType.Axe,
            20 => EquipmentType.Tool,
            21 => EquipmentType.Pickaxe,
            22 => EquipmentType.Scythe,
            114 => EquipmentType.MagicWeapon,
            151 => EquipmentType.Trophy,
            271 => EquipmentType.Lance,
            _ => null
        };
}
