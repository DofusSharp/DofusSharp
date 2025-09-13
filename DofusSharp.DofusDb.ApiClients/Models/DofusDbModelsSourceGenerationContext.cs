using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Models.Titles;
using DofusSharp.DofusDb.ApiClients.Search;

namespace DofusSharp.DofusDb.ApiClients.Models;

// Resources
[JsonSerializable(typeof(DofusDbCharacteristic))]
[JsonSerializable(typeof(DofusDbItem))]
[JsonSerializable(typeof(DofusDbWeapon))]
[JsonSerializable(typeof(DofusDbWeaponBeta))]
[JsonSerializable(typeof(DofusDbItemType))]
[JsonSerializable(typeof(DofusDbItemSuperType))]
[JsonSerializable(typeof(DofusDbItemSet))]
[JsonSerializable(typeof(DofusDbJob))]
[JsonSerializable(typeof(DofusDbRecipe))]
[JsonSerializable(typeof(DofusDbSkill))]
[JsonSerializable(typeof(DofusDbMap))]
[JsonSerializable(typeof(DofusDbMapPosition))]
[JsonSerializable(typeof(DofusDbArea))]
[JsonSerializable(typeof(DofusDbSubArea))]
[JsonSerializable(typeof(DofusDbSuperArea))]
[JsonSerializable(typeof(DofusDbWorld))]
[JsonSerializable(typeof(DofusDbMonster))]
[JsonSerializable(typeof(DofusDbMonsterRace))]
[JsonSerializable(typeof(DofusDbMonsterSuperRace))]
[JsonSerializable(typeof(DofusDbDungeon))]
[JsonSerializable(typeof(DofusDbServer))]
[JsonSerializable(typeof(DofusDbSpell))]
[JsonSerializable(typeof(DofusDbSpellLevel))]
[JsonSerializable(typeof(DofusDbSpellState))]
[JsonSerializable(typeof(DofusDbSpellType))]
[JsonSerializable(typeof(DofusDbSpellVariant))]
[JsonSerializable(typeof(DofusDbTitle))]
// Search results
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbCharacteristic>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItem>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbWeapon>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbWeaponBeta>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItemType>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItemSuperType>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbItemSet>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbJob>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbRecipe>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSkill>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMap>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMapPosition>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbArea>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSubArea>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSuperArea>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbWorld>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMonster>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMonsterRace>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbMonsterSuperRace>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbDungeon>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbServer>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpell>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellLevel>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellState>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellType>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbSpellVariant>))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbTitle>))]
// Others
[JsonSerializable(typeof(DofusDbSearchQuery))]
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web, WriteIndented = true)]
public partial class DofusDbModelsSourceGenerationContext : JsonSerializerContext
{
}
