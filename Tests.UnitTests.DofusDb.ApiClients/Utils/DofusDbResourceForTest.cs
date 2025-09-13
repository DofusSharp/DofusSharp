using DofusSharp.DofusDb.ApiClients.Models;

namespace Tests.UnitTests.DofusDb.ApiClients.Utils;

class DofusDbResourceForTest : DofusDbResource
{
    public string? ClassName { get; set; }
    public string Prop1 { get; set; } = "";
    public int Prop2 { get; set; }
    public bool Prop3 { get; set; }
}
