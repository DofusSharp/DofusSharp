using System.Text.Json;
using DofusSharp.DofusDb.ApiClients.Models;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;
using FluentAssertions;

namespace Tests.UnitTests.DofusDb.ApiClients;

public class DofusDbCriterionJsonConverterTest
{
    readonly DofusDbModelsSourceGenerationContext _context;

    public DofusDbCriterionJsonConverterTest()
    {
        _context = DofusDbModelsSourceGenerationContext.Instance;
        AssertionOptions.FormattingOptions.MaxDepth = 10;
    }

    [Fact]
    public void Deserialize_ShouldDeserializeSimpleCriterion_Text()
    {
        DofusDbCriterion? deserialized = JsonSerializer.Deserialize("""["TEXT"]""", _context.DofusDbCriterion);
        deserialized.Should().Be(new DofusDbCriterionText("TEXT"));
    }

    [Fact]
    public void Deserialize_ShouldDeserializeSimpleCriterion_Resource()
    {
        DofusDbCriterion? deserialized = JsonSerializer.Deserialize("""[{"type":"titles","id":42}]""", _context.DofusDbCriterion);
        deserialized.Should().Be(new DofusDbCriterionResource(42, DofusDbCriterionResourceType.Titles));
    }

    [Fact]
    public void Deserialize_ShouldDeserializeSimpleCriterion_Collection()
    {
        DofusDbCriterion? deserialized = JsonSerializer.Deserialize("""[["TEXT BEFORE",{"type":"titles","id":42},"TEXT AFTER"]]""", _context.DofusDbCriterion);
        deserialized
            .Should()
            .Be(
                new DofusDbCriterionSequence(
                    [
                        new DofusDbCriterionText("TEXT BEFORE"),
                        new DofusDbCriterionResource(42, DofusDbCriterionResourceType.Titles),
                        new DofusDbCriterionText("TEXT AFTER")
                    ]
                )
            );
    }

