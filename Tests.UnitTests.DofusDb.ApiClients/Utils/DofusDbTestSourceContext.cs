using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Search;

namespace Tests.UnitTests.DofusDb.ApiClients.Utils;

[JsonSerializable(typeof(DofusDbResourceForTest))]
[JsonSerializable(typeof(DofusDbSearchResult<DofusDbResourceForTest>))]
partial class DofusDbTestSourceContext : JsonSerializerContext;
