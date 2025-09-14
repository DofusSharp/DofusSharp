using System.Text.Json;
using System.Text.Json.Serialization;
using DofusSharp.DofusDb.ApiClients.Models.Criterion;
using DofusSharp.DofusDb.ApiClients.Models.Titles;
using DofusSharp.DofusDb.ApiClients.Serialization;
using FluentAssertions;
using Tests.UnitTests.DofusDb.ApiClients.Utils;

namespace Tests.UnitTests.DofusDb.ApiClients;

public class DofusDbCriterionJsonConverterTest
{
    readonly DofusDbTestSourceContext _context;

    public DofusDbCriterionJsonConverterTest()
    {
        JsonSerializerOptions options = new(JsonSerializerDefaults.Web)
            { Converters = { new DofusDbCriterionJsonConverter() }, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault };
        _context = new DofusDbTestSourceContext(options);

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
        DofusDbCriterion? deserialized = JsonSerializer.Deserialize("""[{"className":"Titles","id":42}]""", _context.DofusDbCriterion);
        deserialized.Should().Be(new DofusDbCriterionResource(new DofusDbTitle { Id = 42 }));
    }

    [Fact]
    public void Deserialize_ShouldDeserializeSimpleCriterion_Collection()
    {
        DofusDbCriterion? deserialized = JsonSerializer.Deserialize("""[["TEXT BEFORE",{"className":"Titles","id":42},"TEXT AFTER"]]""", _context.DofusDbCriterion);
        deserialized
            .Should()
            .Be(
                new DofusDbCriterionSequence(
                    [
                        new DofusDbCriterionText("TEXT BEFORE"),
                        new DofusDbCriterionResource(new DofusDbTitle { Id = 42 }),
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
            """[["TEXT BEFORE",{"className":"Titles","id":42},["TEXT MIDDLE",{"className":"Titles","id":42},"TEXT AFTER"]]]""",
            _context.DofusDbCriterion
        );
        deserialized
            .Should()
            .Be(
                new DofusDbCriterionSequence(
                    [
                        new DofusDbCriterionText("TEXT BEFORE"),
                        new DofusDbCriterionResource(new DofusDbTitle { Id = 42 }),
                        new DofusDbCriterionSequence(
                            [
                                new DofusDbCriterionText("TEXT MIDDLE"),
                                new DofusDbCriterionResource(new DofusDbTitle { Id = 42 }),
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
        DofusDbCriterionResource criterion = new(new DofusDbTitle { Id = 42 });
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""[{"id":42}]""");
    }

    [Fact]
    public void Serialize_ShouldSerializeSimpleCriterion_Collection()
    {
        DofusDbCriterionSequence criterion = new(
            [
                new DofusDbCriterionText("TEXT BEFORE"),
                new DofusDbCriterionResource(new DofusDbTitle { Id = 42 }),
                new DofusDbCriterionText("TEXT AFTER")
            ]
        );
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""[["TEXT BEFORE",{"id":42},"TEXT AFTER"]]""");
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
                new DofusDbCriterionResource(new DofusDbTitle { Id = 42 }),
                new DofusDbCriterionSequence(
                    [
                        new DofusDbCriterionText("TEXT MIDDLE"),
                        new DofusDbCriterionResource(new DofusDbTitle { Id = 42 }),
                        new DofusDbCriterionText("TEXT AFTER")
                    ]
                )
            ]
        );
        string serialized = JsonSerializer.Serialize(criterion, _context.DofusDbCriterion);
        serialized.Should().Be("""[["TEXT BEFORE",{"id":42},["TEXT MIDDLE",{"id":42},"TEXT AFTER"]]]""");
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