    [Fact]
    public void Deserialize_ShouldDeserializeSimpleCriterion_Operation()
    {
        DofusDbCriterion? deserialized = JsonSerializer.Deserialize("""["TEXT BEFORE","\u0026","TEXT AFTER"]""", _context.DofusDbCriterion);
        deserialized.Should().Be(new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT BEFORE"), DofusDbCriterionOperator.And, new DofusDbCriterionText("TEXT AFTER")));
    }


    [Fact]
    public void Deserialize_ShouldDeserializeNestedCollections()
    {
        DofusDbCriterion? deserialized = JsonSerializer.Deserialize(
            """[["TEXT BEFORE",{"type":"titles","id":42},["TEXT MIDDLE",{"type":"titles","id":42},"TEXT AFTER"]]]""",
            _context.DofusDbCriterion
        );
        deserialized
            .Should()
            .Be(
                new DofusDbCriterionSequence(
                    [
                        new DofusDbCriterionText("TEXT BEFORE"),
                        new DofusDbCriterionResource(42, DofusDbCriterionResourceType.Titles),
                        new DofusDbCriterionSequence(
                            [
                                new DofusDbCriterionText("TEXT MIDDLE"),
                                new DofusDbCriterionResource(42, DofusDbCriterionResourceType.Titles),
                                new DofusDbCriterionText("TEXT AFTER")
                            ]
                        )
                    ]
                )
            );
    }

    [Fact]
    public void Deserialize_ShouldUnflattenNestedOperations_And()
    {
        DofusDbCriterion? deserialized = JsonSerializer.Deserialize(
            """["TEXT BEFORE BEFORE","\u0026","TEXT BEFORE","\u0026","TEXT AFTER","\u0026","TEXT AFTER AFTER"]""",
            _context.DofusDbCriterion
        );
        deserialized
            .Should()
            .Be(
                new DofusDbCriterionOperation(
                    new DofusDbCriterionOperation(
                        new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT BEFORE BEFORE"), DofusDbCriterionOperator.And, new DofusDbCriterionText("TEXT BEFORE")),
                        DofusDbCriterionOperator.And,
                        new DofusDbCriterionText("TEXT AFTER")
                    ),
                    DofusDbCriterionOperator.And,
                    new DofusDbCriterionText("TEXT AFTER AFTER")
                )
            );
    }

    [Fact]
    public void Deserialize_ShouldFlattenNestedOperations_Or()
    {
        DofusDbCriterion? deserialized = JsonSerializer.Deserialize(
            """["TEXT BEFORE BEFORE","|","TEXT BEFORE","|","TEXT AFTER","|","TEXT AFTER AFTER"]""",
            _context.DofusDbCriterion
        );
        deserialized
            .Should()
            .Be(
                new DofusDbCriterionOperation(
                    new DofusDbCriterionOperation(
                        new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT BEFORE BEFORE"), DofusDbCriterionOperator.Or, new DofusDbCriterionText("TEXT BEFORE")),
                        DofusDbCriterionOperator.Or,
                        new DofusDbCriterionText("TEXT AFTER")
                    ),
                    DofusDbCriterionOperator.Or,
                    new DofusDbCriterionText("TEXT AFTER AFTER")
                )
            );
    }

    [Fact]
    public void Deserialize_ShouldNotChangePrecedenceOfOperations_LeftOrAndRightOr()
    {
        DofusDbCriterion? deserialized = JsonSerializer.Deserialize(
            """["TEXT BEFORE BEFORE","|","TEXT BEFORE","\u0026",["TEXT AFTER","|","TEXT AFTER AFTER"]]""",
            _context.DofusDbCriterion
        );
        deserialized
            .Should()
            .Be(
                new DofusDbCriterionOperation(
                    new DofusDbCriterionText("TEXT BEFORE BEFORE"),
                    DofusDbCriterionOperator.Or,
                    new DofusDbCriterionOperation(
                        new DofusDbCriterionText("TEXT BEFORE"),
                        DofusDbCriterionOperator.And,
                        new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT AFTER"), DofusDbCriterionOperator.Or, new DofusDbCriterionText("TEXT AFTER AFTER"))
                    )
                )
            );
    }

    [Fact]
    public void Deserialize_ShouldNotChangePrecedenceOfOperations_RightOrAndLeftOr()
    {
        DofusDbCriterion? deserialized = JsonSerializer.Deserialize(
            """[["TEXT BEFORE BEFORE","|","TEXT BEFORE"],"\u0026","TEXT AFTER","|","TEXT AFTER AFTER"]""",
            _context.DofusDbCriterion
        );
        deserialized
            .Should()
            .Be(
                new DofusDbCriterionOperation(
                    new DofusDbCriterionOperation(
                        new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT BEFORE BEFORE"), DofusDbCriterionOperator.Or, new DofusDbCriterionText("TEXT BEFORE")),
                        DofusDbCriterionOperator.And,
                        new DofusDbCriterionText("TEXT AFTER")
                    ),
                    DofusDbCriterionOperator.Or,
                    new DofusDbCriterionText("TEXT AFTER AFTER")
                )
            );
    }

    [Fact]
    public void Serialize_ShouldSerializeSimpleCriterion_Text()
    {
        DofusDbCriterionText criterion = new("TEXT");
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""["TEXT"]""");
    }

    [Fact]
    public void Serialize_ShouldSerializeSimpleCriterion_Resource()
    {
        DofusDbCriterionResource criterion = new(42, DofusDbCriterionResourceType.Titles);
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""[{"id":42,"type":"titles"}]""");
    }

