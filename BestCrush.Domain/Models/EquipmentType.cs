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
}
