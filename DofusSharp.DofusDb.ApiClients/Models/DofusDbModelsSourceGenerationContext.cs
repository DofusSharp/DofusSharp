using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Models.Alignments;
using DofusSharp.DofusDb.ApiClients.Models.Almanax;
using DofusSharp.DofusDb.ApiClients.Models.Alterations;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Mounts;
using DofusSharp.DofusDb.ApiClients.Models.Npcs;
using DofusSharp.DofusDb.ApiClients.Models.Ornaments;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Social;
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
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAchievement>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAchievementCategory>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAchievementObjective>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAchievementReward>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAlteration>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAllianceRight>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAlmanaxCalendar>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAlignmentRank>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbAlignmentSide>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbArea>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbCharacteristic>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbDungeon>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbGuildRight>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItem>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItemSet>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItemSuperType>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItemType>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbJob>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMap>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMapPosition>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMonster>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMonsterRace>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMonsterSuperRace>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMount>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMountBehavior>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMountFamily>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbNpc>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbNpcMessage>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbOrnament>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbRecipe>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbServer>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSkill>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpell>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellLevel>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellState>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellType>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellVariant>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSubArea>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSuperArea>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbTitle>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbWeapon>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbWorld>))]
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web, WriteIndented = true)]
public partial class DofusDbModelsSourceGenerationContext : JsonSerializerContext
{
    /// <summary>
    ///     Get a <see cref="JsonSerializerOptions" /> with information for all DofusDb models, using production models.
    /// </summary>
    public static JsonSerializerOptions ProdOptions => CreateOptions(false);

    /// <summary>
    ///     Get a <see cref="JsonSerializerOptions" /> with information for all DofusDb models, using beta models.
    /// </summary>
    public static JsonSerializerOptions BetaOptions => CreateOptions(true);

    static JsonSerializerOptions CreateOptions(bool useBetaModels) =>
        new(JsonSerializerDefaults.Web)
        {
            AllowOutOfOrderMetadataProperties = true,
            TypeInfoResolver = Default.WithAddedModifier(ti => DofusDbProdOrBetaJsonTypeInfoResolver.Modifier(ti, useBetaModels)),
            Converters =
            {
                new JsonStringEnumConverter<DofusDbGender>(),
                new JsonStringEnumConverter<ImageFormat>(),
                new JsonStringEnumConverter<DofusDbImageScale>(),
                new JsonStringEnumConverter<DofusDbLanguage>(),
                new JsonStringEnumConverter<DofusDbCriterionResourceType>(JsonNamingPolicy.KebabCaseLower),
                new DofusDbValueTupleJsonConverter<int, int>(),
                new DofusDbValueTupleJsonConverter<int, double>(),
                new DofusDbValueTupleJsonConverter<long, long>(),
                new DofusDbValueOrFalseJsonConverter<DofusDbItemSetMinimal>(),
                new DofusDbDateOnlyJsonConverter(),
                new DofusDbCriterionJsonConverter()
            }
        };
}