    [Fact]
    public void Serialize_ShouldSerializeSimpleCriterion_Collection()
    {
        DofusDbCriterionSequence criterion = new(
            [
                new DofusDbCriterionText("TEXT BEFORE"),
                new DofusDbCriterionResource(42, DofusDbCriterionResourceType.Titles),
                new DofusDbCriterionText("TEXT AFTER")
            ]
        );
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""[["TEXT BEFORE",{"id":42,"type":"titles"},"TEXT AFTER"]]""");
    }

    [Fact]
    public void Serialize_ShouldSerializeSimpleCriterion_Operation()
    {
        DofusDbCriterionOperation criterion = new(new DofusDbCriterionText("TEXT BEFORE"), DofusDbCriterionOperator.And, new DofusDbCriterionText("TEXT AFTER"));
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""["TEXT BEFORE","\u0026","TEXT AFTER"]""");
    }

    [Fact]
    public void Serialize_ShouldSerializeNestedCollections()
    {
        DofusDbCriterionSequence criterion = new(
            [
                new DofusDbCriterionText("TEXT BEFORE"),
                new DofusDbCriterionResource(42, DofusDbCriterionResourceType.Titles),
                new DofusDbCriterionSequence(
                    [
                        new DofusDbCriterionText("TEXT MIDDLE"),
                        new DofusDbCriterionResource(42, DofusDbCriterionResourceType.Titles),
                        new DofusDbCriterionText("TEXT AFTER")
                    ]
                )
            ]
        );
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""[["TEXT BEFORE",{"id":42,"type":"titles"},["TEXT MIDDLE",{"id":42,"type":"titles"},"TEXT AFTER"]]]""");
    }

    [Fact]
    public void Serialize_ShouldFlattenNestedOperations_And()
    {
        DofusDbCriterionOperation criterion = new(
            new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT BEFORE BEFORE"), DofusDbCriterionOperator.And, new DofusDbCriterionText("TEXT BEFORE")),
            DofusDbCriterionOperator.And,
            new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT AFTER"), DofusDbCriterionOperator.And, new DofusDbCriterionText("TEXT AFTER AFTER"))
        );
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""["TEXT BEFORE BEFORE","\u0026","TEXT BEFORE","\u0026","TEXT AFTER","\u0026","TEXT AFTER AFTER"]""");
    }

    [Fact]
    public void Serialize_ShouldFlattenNestedOperations_Or()
    {
        DofusDbCriterionOperation criterion = new(
            new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT BEFORE BEFORE"), DofusDbCriterionOperator.Or, new DofusDbCriterionText("TEXT BEFORE")),
            DofusDbCriterionOperator.Or,
            new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT AFTER"), DofusDbCriterionOperator.Or, new DofusDbCriterionText("TEXT AFTER AFTER"))
        );
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""["TEXT BEFORE BEFORE","|","TEXT BEFORE","|","TEXT AFTER","|","TEXT AFTER AFTER"]""");
    }

    [Fact]
    public void Serialize_ShouldNotChangePrecedenceOfOperations_AndOr()
    {
        DofusDbCriterionOperation criterion = new(
            new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT BEFORE BEFORE"), DofusDbCriterionOperator.Or, new DofusDbCriterionText("TEXT BEFORE")),
            DofusDbCriterionOperator.And,
            new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT AFTER"), DofusDbCriterionOperator.Or, new DofusDbCriterionText("TEXT AFTER AFTER"))
        );
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""[["TEXT BEFORE BEFORE","|","TEXT BEFORE"],"\u0026",["TEXT AFTER","|","TEXT AFTER AFTER"]]""");
    }

    [Fact]
    public void Serialize_ShouldNotChangePrecedenceOfOperations_OrAnd()
    {
        DofusDbCriterionOperation criterion = new(
            new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT BEFORE BEFORE"), DofusDbCriterionOperator.And, new DofusDbCriterionText("TEXT BEFORE")),
            DofusDbCriterionOperator.Or,
            new DofusDbCriterionOperation(new DofusDbCriterionText("TEXT AFTER"), DofusDbCriterionOperator.And, new DofusDbCriterionText("TEXT AFTER AFTER"))
        );
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""["TEXT BEFORE BEFORE","\u0026","TEXT BEFORE","|","TEXT AFTER","\u0026","TEXT AFTER AFTER"]""");
    }
}
