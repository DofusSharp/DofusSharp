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
    Shield,
    MagicWeapon,
    Trophy,
    Lance
}

public static class EquipmentTypeExtensions
{
    public static string ToDisplayName(this EquipmentType type) =>
        type switch
        {
            EquipmentType.Amulet => "Amulette",
            EquipmentType.Bow => "Arc",
            EquipmentType.Wand => "Baguette",
            EquipmentType.Staff => "Bâton",
            EquipmentType.Dagger => "Dague",
            EquipmentType.Sword => "Épée",
            EquipmentType.Hammer => "Marteau",
            EquipmentType.Shovel => "Pelle",
            EquipmentType.Ring => "Anneau",
            EquipmentType.Belt => "Ceinture",
            EquipmentType.Boots => "Bottes",
            EquipmentType.Hat => "Chapeau",
            EquipmentType.Cloak => "Cape",
            EquipmentType.Pet => "Familier",
            EquipmentType.Petsmount => "Montilier",
            EquipmentType.Mount => "Monture",
            EquipmentType.Axe => "Hache",
            EquipmentType.Tool => "Outil",
            EquipmentType.Pickaxe => "Pioche",
            EquipmentType.Scythe => "Faux",
            EquipmentType.Shield => "Bouclier",
            EquipmentType.MagicWeapon => "Arme magique",
            EquipmentType.Trophy => "Trophée",
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
            EquipmentType.Shield => 82,
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
            82 => EquipmentType.Shield,
            114 => EquipmentType.MagicWeapon,
            151 => EquipmentType.Trophy,
            271 => EquipmentType.Lance,
            _ => null
        };
}
