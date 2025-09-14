using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Mounts;
using DofusSharp.DofusDb.ApiClients.Models.Ornaments;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Models.Titles;
using DofusSharp.DofusDb.ApiClients.Search;
using DofusSharp.DofusDb.ApiClients.Serialization;

namespace DofusSharp.DofusDb.ApiClients.Models;

[JsonSerializable(typeof(DofusDbResource))]
[JsonSerializable(typeof(DofusDbCriterion))]
[JsonSerializable(typeof(DofusDbSearchQuery))]
// Search results: all concrete search results must be listed here, there is currently no way to tell the generation context that it must combine a generic type with
// all the types it already knows
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAchievementCategory>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAchievementObjective>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAchievementReward>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAchievementRewardBeta>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAchievement>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAlmanaxCalendar>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbCharacteristic>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItem>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbWeapon>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbWeaponBeta>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItemSet>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItemSuperType>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItemType>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbJob>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbRecipe>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSkill>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbArea>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbDungeon>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMap>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMapPosition>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSubArea>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSuperArea>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbWorld>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMonster>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMonsterRace>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMonsterSuperRace>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMount>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMountFamily>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMountBehavior>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbServer>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpell>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellLevel>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellState>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellType>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellVariant>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbOrnament>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbTitle>))]
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web, WriteIndented = true)]
public partial class DofusDbModelsSourceGenerationContext : JsonSerializerContext
{
    public static DofusDbModelsSourceGenerationContext Instance { get; } = new(CreateOptions());

    public static JsonSerializerOptions CreateOptions() =>
        new(JsonSerializerDefaults.Web)
        {
            AllowOutOfOrderMetadataProperties = true,
            Converters =
            {
                new JsonStringEnumConverter<DofusDbGender>(),
                new JsonStringEnumConverter<ImageFormat>(),
                new JsonStringEnumConverter<DofusDbImageScale>(),
                new JsonStringEnumConverter<DofusDbLanguage>(),
                new JsonStringEnumConverter<DofusDbCriterionResourceType>(JsonNamingPolicy.KebabCaseLower),
                new DofusDbValueTupleJsonConverter<int, int>(),
                new DofusDbValueTupleJsonConverter<int, double>(),
                new DofusDbValueOrFalseJsonConverter<DofusDbItemSetMinimal>(),
                new DofusDbDateOnlyJsonConverter(),
                new DofusDbCriterionJsonConverter()
            }
        };
}
