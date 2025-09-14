using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Mounts;
using DofusSharp.DofusDb.ApiClients.Models.Npcs;
using DofusSharp.DofusDb.ApiClients.Models.Ornaments;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Models.Titles;

namespace DofusSharp.DofusDb.ApiClients.Models;

[JsonPolymorphic(
    TypeDiscriminatorPropertyName = "className",
    IgnoreUnrecognizedTypeDiscriminators = true,
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor
)]
[JsonDerivedType(typeof(DofusDbAchievementCategory), "AchievementCategories")]
[JsonDerivedType(typeof(DofusDbAchievementObjective), "AchievementObjectives")]
[JsonDerivedType(typeof(DofusDbAchievementReward), "AchievementRewards")]
[JsonDerivedType(typeof(DofusDbAchievement), "Achievements")]
[JsonDerivedType(typeof(DofusDbCharacteristic), "Characteristics")]
[JsonDerivedType(typeof(DofusDbItem), "Items")]
[JsonDerivedType(typeof(DofusDbWeapon), "Weapons")]
[JsonDerivedType(typeof(DofusDbItemSet), "ItemSets")]
[JsonDerivedType(typeof(DofusDbItemSuperType), "ItemSuperTypes")]
[JsonDerivedType(typeof(DofusDbItemType), "ItemTypes")]
[JsonDerivedType(typeof(DofusDbJob), "Jobs")]
[JsonDerivedType(typeof(DofusDbRecipe), "Recipes")]
[JsonDerivedType(typeof(DofusDbSkill), "Skills")]
[JsonDerivedType(typeof(DofusDbArea), "Areas")]
[JsonDerivedType(typeof(DofusDbDungeon), "Dungeons")]
[JsonDerivedType(typeof(DofusDbMap), "Maps")]
[JsonDerivedType(typeof(DofusDbMapPosition), "MapPositions")]
[JsonDerivedType(typeof(DofusDbSubArea), "SubAreas")]
[JsonDerivedType(typeof(DofusDbSuperArea), "SuperAreas")]
[JsonDerivedType(typeof(DofusDbWorld), "WorldMaps")]
[JsonDerivedType(typeof(DofusDbMonster), "Monsters")]
[JsonDerivedType(typeof(DofusDbMonsterRace), "MonsterRaces")]
[JsonDerivedType(typeof(DofusDbMonsterSuperRace), "MonsterSuperRaces")]
[JsonDerivedType(typeof(DofusDbMount), "Mounts")]
[JsonDerivedType(typeof(DofusDbMountFamily), "MountFamilies")]
[JsonDerivedType(typeof(DofusDbMountBehavior), "MountBehaviors")]
[JsonDerivedType(typeof(DofusDbNpc), "Npcs")]
[JsonDerivedType(typeof(DofusDbNpcMessage), "NpcMessages")]
[JsonDerivedType(typeof(DofusDbServer), "Servers")]
[JsonDerivedType(typeof(DofusDbSpell), "Spells")]
[JsonDerivedType(typeof(DofusDbSpellLevel), "SpellLevels")]
[JsonDerivedType(typeof(DofusDbSpellState), "SpellStates")]
[JsonDerivedType(typeof(DofusDbSpellType), "SpellTypes")]
[JsonDerivedType(typeof(DofusDbSpellVariant), "SpellVariants")]
[JsonDerivedType(typeof(DofusDbOrnament), "Ornaments")]
[JsonDerivedType(typeof(DofusDbTitle), "Titles")]
public abstract class DofusDbResource : IEquatable<DofusDbResource>
{
    /// <summary>
    ///     The unique identifier of the resource.
    /// </summary>
    public long? Id { get; init; }

    /// <summary>
    ///     The creation date of the resource in the database.
    /// </summary>
    public DateTimeOffset? CreatedAt { get; init; }

    /// <summary>
    ///     The last update date of the resource in the database.
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; init; }

    public bool Equals(DofusDbResource? other)
    {
        if (other?.Id is null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        if (obj.GetType() != GetType())
        {
            return false;
        }
        return Equals((DofusDbResource)obj);
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(DofusDbResource? left, DofusDbResource? right) => Equals(left, right);

    public static bool operator !=(DofusDbResource? left, DofusDbResource? right) => !Equals(left, right);
}
