using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;

namespace DofusSharp.DofusDb.ApiClients.Models;

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
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web, WriteIndented = true)]
partial class SourceGenerationContext : JsonSerializerContext
{
}
