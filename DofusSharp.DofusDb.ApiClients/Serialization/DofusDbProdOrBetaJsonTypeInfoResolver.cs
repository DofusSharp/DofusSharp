using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using DofusSharp.DofusDb.ApiClients.Models;
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

namespace DofusSharp.DofusDb.ApiClients.Serialization;

static class DofusDbProdOrBetaJsonTypeInfoResolver
{
    public static void Modifier(JsonTypeInfo obj, bool useBetaModels)
    {
        if (obj.Type == typeof(DofusDbResource))
        {
            SetDofusDbResourcePolymorphismOptions(obj, useBetaModels);
        }

        if (obj.Type == typeof(DofusDbItem))
        {
            SetDofusDbItemPolymorphismOptions(obj, useBetaModels);
        }
    }

    static void SetDofusDbResourcePolymorphismOptions(JsonTypeInfo typeInfo, bool useBetaModels) =>
        typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
        {
            TypeDiscriminatorPropertyName = "className",
            IgnoreUnrecognizedTypeDiscriminators = true,
            UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor,
            DerivedTypes =
            {
                new JsonDerivedType(typeof(DofusDbAchievementCategory), useBetaModels ? "AchievementCategoryData" : "AchievementCategories"),
                new JsonDerivedType(typeof(DofusDbAchievementObjective), useBetaModels ? "AchievementObjectiveData" : "AchievementObjectives"),
                new JsonDerivedType(typeof(DofusDbAchievementReward), useBetaModels ? "AchievementRewardData" : "AchievementRewards"),
                new JsonDerivedType(typeof(DofusDbAchievement), useBetaModels ? "AchievementData" : "Achievements"),
                new JsonDerivedType(typeof(DofusDbCharacteristic), useBetaModels ? "CharacteristicData" : "Characteristics"),
                new JsonDerivedType(typeof(DofusDbItem), useBetaModels ? "ItemData" : "Items"),
                new JsonDerivedType(typeof(DofusDbWeapon), useBetaModels ? "WeaponData" : "Weapons"),
                new JsonDerivedType(typeof(DofusDbItemSet), useBetaModels ? "ItemSetData" : "ItemSets"),
                new JsonDerivedType(typeof(DofusDbItemSuperType), useBetaModels ? "ItemSuperTypeData" : "ItemSuperTypes"),
                new JsonDerivedType(typeof(DofusDbItemType), useBetaModels ? "ItemTypeData" : "ItemTypes"),
                new JsonDerivedType(typeof(DofusDbJob), useBetaModels ? "JobData" : "Jobs"),
                new JsonDerivedType(typeof(DofusDbRecipe), useBetaModels ? "RecipeData" : "Recipes"),
                new JsonDerivedType(typeof(DofusDbSkill), useBetaModels ? "SkillData" : "Skills"),
                new JsonDerivedType(typeof(DofusDbArea), useBetaModels ? "AreaData" : "Areas"),
                new JsonDerivedType(typeof(DofusDbDungeon), useBetaModels ? "DungeonData" : "Dungeons"),
                new JsonDerivedType(typeof(DofusDbMap), useBetaModels ? "MapData" : "Maps"),
                new JsonDerivedType(typeof(DofusDbMapPosition), useBetaModels ? "MapPositionData" : "MapPositions"),
                new JsonDerivedType(typeof(DofusDbSubArea), useBetaModels ? "SubAreaData" : "SubAreas"),
                new JsonDerivedType(typeof(DofusDbSuperArea), useBetaModels ? "SuperAreaData" : "SuperAreas"),
                new JsonDerivedType(typeof(DofusDbWorld), useBetaModels ? "WorldData" : "WorldMaps"),
                new JsonDerivedType(typeof(DofusDbMonster), useBetaModels ? "MonsterData" : "Monsters"),
                new JsonDerivedType(typeof(DofusDbMonsterRace), useBetaModels ? "MonsterRaceData" : "MonsterRaces"),
                new JsonDerivedType(typeof(DofusDbMonsterSuperRace), useBetaModels ? "MonsterSuperRaceData" : "MonsterSuperRaces"),
                new JsonDerivedType(typeof(DofusDbMount), useBetaModels ? "MountData" : "Mounts"),
                new JsonDerivedType(typeof(DofusDbMountFamily), useBetaModels ? "MountFamilyData" : "MountFamilies"),
                new JsonDerivedType(typeof(DofusDbMountBehavior), useBetaModels ? "MountBehaviorData" : "MountBehaviors"),
                new JsonDerivedType(typeof(DofusDbNpc), useBetaModels ? "NpcData" : "Npcs"),
                new JsonDerivedType(typeof(DofusDbNpcMessage), useBetaModels ? "NpcMessageData" : "NpcMessages"),
                new JsonDerivedType(typeof(DofusDbServer), useBetaModels ? "ServerData" : "Servers"),
                new JsonDerivedType(typeof(DofusDbSpell), useBetaModels ? "SpellData" : "Spells"),
                new JsonDerivedType(typeof(DofusDbSpellLevel), useBetaModels ? "SpellLevelData" : "SpellLevels"),
                new JsonDerivedType(typeof(DofusDbSpellState), useBetaModels ? "SpellStateData" : "SpellStates"),
                new JsonDerivedType(typeof(DofusDbSpellType), useBetaModels ? "SpellTypeData" : "SpellTypes"),
                new JsonDerivedType(typeof(DofusDbSpellVariant), useBetaModels ? "SpellVariantData" : "SpellVariants"),
                new JsonDerivedType(typeof(DofusDbOrnament), useBetaModels ? "OrnamentData" : "Ornaments"),
                new JsonDerivedType(typeof(DofusDbTitle), useBetaModels ? "TitleData" : "Titles")
            }
        };

    static void SetDofusDbItemPolymorphismOptions(JsonTypeInfo typeInfo, bool useBetaModels) =>
        typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
        {
            TypeDiscriminatorPropertyName = "className",
            IgnoreUnrecognizedTypeDiscriminators = true,
            UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor,
            DerivedTypes =
            {
                new JsonDerivedType(typeof(DofusDbWeapon), useBetaModels ? "WeaponData" : "Weapons")
            }
        };
}
